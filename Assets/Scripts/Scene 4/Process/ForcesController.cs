using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene4
{
    public class ForcesController : MonoBehaviour
    {
        public int framerate = 60;

        private bool unified = false;
        private Vector2 relativeSpeedDir = Vector2.zero;
        private Vector2 lastFrameSpeedTop = Vector2.zero;
        [System.Serializable]
        public class TopBody 
        {
            public GameObject obj;

            [HideInInspector] public Vector2 mg, N, Fr, ma;

            public ArrowHandler mgArrow, nArrow, frArrow, vArrow, maArrow;

            public float friction;

            public void UpdateArrows()
            {
                mgArrow.SetVector(mg);
                nArrow.SetVector(N);
                frArrow.SetVector(Fr);
                if (stopped) vArrow.SetVector(obj.GetComponent<TopBodyHandler>().Impulse);
                else vArrow.SetVector(obj.GetComponent<Rigidbody2D>().velocity);
                maArrow.SetVector(ma);
            }

        }

        [System.Serializable]
        public class BottomBody
        {
            public GameObject obj;

            [HideInInspector] public Vector2 mg, N, P, FrBody, FrPlane, ma;

            public ArrowHandler mgArrow, nArrow, pArrow, frPlaneArrow, frBodyArrow, vArrow, maArrow;

            public float friction;

            public void UpdateArrows()
            {
                mgArrow.SetVector(mg);
                nArrow.SetVector(N);
                pArrow.SetVector(P);
                frBodyArrow.SetVector(FrBody);
                frPlaneArrow.SetVector(FrPlane);
                if (stopped) vArrow.SetVector(obj.GetComponent<BottomBodyHandler>().Impulse);
                else vArrow.SetVector(obj.GetComponent<Rigidbody2D>().velocity);
                maArrow.SetVector(ma);
            }
        }

        public TopBody topBody = new TopBody();
        public BottomBody bottomBody = new BottomBody();

        public static bool stopped { get; set; } = true;
        public float threshold = 0.05f;
        public float speedThreshold = 0.2f;

        public void UpdateAll()
        {
            UpdateForces();
            UpdateArrows();
        }

        private void OnEnable()
        {
            UpdateAll();
        }

        private void FixedUpdate()
        {
            UpdateForces();
        }

        private void Update()
        {
            UpdateArrows();
        }

        public void UpdateForces()
        {
            ProcessController pc = GetComponent<ProcessController>();

            if (Mathf.Abs(topBody.obj.transform.position.x - bottomBody.obj.transform.position.x)
                > bottomBody.obj.GetComponent<SpriteRenderer>().size.x / 2f * bottomBody.obj.transform.localScale.x
                -topBody.obj.GetComponent<SpriteRenderer>().size.x / 2f * topBody.obj.transform.localScale.x
                -0.1f) 
                pc.PauseNoResume.Invoke();

            float _speedThreshold = speedThreshold * Time.timeScale;
            Rigidbody2D rb = topBody.obj.GetComponent<Rigidbody2D>();
            float _threshold = threshold * rb.mass;

            if (relativeSpeedDir == Vector2.zero) relativeSpeedDir = (rb.velocity - bottomBody.obj.GetComponent<Rigidbody2D>().velocity).normalized;

            topBody.mg = rb.mass * Physics.gravity;
            topBody.N = -Vector3.Project(topBody.mg, topBody.obj.transform.up);
            topBody.Fr = topBody.N.magnitude * topBody.friction 
                * -Vector3.Project((rb.velocity - bottomBody.obj.GetComponent<Rigidbody2D>().velocity).normalized, topBody.obj.transform.right);


            if (stopped && topBody.obj.GetComponent<TopBodyHandler>().Impulse.magnitude > 0.001f)
                topBody.Fr = topBody.N.magnitude * topBody.friction 
                    * -Vector3.Project(topBody.obj.GetComponent<TopBodyHandler>().Impulse.normalized, topBody.obj.transform.right);

            else if (stopped || (rb.velocity - bottomBody.obj.GetComponent<Rigidbody2D>().velocity).magnitude < _speedThreshold || 
                (relativeSpeedDir - (rb.velocity - bottomBody.obj.GetComponent<Rigidbody2D>().velocity).normalized).magnitude > 0.001f)
            {
                bottomBody.ma -= bottomBody.FrBody;

                if (!stopped)
                {
                    topBody.Fr = bottomBody.ma / bottomBody.obj.GetComponent<Rigidbody2D>().mass * rb.mass;
                    unified = true;
                    rb.velocity = bottomBody.obj.GetComponent<Rigidbody2D>().velocity;
                    Debug.Log("Detected");
                }

                bottomBody.ma += bottomBody.FrBody;
            }

            topBody.ma = topBody.mg + topBody.N + topBody.Fr;



            rb = bottomBody.obj.GetComponent<Rigidbody2D>();
            _threshold = threshold * rb.mass;
            bottomBody.mg = rb.mass * Physics.gravity;
            bottomBody.P = -topBody.N;
            bottomBody.N = -Vector3.Project(bottomBody.mg + bottomBody.P, bottomBody.obj.transform.up);
            bottomBody.FrPlane = bottomBody.N.magnitude * bottomBody.friction
                * -Vector3.Project(rb.velocity.normalized, bottomBody.obj.transform.right);
            bottomBody.FrBody = -topBody.Fr;
            if (unified) bottomBody.FrBody = Vector2.zero;

            if (stopped && bottomBody.obj.GetComponent<BottomBodyHandler>().Impulse.magnitude > 0.001f)
                bottomBody.FrPlane = bottomBody.N.magnitude * bottomBody.friction
                    * -Vector3.Project(bottomBody.obj.GetComponent<BottomBodyHandler>().Impulse.normalized, bottomBody.obj.transform.right);

            else if (stopped || rb.velocity.magnitude < _speedThreshold)
            {
                if ((bottomBody.N.magnitude * bottomBody.friction * -bottomBody.obj.transform.right).magnitude
                    > Vector3.Project(bottomBody.FrBody, bottomBody.obj.transform.right).magnitude - _threshold)
                {
                    bottomBody.FrPlane = -Vector3.Project(bottomBody.FrBody, bottomBody.obj.transform.right);
                    rb.velocity = Vector2.zero;
                }

                else bottomBody.FrPlane = bottomBody.N.magnitude * bottomBody.friction * -bottomBody.obj.transform.right;

            }

            bottomBody.ma = bottomBody.mg + bottomBody.P + bottomBody.N + bottomBody.FrPlane + bottomBody.FrBody;



            if (bottomBody.ma.magnitude > _threshold)
            {
                bottomBody.obj.GetComponent<Rigidbody2D>().AddForce(bottomBody.ma);
            }
            if (topBody.ma.magnitude > _threshold)
            {
                topBody.obj.GetComponent<Rigidbody2D>().AddForce(topBody.ma);
            }

            if (topBody.mg.magnitude < _threshold) topBody.mgArrow.Disable();
            else topBody.mgArrow.Enable();

            if (topBody.N.magnitude < _threshold) topBody.nArrow.Disable();
            else topBody.nArrow.Enable();

            if (topBody.Fr.magnitude < _threshold) topBody.frArrow.Disable();
            else topBody.frArrow.Enable();

            if (topBody.ma.magnitude < _threshold) topBody.maArrow.Disable();
            else topBody.maArrow.Enable();


            if (bottomBody.mg.magnitude < _threshold) bottomBody.mgArrow.Disable();
            else bottomBody.mgArrow.Enable();

            if (bottomBody.N.magnitude < _threshold) bottomBody.nArrow.Disable();
            else bottomBody.nArrow.Enable();

            if (bottomBody.P.magnitude < _threshold) bottomBody.pArrow.Disable();
            else bottomBody.pArrow.Enable();

            if (bottomBody.FrBody.magnitude < _threshold) bottomBody.frBodyArrow.Disable();
            else bottomBody.frBodyArrow.Enable();

            if (bottomBody.FrPlane.magnitude < _threshold) bottomBody.frPlaneArrow.Disable();
            else bottomBody.frPlaneArrow.Enable();

            if (bottomBody.ma.magnitude < _threshold)
                bottomBody.maArrow.Disable();
            else bottomBody.maArrow.Enable();

            lastFrameSpeedTop = topBody.obj.GetComponent<Rigidbody2D>().velocity;
        }

        public void UpdateArrows()
        {
            topBody.UpdateArrows();
            bottomBody.UpdateArrows();
        }

        public void ResetVariables()
        {
            relativeSpeedDir = Vector2.zero;
            unified = false;
            lastFrameSpeedTop = Vector2.zero;
        }
    }

}
