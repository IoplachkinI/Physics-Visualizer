using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene3
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BodyHandler : MonoBehaviour
    {
        public Vector2 startingPos;

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

        }

        private void Update()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            if (inSimulation) velT.text = string.Format("V={0:f1}�/�", GetComponent<Rigidbody2D>().velocity.magnitude);
            else velT.text = string.Format("V={0:f1}�/�", impulse.magnitude);
        }


        public void StopSimulation()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            transform.position = startingPos;
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
            GetComponent<Rigidbody2D>().mass = mass;
            if (mass < 1f) massT.text = string.Format("m={0:f2}��", mass);
            else massT.text = string.Format("m={0:f0}��", mass);
        }

        public void Rotate(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

    }
}
