using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene6
{
    public class ForcesController : MonoBehaviour
    {
        public int framerate = 60;

        [System.Serializable]
        public class Body
        {
            public GameObject obj;

            [HideInInspector] public Vector2 mg, T, ma;

            public ArrowHandler mgArrow, tArrow, vArrow, maArrow;

            public void UpdateArrows()
            {
                mgArrow.SetVector(mg);
                tArrow.SetVector(T);
                if (stopped) vArrow.SetVector(obj.GetComponent<BodyHandler>().Impulse);
                else vArrow.SetVector(obj.GetComponent<Rigidbody2D>().velocity);
                maArrow.SetVector(ma);
            }

        }

        public Body leftBody = new Body();
        public Body rightBody = new Body();

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

            /*float _speedThreshold = speedThreshold * Time.timeScale;
            float m1 = leftBody.obj.GetComponent<Rigidbody2D>().mass;
            float m2 = rightBody.obj.GetComponent<Rigidbody2D>().mass;
            ProcessController pc = GetComponent<ProcessController>();

            Rigidbody2D rb = topBody.obj.GetComponent<Rigidbody2D>();
            float _threshold = m * threshold;

            Vector2 relativeSpeedDir = (rb.velocity - bottomBody.obj.GetComponent<Rigidbody2D>().velocity).normalized;
            Vector2 relativeSpeed = rb.velocity - bottomBody.obj.GetComponent<Rigidbody2D>().velocity;

            topBody.mg = m * Physics.gravity;
            topBody.N = -Vector3.Project(topBody.mg, topBody.obj.transform.up);
            topBody.Fr = topBody.N.magnitude * topBody.friction
                * -Vector3.Project(relativeSpeedDir, topBody.obj.transform.right);
            topBody.F = topBody.obj.GetComponent<TopBodyHandler>().Force;

            if (topBody.F.magnitude - topBody.N.magnitude * topBody.friction < _threshold)
            {
                Debug.Log("detected");
                topBody.F = m / (M + m) * topBody.obj.GetComponent<TopBodyHandler>().Force;
                topBody.Fr = Vector2.zero;
            }

            topBody.ma = topBody.mg + topBody.N + topBody.Fr + topBody.F;


            rb = bottomBody.obj.GetComponent<Rigidbody2D>();
            bottomBody.mg = M * Physics.gravity;
            bottomBody.P = -topBody.N;
            bottomBody.N = -Vector3.Project(bottomBody.mg + bottomBody.P, bottomBody.obj.transform.up);
            bottomBody.FrPlane = bottomBody.N.magnitude * bottomBody.friction
                * -Vector3.Project(rb.velocity.normalized, bottomBody.obj.transform.right);
            bottomBody.FrBody = -topBody.Fr;
            bottomBody.F = bottomBody.obj.GetComponent<BottomBodyHandler>().Force;

            if (topBody.F.magnitude - topBody.N.magnitude * topBody.friction < _threshold)
            {
                bottomBody.FrBody = M / (M + m) * topBody.obj.GetComponent<TopBodyHandler>().Force;
            }

            else if ((bottomBody.F + bottomBody.FrBody).magnitude - bottomBody.N.magnitude * bottomBody.friction < _threshold && rb.velocity.magnitude < _speedThreshold)
            {
                bottomBody.FrPlane = -(bottomBody.F + bottomBody.FrBody);
                rb.velocity = Vector2.zero;
            }

            bottomBody.ma = bottomBody.mg + bottomBody.P + bottomBody.N + bottomBody.FrPlane + bottomBody.FrBody + bottomBody.F;

            _threshold = M * threshold;
            if (bottomBody.ma.magnitude > _threshold)
            {
                bottomBody.obj.GetComponent<Rigidbody2D>().AddForce(bottomBody.ma);
            }

            _threshold = m * threshold;
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

            if (topBody.F.magnitude < _threshold) topBody.fArrow.Disable();
            else topBody.fArrow.Enable();

            if (topBody.ma.magnitude < _threshold) topBody.maArrow.Disable();
            else topBody.maArrow.Enable();


            _threshold = M * threshold;
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

            if (bottomBody.F.magnitude < _threshold) bottomBody.fArrow.Disable();
            else bottomBody.fArrow.Enable();

            if (bottomBody.ma.magnitude < _threshold)
                bottomBody.maArrow.Disable();
            else bottomBody.maArrow.Enable();
            */

        }

        public void UpdateArrows()
        {
            leftBody.UpdateArrows();
            rightBody.UpdateArrows();
        }

        public void ResetVariables()
        {
        }
    }
}

