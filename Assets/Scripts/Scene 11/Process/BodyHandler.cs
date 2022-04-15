using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scene11
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BodyHandler : MonoBehaviour
    {
        public Vector2 startingPos;
        public Vector2 offsetStartingPos;
        public float offsetPercent;
        public GameObject wall;

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
            transform.position = offsetStartingPos;
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


        public void SetOffset(float value)
        {
            float halfBodyWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
            float wallLevel = wall.transform.position.x + wall.GetComponent<SpriteRenderer>().bounds.extents.x;
            offsetPercent = value;
            offsetStartingPos = new Vector2((startingPos.x - halfBodyWidth - wallLevel) * (1f + offsetPercent)
                + halfBodyWidth + wallLevel, startingPos.y);
            transform.position = offsetStartingPos;
        }


        public void SetLength(float value)
        {
            float halfBodyWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
            float wallLevel = wall.transform.position.x + wall.GetComponent<SpriteRenderer>().bounds.extents.x;
            startingPos = new Vector2(wallLevel + value + halfBodyWidth, transform.position.y);
            offsetStartingPos = new Vector2((startingPos.x - halfBodyWidth - wallLevel) * (1f + offsetPercent)
                + halfBodyWidth + wallLevel, startingPos.y);
            transform.position = offsetStartingPos;

        }

    }
}



