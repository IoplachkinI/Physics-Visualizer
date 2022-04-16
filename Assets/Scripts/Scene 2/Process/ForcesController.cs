using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene2
{

    public class ForcesController : MonoBehaviour
    {
        [SerializeField] private GameObject body;
        [SerializeField] private GameObject plane;
        [SerializeField] private ProcessController pc;

        [SerializeField] private ArrowHandler vx;
        [SerializeField] private ArrowHandler vy;
        [SerializeField] private ArrowHandler v;

        private Vector2 mg;

        public bool stopped { get; set; } = true;

        private void Update()
        {
            UpdateArrows();
        }

        private void FixedUpdate()
        {
            UpdateForces();
        }

        public void UpdateAll()
        {
            UpdateForces();
            UpdateArrows();
        }

        public void UpdateArrows()
        {
            Rigidbody2D rb = body.GetComponent<Rigidbody2D>();
            BodyHandler bh = rb.GetComponent<BodyHandler>();

            if (stopped) v.SetVector(bh.impulse);
            else v.SetVector(rb.velocity);
            vx.SetVector(new Vector2(v.GetVector().x, 0));
            vy.SetVector(new Vector2(0, v.GetVector().y));

        }

        public void UpdateForces()
        {
            Rigidbody2D rb = body.GetComponent<Rigidbody2D>();
            float planeHeight = body.GetComponent<BodyHandler>().planeHeight;
            float bodyHeight = body.GetComponent<BodyHandler>().bodyHeight;

            float dist = Vector3.Project(body.transform.position - plane.transform.position, plane.transform.parent.up).magnitude;
            
            if (dist < planeHeight / 2f + bodyHeight / 2f - 0.001f)
            {
                rb.velocity = Vector2.zero;
                body.transform.position = plane.transform.parent.up * (planeHeight / 2f + bodyHeight / 2f) + plane.transform.position
                    + Vector3.Project(body.transform.position - plane.transform.position, plane.transform.parent.right);

                pc.PauseNoResume.Invoke();
                return;
            }

            mg = rb.mass * Physics.gravity;

            rb.AddForce(mg);
        }
    }
}
