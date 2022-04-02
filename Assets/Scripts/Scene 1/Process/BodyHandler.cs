using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene1
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BodyHandler : MonoBehaviour
    {

        private Rigidbody2D rb;
        private Vector2 impulse; //WITHOUT CONSIDERATION FOR MASS (AS IF THE MASS IS 1kg)
        private Vector2 startingPos;
        private bool inSimulation = false;
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

        [SerializeField] private TextMesh massT, velT;

        public void OnEnable()
        {
            rb = GetComponent<Rigidbody2D>();
            startingPos = rb.transform.position;
        }

        private void Update()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            if (inSimulation) velT.text = string.Format("V={0:f1}ì/ñ", rb.velocity.magnitude);
            else velT.text = string.Format("V={0:f1}ì/ñ", impulse.magnitude);
        }


        public void StopSimulation()
        {
            if (rb == null) rb = GetComponent<Rigidbody2D>();
            if (startingPos == null) startingPos = rb.transform.position;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.position = startingPos;
            inSimulation = false;
        }

        public void StartSimulation()
        {
            if (rb == null) rb = GetComponent<Rigidbody2D>();
            if (startingPos == null) startingPos = rb.transform.position;
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

    }
}
