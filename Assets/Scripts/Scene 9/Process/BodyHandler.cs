using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scene9
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BodyHandler : MonoBehaviour
    {
        public Vector2 startingPos;
        public GameObject plane;

        [SerializeField] private TextMesh massT, velT;

        private void Update()
        {
            UpdateText();
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


        public void SetLength(float value)
        {
            float halfBodyHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2f;
            float groundLevel = plane.transform.position.y + plane.GetComponent<SpriteRenderer>().bounds.extents.y;
            startingPos = new Vector2(transform.position.x, groundLevel + value + halfBodyHeight);
            transform.position = startingPos;
        }

    }
}



