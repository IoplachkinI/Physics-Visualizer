using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene8
{
    public class ForcesController : MonoBehaviour
    {
        public int framerate = 60;

        [System.Serializable]
        public class Body
        {
            public GameObject obj;

            [HideInInspector] public Vector2 mg, Fa, Fc, N, ma;

            public ArrowHandler mgArrow, faArrow, nArrow, vArrow, maArrow;

            public void UpdateArrows()
            {
                mgArrow.SetVector(mg);
                faArrow.SetVector(Fa);
                nArrow.SetVector(N);
                vArrow.SetVector(obj.GetComponent<Rigidbody2D>().velocity);
                maArrow.SetVector(ma);
            }

        }

        public Body body = new Body();
        public GameObject water;
        public GameObject plane;

        public float threshold = 0.05f;
        public float speedThreshold = 0.1f;
        public float dragCoefficient = 1f;

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
            float m = body.obj.GetComponent<Rigidbody2D>().mass;
            float waterLevel = water.transform.position.y + water.GetComponent<SpriteRenderer>().bounds.extents.y;
            float groundLevel = plane.transform.position.y + plane.GetComponent<SpriteRenderer>().bounds.extents.y;
            float BodyHeight = body.obj.GetComponent<SpriteRenderer>().bounds.size.y;

            float _threshold = m * threshold;

            body.mg = m * Physics.gravity;
            body.Fc = dragCoefficient * -body.obj.GetComponent<Rigidbody2D>().velocity;
            body.N = Vector2.zero;

            if (body.obj.GetComponent<Rigidbody2D>().velocity.magnitude < speedThreshold) body.Fc = Vector2.zero;

            if (body.obj.transform.position.y > waterLevel
                && body.obj.transform.position.y - BodyHeight / 2f < waterLevel)
            {
                body.Fa = -Physics.gravity * 1000
                    * (waterLevel - (body.obj.transform.position.y - BodyHeight / 2f))
                    / BodyHeight;
            }

            else if (body.obj.transform.position.y <= waterLevel
                && body.obj.transform.position.y + BodyHeight / 2f > waterLevel)
            {
                body.Fa = -Physics.gravity * 1000
                    * (waterLevel - (body.obj.transform.position.y - BodyHeight / 2f))
                    / BodyHeight;
            }

            else if (body.obj.transform.position.y < waterLevel)
            {
                body.Fa = -Physics.gravity * 1000;
            }

            else
            {
                body.Fc = Vector2.zero;
                body.Fa = Vector2.zero;
            }

            body.ma = body.mg + body.Fc + body.Fa;

            if (body.obj.transform.position.y - BodyHeight / 2f < groundLevel - 0.001f)
            {
                body.obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                body.obj.transform.position = new Vector2(body.obj.transform.position.x, groundLevel + BodyHeight / 2f);
            }

            if (body.obj.transform.position.y - BodyHeight / 2f < groundLevel + 0.001f
                && (body.ma.normalized - Vector2.down).magnitude < 0.001f)
            {
                body.N = -body.ma;
                body.ma = Vector2.zero;
            }

            if (body.ma.magnitude > _threshold)
            {
                body.obj.GetComponent<Rigidbody2D>().AddForce(body.ma);
            }


            if (body.mg.magnitude < _threshold) body.mgArrow.Disable();
            else body.mgArrow.Enable();

            if (body.Fa.magnitude < _threshold) body.faArrow.Disable();
            else body.faArrow.Enable();

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

