using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene2
{

    public class ForcesController : MonoBehaviour
    {
        [SerializeField] private GameObject body;
        [SerializeField] private ProcessController pc;

        [SerializeField] private ArrowHandler vx;
        [SerializeField] private ArrowHandler vy;
        [SerializeField] private ArrowHandler v;

        private Rigidbody2D rb;
        private Vector2 mg = Vector2.zero;

        public bool stopped { get; set; } = true;

        private void OnEnable()
        {
            rb = body.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            UpdateAll();
        }

        public void UpdateAll()
        {
            UpdateForces();
            UpdateArrows();
        }

        public void UpdateForces()
        {
            if (rb == null) rb = body.GetComponent<Rigidbody2D>();
            mg = Vector2.down * Mathf.Abs(rb.mass * Physics.gravity.y);

            rb.AddForce(mg);
        }

        public void UpdateArrows()
        {
            if (stopped)
            {
                BodyHandler bh = rb.GetComponent<BodyHandler>();
                vx.SetVector(new Vector2(bh.Impulse.x, 0));
                vy.SetVector(new Vector2(0, bh.Impulse.y));
                v.SetVector(bh.Impulse);
            }
            else
            {
                vx.SetVector(new Vector2(rb.velocity.x, 0));
                vy.SetVector(new Vector2(0, rb.velocity.y));
                v.SetVector(rb.velocity);
            }
        }
    }
}
