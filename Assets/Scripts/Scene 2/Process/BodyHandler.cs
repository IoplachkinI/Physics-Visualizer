using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene2
{
    public class BodyHandler : MonoBehaviour
    {
        [SerializeField] private TextMesh massT, velT;
        [SerializeField] private Text vxT, vyT;
        private bool inSimulation = false;

        [SerializeField] private GameObject plane;

        public float planeHeight;
        public float bodyHeight;
        public float impulseMag = 0f;
        public Vector2 impulseDirRel = Vector2.right;
        public Vector2 impulse = Vector2.zero;

        private float distance = 0f;
        private Vector2 startingPos = Vector2.zero;

        private void Update()
        {
            UpdateText();
            UpdateTextx();
            UpdateTexty();
        }


        public void StopSimulation()
        {
            //(SOMETIMES) The event calls the method earlier than OnEnable and Start so the variables need to be initialized here too
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
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.mass = mass;
            if (mass < 1f) massT.text = string.Format("m={0:f2}êã", mass);
            else massT.text = string.Format("m={0:f0}êã", mass);
        }

        public void UpdateTextx()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (inSimulation) vxT.text = string.Format("Vx = {0:f1} ì/ñ", rb.velocity.x);
            else vxT.text = string.Format("Vx = {0:f1} ì/ñ", impulse.x);
        }
        public void UpdateTexty()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (inSimulation) vyT.text = string.Format("Vy = {0:f1} ì/ñ", rb.velocity.y);
            else vyT.text = string.Format("Vy = {0:f1} ì/ñ", impulse.y);
        }
        public void UpdateText()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (inSimulation) velT.text = string.Format("V={0:f1}ì/ñ", rb.velocity.magnitude);
            else velT.text = string.Format("V={0:f1}ì/ñ", impulse.magnitude);
        }

        public void UpdateImpulse()
        {
            impulse = Quaternion.FromToRotation(Vector2.right, plane.transform.right) * impulseDirRel * impulseMag;
        }

        public void SetDistance(float _distance)
        {
            distance = _distance;
            UpdateStartingPos();
        }

        public void UpdateStartingPos()
        {
            transform.rotation = plane.transform.parent.rotation;
            transform.position = plane.transform.position + plane.transform.parent.up * (planeHeight / 2f + bodyHeight /2f + distance);
            startingPos = transform.position;
        }

    }
}
