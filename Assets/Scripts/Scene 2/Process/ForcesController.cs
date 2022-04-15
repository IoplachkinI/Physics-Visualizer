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

        public bool stopped { get; set; } = true;

        private void Update()
        {
            UpdateArrows();
        }

        public void UpdateAll()
        {
            UpdateForces();
            UpdateArrows();
        }

        public void UpdateArrows()
        {
            Rigidbody2D rb = body.GetComponent<Rigidbody2D>();
            if (stopped)
            {
                BodyHandler bh = rb.GetComponent<BodyHandler>();
                v.SetVector(bh.Impulse);
            }
            else
            {

                v.SetVector(rb.velocity);
            }

            vx.SetVector(new Vector2(v.GetVector().x, 0));
            vy.SetVector(new Vector2(0, v.GetVector().y));

        }

        public void UpdateForces()
        {

        }
    }
}
