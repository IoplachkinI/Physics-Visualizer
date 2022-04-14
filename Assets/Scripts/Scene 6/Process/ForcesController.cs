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
                vArrow.SetVector(obj.GetComponent<Rigidbody2D>().velocity);
                maArrow.SetVector(ma);
            }

        }

        public Body leftBody = new Body();
        public Body rightBody = new Body();

        public float threshold = 0.05f;
        public float speedThreshold = 0.2f;
        public float heightThreshold;

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
            float m1 = leftBody.obj.GetComponent<Rigidbody2D>().mass;
            float m2 = rightBody.obj.GetComponent<Rigidbody2D>().mass;

            float _heightThreshold = heightThreshold * (1f - rightBody.obj.GetComponent<Rigidbody2D>().velocity.magnitude / 30f);

            float _threshold1 = m1 * threshold;
            float _threshold2 = m2 * threshold;

            ProcessController pc = GetComponent<ProcessController>();


            if (leftBody.obj.transform.position.y > _heightThreshold ||
                rightBody.obj.transform.position.y > _heightThreshold)
                pc.PauseNoResume.Invoke();


            leftBody.mg = m1 * Physics.gravity;
            leftBody.T = -m2 * Physics.gravity;

            leftBody.ma = leftBody.mg + leftBody.T;


            rightBody.mg = m2 * Physics.gravity;
            rightBody.T = -m1 * Physics.gravity;

            rightBody.ma = rightBody.mg + rightBody.T;


            if (leftBody.ma.magnitude > _threshold1)
            {
                leftBody.obj.GetComponent<Rigidbody2D>().AddForce(leftBody.ma);
            }

            if (rightBody.ma.magnitude > _threshold2)
            {
                rightBody.obj.GetComponent<Rigidbody2D>().AddForce(rightBody.ma);
            }


            if (leftBody.mg.magnitude < _threshold1) leftBody.mgArrow.Disable();
            else leftBody.mgArrow.Enable();

            if (leftBody.T.magnitude < _threshold1) leftBody.tArrow.Disable();
            else leftBody.tArrow.Enable();

            if (leftBody.ma.magnitude < _threshold1) leftBody.maArrow.Disable();
            else leftBody.maArrow.Enable();



            if (rightBody.mg.magnitude < _threshold2) rightBody.mgArrow.Disable();
            else rightBody.mgArrow.Enable();

            if (rightBody.T.magnitude < _threshold2) rightBody.tArrow.Disable();
            else rightBody.tArrow.Enable();

            if (rightBody.ma.magnitude < _threshold2) rightBody.maArrow.Disable();
            else rightBody.maArrow.Enable();
        
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

