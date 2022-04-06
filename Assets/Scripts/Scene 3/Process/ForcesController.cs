using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scene3
{
    public class ForcesController : MonoBehaviour
    {
        [System.Serializable]
        public class Body
        {
            [HideInInspector] public float friction;

            public ArrowHandler mgArrow, nArrow, frArrow, maArrow, vArrow;

            [HideInInspector] public Vector2 mg, N, Fr;

            public GameObject obj;

            [HideInInspector] public Rigidbody2D rb;
        }

        [System.Serializable]
        public class Wedge
        {
            [HideInInspector] public float friction;

            public ArrowHandler mgArrow, nArrow, frArrow, frBodyArrow, pArrow, maArrow, vArrow;

            [HideInInspector] public Vector2 mg, N, Fr, FrBody, P;

            public GameObject obj;

            [HideInInspector] public Rigidbody2D rb;
        }

        public Body body = new Body();
        public Wedge wedge = new Wedge();

        [SerializeField] private ProcessController pc;


        public float threshold = 0.05f;

        public bool stopped { get; set; } = true;

        private void OnEnable()
        {
            body.rb = body.obj.GetComponent<Rigidbody2D>();
            wedge.rb = wedge.obj.GetComponent<Rigidbody2D>();
        }

        public void UpdateAll()
        {
            UpdateForces();
            UpdateArrows();
        }

        public void UpdateArrows()
        {
            body.maArrow.SetVector(body.mg + body.N + body.Fr);
            body.mgArrow.SetVector(body.mg);
            body.nArrow.SetVector(body.N);
            body.frArrow.SetVector(body.Fr);
            if (stopped) body.vArrow.SetVector(body.obj.GetComponent<BodyHandler>().Impulse);
            else body.vArrow.SetVector(body.rb.velocity);
        }

        public void UpdateForces()
        {
            float _threshold = threshold * body.rb.mass;

            body.mg = body.rb.mass * Physics2D.gravity;
            body.N = -Vector3.Project(body.mg, body.obj.transform.up);
            body.Fr = body.N.magnitude * body.friction * -Vector3.Project(body.rb.velocity.normalized, body.obj.transform.right);

            if (stopped && body.obj.GetComponent<BodyHandler>().Impulse.magnitude > 0.001f)
                body.Fr = body.N.magnitude * body.friction * -Vector3.Project(body.obj.GetComponent<BodyHandler>().Impulse.normalized, body.obj.transform.right);

            else if (stopped || body.rb.velocity.magnitude < 0.2f)
            {
                if ((body.N.magnitude * body.friction * -body.obj.transform.right).magnitude 
                    > Vector3.Project(body.mg, body.obj.transform.right).magnitude - _threshold)
                {
                    body.Fr = -Vector3.Project(body.mg, body.obj.transform.right);
                    body.rb.velocity = Vector2.zero;
                }

                else body.Fr = body.N.magnitude * body.friction * -body.obj.transform.right;

            }

            if (body.mg.magnitude < _threshold) body.mgArrow.Disable();
            else body.mgArrow.Enable();

            if (body.N.magnitude < _threshold) body.nArrow.Disable();
            else body.nArrow.Enable();

            if (body.Fr.magnitude < _threshold) body.frArrow.Disable();
            else body.frArrow.Enable();

            if ((body.mg + body.N + body.Fr).magnitude < _threshold) body.maArrow.Disable();
            else body.maArrow.Enable();


            _threshold = threshold * wedge.rb.mass;
            wedge.mg = wedge.rb.mass * Physics2D.gravity;
            wedge.FrBody = body.Fr;
            wedge.P = -wedge.N;
            wedge.N = new Vector2(0, -wedge.mg.y + -wedge.P.y + -wedge.FrBody.y);

            wedge.Fr = wedge.N.magnitude * wedge.friction * -Vector3.Project(wedge.rb.velocity.normalized, wedge.obj.transform.right);

            if (wedge.mg.magnitude < _threshold) wedge.mgArrow.Disable();
            else wedge.mgArrow.Enable();

            if (wedge.N.magnitude < _threshold) wedge.nArrow.Disable();
            else wedge.nArrow.Enable();

            if (wedge.Fr.magnitude < _threshold) wedge.frArrow.Disable();
            else wedge.frArrow.Enable();

            if (wedge.FrBody.magnitude < _threshold) wedge.frBodyArrow.Disable();
            else wedge.frBodyArrow.Enable();

            if (wedge.P.magnitude < _threshold) wedge.pArrow.Disable();
            else wedge.pArrow.Enable();

            if ((wedge.mg + wedge.N + wedge.Fr).magnitude < _threshold) wedge.maArrow.Disable();
            else wedge.maArrow.Enable();

            if ((wedge.mg + wedge.N + wedge.Fr + wedge.FrBody + wedge.P).magnitude > _threshold) wedge.rb.AddForce(wedge.mg + wedge.N + wedge.Fr);
            if ((body.mg + body.N + body.Fr).magnitude > _threshold) body.rb.AddForce(body.mg + body.N + body.Fr);
        }

    }
}
