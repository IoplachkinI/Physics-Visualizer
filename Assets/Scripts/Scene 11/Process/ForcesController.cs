using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene11
{
    public class ForcesController : MonoBehaviour
    {
        public int framerate = 60;

        [System.Serializable]
        public class Body
        {
            public GameObject obj;

            public float friction;

            [HideInInspector] public Vector2 mg, Fel, Fr, N, ma;

            public ArrowHandler mgArrow, felArrow, frArrow, nArrow, vArrow, maArrow;

            public void UpdateArrows()
            {
                mgArrow.SetVector(mg);
                felArrow.SetVector(Fel);
                frArrow.SetVector(Fr);
                nArrow.SetVector(N);
                vArrow.SetVector(obj.GetComponent<Rigidbody2D>().velocity);
                maArrow.SetVector(ma);
            }

        }

        public Body body = new Body();
        public GameObject spring; //SPRITE MODE HAS TO BE TILED
        public GameObject wall;

        public float k = 1000;
        public float threshold = 0.05f;
        public float speedThreshold = 0.1f;

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

            float wallLevel = wall.transform.position.x + wall.GetComponent<SpriteRenderer>().bounds.extents.x;
            spring.GetComponent<SpriteRenderer>().size = new Vector2(
                (body.obj.transform.position.x  - body.obj.GetComponent<SpriteRenderer>().bounds.extents.x - wallLevel)
                / spring.transform.localScale.x,
                spring.GetComponent<SpriteRenderer>().size.y);
        }

        public void UpdateForces()
        {
            float m = body.obj.GetComponent<Rigidbody2D>().mass;
            float wallLevel = wall.transform.position.x + wall.GetComponent<SpriteRenderer>().bounds.extents.x;
            float bodyWidth = body.obj.GetComponent<SpriteRenderer>().bounds.size.x;

            float _threshold = m * threshold;

            body.mg = m * Physics.gravity;
            body.N = -body.mg;
            body.Fel = k * -((Vector2)body.obj.transform.position - body.obj.GetComponent<BodyHandler>().startingPos);
            body.Fr = body.friction * body.N.magnitude * -body.obj.GetComponent<Rigidbody2D>().velocity.normalized;

            body.ma = body.mg + body.Fel + body.N + body.Fr;

            if (body.obj.GetComponent<Rigidbody2D>().velocity.magnitude < speedThreshold 
                && body.N.magnitude * body.friction > body.Fel.magnitude - 0.01f)
            {
                body.Fr = -body.Fel;
                body.obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                body.ma = Vector2.zero;
            }

            if (body.obj.transform.position.x - bodyWidth / 2f < wallLevel - 0.001f)
            {
                body.obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                body.obj.transform.position = new Vector2(wallLevel + bodyWidth / 2f, body.obj.transform.position.y);
            }

            if (body.ma.magnitude > _threshold)
            {
                body.obj.GetComponent<Rigidbody2D>().AddForce(body.ma);
            }


            if (body.mg.magnitude < _threshold) body.mgArrow.Disable();
            else body.mgArrow.Enable();

            if (body.Fel.magnitude < _threshold) body.felArrow.Disable();
            else body.felArrow.Enable();

            if (body.Fr.magnitude < _threshold) body.frArrow.Disable();
            else body.frArrow.Enable();

            if (body.N.magnitude < _threshold) body.nArrow.Disable();
            else body.nArrow.Enable();

            if (body.ma.magnitude < _threshold) body.maArrow.Disable();
            else body.maArrow.Enable();

        }

        public void UpdateArrows()
        {
            body.UpdateArrows();
        }

        public void ResetVariables()
        {

        }
    }
}

