using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene1
{

    public class ForcesController : MonoBehaviour
    {
        [SerializeField] private GameObject body;
        [SerializeField] private ArrowHandler mgArrow;
        [SerializeField] private ArrowHandler nArrow;
        [SerializeField] private ArrowHandler frArrow;
        [SerializeField] private ArrowHandler vArrow;
        [SerializeField] private ArrowHandler maArrow;
        private Vector2 mg = Vector2.zero;
        private Vector2 N = Vector2.zero;
        private Vector2 Fr = Vector2.zero;

        public bool stopped { get; set; } = true;
        public float friction = 0f;
        public float threshold = 0.05f;

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
            Rigidbody2D rb = body.GetComponent<Rigidbody2D>();
            float _threshold = threshold * rb.mass;
            mg = Vector2.down * Mathf.Abs(rb.mass * Physics.gravity.y);
            N = -Vector3.Project(mg, body.transform.up);
            Fr = N.magnitude * friction * -Vector3.Project(rb.velocity.normalized, body.transform.right);

            if (stopped && body.GetComponent<BodyHandler>().Impulse.magnitude > 0.001f)
                Fr = N.magnitude * friction * -Vector3.Project(body.GetComponent<BodyHandler>().Impulse.normalized, body.transform.right);

            else if (stopped || rb.velocity.magnitude < 0.2f)
            {
                if ((N.magnitude * friction * -body.transform.right).magnitude > Vector3.Project(mg, body.transform.right).magnitude - _threshold)
                {
                    Fr = -Vector3.Project(mg, body.transform.right);
                    rb.velocity = Vector2.zero;
                }

                else Fr = N.magnitude * friction * -body.transform.right;

            }

            if (mg.magnitude < _threshold) mgArrow.Disable();
            else mgArrow.Enable();

            if (N.magnitude < _threshold) nArrow.Disable();
            else nArrow.Enable();

            if (Fr.magnitude < _threshold) frArrow.Disable();
            else frArrow.Enable();

            if ((mg + N + Fr).magnitude < _threshold) maArrow.Disable();
            else maArrow.Enable();

            if ((mg + N + Fr).magnitude < _threshold) return;
            rb.AddForce(mg + N + Fr);
        }

        public void UpdateArrows()
        {
            maArrow.SetVector(mg + N + Fr);
            mgArrow.SetVector(mg);
            nArrow.SetVector(N);
            frArrow.SetVector(Fr);
            if (stopped) vArrow.SetVector(body.GetComponent<BodyHandler>().Impulse);
            else vArrow.SetVector(body.GetComponent<Rigidbody2D>().velocity);
        }
    }
}
