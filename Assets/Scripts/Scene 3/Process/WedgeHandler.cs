using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene3
{
    public class WedgeHandler : MonoBehaviour
{

        public Vector2 startingPos;

        [SerializeField] private TextMesh velT, massT;

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

        public void SetMass(float mass)
        {
            GetComponent<Rigidbody2D>().mass = mass;
            if (mass < 1f) massT.text = string.Format("M={0:f2}êã", mass);
            else massT.text = string.Format("M={0:f0}êã", mass);
        }

        public void SetAngle (float angle)
        {
            transform.localScale = new Vector2(transform.localScale.y / Mathf.Tan(Mathf.Deg2Rad * angle), transform.localScale.y);
        }

        public void UpdateText()
        {
            velT.text = string.Format("V={0:f1}ì/ñ", GetComponent<Rigidbody2D>().velocity.magnitude);
        }
    }
}
