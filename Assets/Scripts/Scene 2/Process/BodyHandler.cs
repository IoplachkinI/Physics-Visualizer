using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene2
{
    public class BodyHandler : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField] private TextMesh massT, velT;
        [SerializeField] private Text vxT, vyT;
        private bool inSimulation = false;

        [SerializeField] private Vector2 defStartingPos;
        [SerializeField] private GameObject plane;
        [SerializeField] private ProcessController pc;
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
            UpdateTextx();
            UpdateTexty();
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
            if ((startingPos == rotatedDefStartingPos)
                && (Vector2.Distance(impulseDir, plane.transform.right) < 0.001f
                    || Vector2.Distance(impulseDir, -plane.transform.right) < 0.001f
                    || impulseMag < 0.0001f)
                && (Vector2)plane.transform.right != Vector2.down)
            {
                pc.PauseNoResume.Invoke();
                return;
            }
            if (rb == null) rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.AddForce(impulse * rb.mass, ForceMode2D.Impulse);
            inSimulation = true;
        }

        public void SetMass(float mass)
        {
            rb.mass = mass;
            if (mass < 1f) massT.text = string.Format("m={0:f2}êã", mass);
            else massT.text = string.Format("m={0:f0}êã", mass);
        }

        public void UpdateTextx()
        {
            if (inSimulation) vxT.text = string.Format("Vx = {0:f1} ì/ñ", rb.velocity.x);
            else vxT.text = string.Format("Vx = {0:f1} ì/ñ", impulse.x);
        }
        public void UpdateTexty()
        {
            if (inSimulation) vyT.text = string.Format("Vy = {0:f1} ì/ñ", rb.velocity.y);
            else vyT.text = string.Format("Vy = {0:f1} ì/ñ", impulse.y);
        }
        public void UpdateText()
        {
            if (inSimulation) velT.text = string.Format("V={0:f1}ì/ñ", rb.velocity.magnitude);
            else velT.text = string.Format("V={0:f1}ì/ñ", impulse.magnitude);
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
