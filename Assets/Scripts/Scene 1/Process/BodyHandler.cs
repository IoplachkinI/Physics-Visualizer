using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene1
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BodyHandler : MonoBehaviour
    {
        public float planeHeight;
        public float bodyHeight;

        [SerializeField] private GameObject plane;

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

        private void Update()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (inSimulation) velT.text = string.Format("V={0:f1}ì/ñ", rb.velocity.magnitude);
            else velT.text = string.Format("V={0:f1}ì/ñ", impulse.magnitude);
        }


        public void StopSimulation()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            transform.position = plane.transform.position + plane.transform.parent.up * (planeHeight / 2f + bodyHeight / 2f);
            inSimulation = false;
        }

        public void StartSimulation()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.AddForce(impulse * rb.mass, ForceMode2D.Impulse);
            inSimulation = true;
        }

        public void SetMass(float mass)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.mass = mass;
            if (mass < 1f) massT.text = string.Format("m={0:f2}êã", mass);
            else massT.text = string.Format("m={0:f0}êã", mass);
        }

    }
}
