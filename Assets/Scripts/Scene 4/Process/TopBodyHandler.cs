using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene4
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TopBodyHandler : MonoBehaviour
    {

        private Rigidbody2D rb;

        public Vector2 startingPos;
        private Vector2 lastPos;

        private Vector2 impulse; //WITHOUT CONSIDERATION FOR MASS (AS IF THE MASS IS 1kg)
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
            if (rb == null)
            {
                lastPos = startingPos;
                rb = GetComponent<Rigidbody2D>();
            }
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            transform.position = lastPos;
            inSimulation = false;
        }

        public void StartSimulation()
        {
            if (rb == null) rb = GetComponent<Rigidbody2D>();
            lastPos = rb.position;
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
