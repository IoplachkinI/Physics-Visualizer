using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene7
{
    public class ForcesController : MonoBehaviour
    {
        public int framerate = 60;

        [HideInInspector] public Vector2 F;
        public ArrowHandler fArrow;

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
                vArrow.SetVector(obj.GetComponent<Rigidbody2D>().velocity);
                maArrow.SetVector(ma);
            }

        }

        public Body body = new Body();

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
            float m = body.obj.GetComponent<Rigidbody2D>().mass;

            float _threshold = m * threshold;

            ProcessController pc = GetComponent<ProcessController>();
            ObjectsHandler oh = GetComponent<ObjectsHandler>();


            if (pc.GetState() == ProcessController.State.Running && !oh.PulleyInBounds())
            {
                oh.SetHighestPulleyPos();
                pc.PauseNoResume.Invoke();
            }


            body.mg = m * Physics.gravity;
            body.T = 2 * F;

            body.ma = body.mg + body.T;


            if (body.ma.magnitude > _threshold)
            {
                body.obj.GetComponent<Rigidbody2D>().AddForce(body.ma);
            }


            if (body.mg.magnitude < _threshold) body.mgArrow.Disable();
            else body.mgArrow.Enable();

            if (body.T.magnitude < _threshold) body.tArrow.Disable();
            else body.tArrow.Enable();

            if (F.magnitude < _threshold) fArrow.Disable();
            else fArrow.Enable();

            if (body.ma.magnitude < _threshold) body.maArrow.Disable();
            else body.maArrow.Enable();


        }

        public void UpdateArrows()
        {
            body.UpdateArrows();
            fArrow.SetVector(F);
        }

        public void ResetVariables()
        {

        }
    }
}

