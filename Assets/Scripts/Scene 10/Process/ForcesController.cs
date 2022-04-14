using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene10
{
    public class ForcesController : MonoBehaviour
    {
        public int framerate = 60;

        [System.Serializable]
        public class Body
        {
            public GameObject obj;

            [HideInInspector] public Vector2 mg, Fel, N, ma;

            public ArrowHandler mgArrow, felArrow, nArrow, vArrow, maArrow;

            public void UpdateArrows()
            {
                mgArrow.SetVector(mg);
                felArrow.SetVector(Fel);
                nArrow.SetVector(N);
                vArrow.SetVector(obj.GetComponent<Rigidbody2D>().velocity);
                maArrow.SetVector(ma);
            }

        }

        public Body body = new Body();
        public GameObject spring; //SPRITE MODE HAS TO BE TILED
        public GameObject plane;

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

            float groundLevel = plane.transform.position.y + plane.GetComponent<SpriteRenderer>().bounds.extents.y;
            spring.GetComponent<SpriteRenderer>().size = new Vector2(
                (body.obj.transform.position.y - body.obj.GetComponent<SpriteRenderer>().bounds.extents.y - groundLevel)
                / spring.transform.localScale.x,
                spring.GetComponent<SpriteRenderer>().size.y);
        }

        public void UpdateForces()
        {
            float m = body.obj.GetComponent<Rigidbody2D>().mass;
            float groundLevel = plane.transform.position.y + plane.GetComponent<SpriteRenderer>().bounds.extents.y;
            float bodyHeight = body.obj.GetComponent<SpriteRenderer>().bounds.size.y;

            float _threshold = m * threshold;

            body.mg = m * Physics.gravity;
            body.N = Vector2.zero;
            body.Fel = k * -((Vector2)body.obj.transform.position - body.obj.GetComponent<BodyHandler>().startingPos);

            body.ma = body.mg + body.Fel;

            if (body.obj.transform.position.y - bodyHeight / 2f < groundLevel - 0.001f)
            {
                body.obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                body.obj.transform.position = new Vector2(body.obj.transform.position.x, groundLevel + bodyHeight / 2f);
            }

            if (body.obj.transform.position.y - bodyHeight / 2f < groundLevel + 0.001f
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

            if (body.Fel.magnitude < _threshold) body.felArrow.Disable();
            else body.felArrow.Enable();

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
