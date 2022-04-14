using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scene8
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BodyHandler : MonoBehaviour
    {
        public Vector2 defStartingPos;
        public Vector2 startingPos;

        [SerializeField] private TextMesh massT, velT;

        private void Update()
        {
            UpdateText();
        }

        private void OnEnable()
        {
            startingPos = defStartingPos;
            transform.position = startingPos;
        }

        public void UpdateText()
        {
            velT.text = string.Format("V={0:f1}Ï/Ò", GetComponent<Rigidbody2D>().velocity.magnitude);
        }


        public void StopSimulation()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            transform.position = startingPos;
        }

        public void StartSimulation()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
        }

        public void SetMass(float value)
        {
            GetComponent<Rigidbody2D>().mass = value;
            massT.text = string.Format("m={0:f0}Í„", value);
        }


        public void SetHeight(float value)
        {
            startingPos = defStartingPos + new Vector2(0, value);
            transform.position = startingPos;
        }

    }
}


