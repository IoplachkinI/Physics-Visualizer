using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene2
{
    public class BodyHandler : MonoBehaviour
    {
        private Rigidbody2D rb;

        private Vector2 impulse;
        [SerializeField] private Vector2 startingPos;
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

        [SerializeField] private TextMesh massT, velT, velxT, velyT;
        private bool inSimulation = false;

        public void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            UpdateText();
        }


        public void StopSimulation()
        {
            if (rb == null) rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.position = startingPos;
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
            if (mass < 1f) massT.text = string.Format("m={0:f2}êã", mass);
            else massT.text = string.Format("m={0:f0}êã", mass);
        }

        public void UpdateTextx()
        {
            if (inSimulation) velxT.text = string.Format("V={0:f1}ì/ñ", Mathf.Abs(rb.velocity.x));
            else velxT.text = string.Format("V={0:f1}ì/ñ", Mathf.Abs(impulse.x));
        }
        public void UpdateTexty()
        {
            if (inSimulation) velyT.text = string.Format("V={0:f1}ì/ñ", Mathf.Abs(rb.velocity.y));
            else velyT.text = string.Format("V={0:f1}ì/ñ", Mathf.Abs(impulse.y));
        }
        public void UpdateText()
        {
            if (inSimulation) velT.text = string.Format("V={0:f1}ì/ñ", rb.velocity.magnitude);
            else velT.text = string.Format("V={0:f1}ì/ñ", impulse.magnitude);
        }



    }
}
