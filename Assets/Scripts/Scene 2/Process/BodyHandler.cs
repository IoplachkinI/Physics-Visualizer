using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene2
{
    public class BodyHandler : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField] private TextMesh massT, velT, velxT, velyT;
        private bool inSimulation = false;

        [SerializeField] private Vector2 defStartingPos;
        private Vector2 rotatedDefStartingPos;
        public Vector2 startingPos;

        private Vector2 impulse; //WITHOUT CONSIDERATION FOR MASS

        private float impulseMag = 0f;
        private Vector2 impulseDir = Vector2.right;
        private Vector2 impulseDirRel = Vector2.right;

        public float ImpulseMag
        {
            get
            {
                return impulseMag;
            }
            set
            {
                impulseMag = value;
                impulse = impulseDir * impulseMag;
                Update();
            }
        }

        public Vector2 ImpulseDirRel
        {
            get
            {
                return impulseDirRel;
            }
            set
            {
                impulseDirRel = value.normalized;
                Update();
            }
        }

        public Vector2 ImpulseDir
        {
            get
            {
                return impulseDir;
            }
            set
            {
                impulseDir = value.normalized;
                impulse = impulseDir * impulseMag;
                Update();
            }
        }
        public Vector2 Impulse
        {
            get
            {
                return impulse;
            }
            set
            {
                impulse = value;
                Update();
            }
        }

        public Vector2 StartingPos
        {
            get
            {
                return startingPos;
            }
            set
            {
                startingPos = value;
                transform.position = startingPos;
            }
        }

        public void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();
            startingPos = defStartingPos;
            rotatedDefStartingPos = defStartingPos;
        }

        private void Update()
        {
            UpdateText();
        }


        public void StopSimulation()
        {
            //(SOMETIMES) The event calls the method earlier than OnEnable and Start so the variables need to be initialized here too
            if (rb == null) 
            {
                rb = GetComponent<Rigidbody2D>();
                startingPos = defStartingPos;
                rotatedDefStartingPos = defStartingPos;
            }
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            transform.position = startingPos;
            inSimulation = false;
        }

        public void StartSimulation()
        {
            if (rb == null) rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.AddForce(impulse * rb.mass, ForceMode2D.Impulse);
            inSimulation = true;
        }

        public void SetMass(float mass)
        {
            rb.mass = mass;
            if (mass < 1f) massT.text = string.Format("m={0:f2}��", mass);
            else massT.text = string.Format("m={0:f0}��", mass);
        }

        public void UpdateTextx()
        {
            if (inSimulation) velxT.text = string.Format("V={0:f1}�/�", Mathf.Abs(rb.velocity.x));
            else velxT.text = string.Format("V={0:f1}�/�", Mathf.Abs(impulse.x));
        }
        public void UpdateTexty()
        {
            if (inSimulation) velyT.text = string.Format("V={0:f1}�/�", Mathf.Abs(rb.velocity.y));
            else velyT.text = string.Format("V={0:f1}�/�", Mathf.Abs(impulse.y));
        }
        public void UpdateText()
        {
            if (inSimulation) velT.text = string.Format("V={0:f1}�/�", rb.velocity.magnitude);
            else velT.text = string.Format("V={0:f1}�/�", impulse.magnitude);
        }

        public Vector2 GetRotatedDefStartingPos()
        {
            return rotatedDefStartingPos;
        }

        public void SetRotatedDefStartingPos(Quaternion rotation)
        {
            Vector2 diff = startingPos - rotatedDefStartingPos;
            rotatedDefStartingPos = rotation * defStartingPos;
            StartingPos = rotatedDefStartingPos + diff;
        }

    }
}
