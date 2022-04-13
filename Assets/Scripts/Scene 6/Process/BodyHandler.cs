using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scene6
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

        private void Update()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            if (inSimulation) velT.text = string.Format("V={0:f1}Ï/Ò", GetComponent<Rigidbody2D>().velocity.magnitude);
            else velT.text = string.Format("V={0:f1}Ï/Ò", impulse.magnitude);
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

        public void SetMass(float value)
        {
            GetComponent<Rigidbody2D>().mass = value;
            massT.text = string.Format("m={0:f2}Í„", value);
        }

    }
}

