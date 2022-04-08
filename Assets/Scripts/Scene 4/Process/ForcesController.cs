using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scene4
{
    public class ForcesController : MonoBehaviour
    {
        [System.Serializable]
        public class TopBody 
        {
            public GameObject obj;

            public Vector2 mg;
            public Vector2 N;
            public Vector2 Fr;
            public Vector2 v;
            public Vector2 ma;

            public ArrowHandler mgArrow;
            public ArrowHandler nArrow;
            public ArrowHandler frArrow;
            public ArrowHandler vArrow;
            public ArrowHandler maArrow;

            public float friction;

        }

        [System.Serializable]
        public class BottomBody
        {
            public GameObject obj;

            public Vector2 mg;
            public Vector2 N;
            public Vector2 P;
            public Vector2 FrPlane;
            public Vector2 FrBody;
            public Vector2 v;
            public Vector2 ma;

            public ArrowHandler mgArrow;
            public ArrowHandler nArrow;
            public ArrowHandler pArrow;
            public ArrowHandler frPlaneArrow;
            public ArrowHandler frBodyArrow;
            public ArrowHandler vArrow;
            public ArrowHandler maArrow;

            public float friction;
        }

        public TopBody topBody = new TopBody();
        public BottomBody bottomBody = new BottomBody();

        public bool stopped { get; set; } = true;
        public float threshold = 0.05f;

        public void UpdateAll()
        {
            UpdateForces();
            UpdateArrows();
        }

        private void OnEnable()
        {
            UpdateAll();
        }

        private void FixedUpdate()
        {
            UpdateForces();
        }

        private void Update()
        {
            UpdateArrows();
        }

        public void UpdateForces()
        {
            Rigidbody2D rb = topBody.obj.GetComponent<Rigidbody2D>();
            float _threshold = threshold * rb.mass;
            topBody.mg = Vector2.down * Mathf.Abs(rb.mass * Physics.gravity.y);
            topBody.N = -Vector3.Project(topBody.mg, topBody.obj.transform.up);
            topBody.Fr = topBody.N.magnitude * topBody.friction 
                * -Vector3.Project(rb.velocity.normalized, topBody.obj.transform.right);

            if (stopped && topBody.obj.GetComponent<TopBodyHandler>().Impulse.magnitude > 0.001f)
                topBody.Fr = topBody.N.magnitude * topBody.friction 
                    * -Vector3.Project(topBody.obj.GetComponent<TopBodyHandler>().Impulse.normalized, topBody.obj.transform.right);

            else if (stopped || rb.velocity.magnitude < 0.2f)
            {
                if ((topBody.N.magnitude * topBody.friction * -topBody.obj.transform.right).magnitude 
                    > Vector3.Project(topBody.mg, topBody.obj.transform.right).magnitude - _threshold)
                {
                    topBody.Fr = -Vector3.Project(topBody.mg, topBody.obj.transform.right);
                    rb.velocity = Vector2.zero;
                }

                else topBody.Fr = topBody.N.magnitude * topBody.friction * -topBody.obj.transform.right;

            }

            if (topBody.mg.magnitude < _threshold) topBody.mgArrow.Disable();
            else topBody.mgArrow.Enable();

            if (topBody.N.magnitude < _threshold) topBody.nArrow.Disable();
            else topBody.nArrow.Enable();

            if (topBody.Fr.magnitude < _threshold) topBody.frArrow.Disable();
            else topBody.frArrow.Enable();

            if ((topBody.mg + topBody.N + topBody.Fr).magnitude < _threshold) topBody.maArrow.Disable();
            else topBody.maArrow.Enable();

            if ((topBody.mg + topBody.N + topBody.Fr).magnitude > _threshold) 
                rb.AddForce(topBody.mg + topBody.N + topBody.Fr);


            rb = bottomBody.obj.GetComponent<Rigidbody2D>();
            _threshold = threshold * rb.mass;
            bottomBody.mg = Vector2.down * Mathf.Abs(rb.mass * Physics.gravity.y);
            bottomBody.P = -topBody.N;
            bottomBody.N = -Vector3.Project(bottomBody.mg + bottomBody.P, bottomBody.obj.transform.up);
            bottomBody.FrPlane = bottomBody.N.magnitude * bottomBody.friction
                * -Vector3.Project(rb.velocity.normalized, bottomBody.obj.transform.right);
            bottomBody.FrBody = -topBody.Fr;

            if (stopped && topBody.obj.GetComponent<TopBodyHandler>().Impulse.magnitude > 0.001f)
                bottomBody.FrPlane = bottomBody.N.magnitude * bottomBody.friction
                    * Vector3.Project(topBody.obj.GetComponent<TopBodyHandler>().Impulse.normalized, bottomBody.obj.transform.right);

            else if (stopped || rb.velocity.magnitude < 0.2f)
            {
                if ((bottomBody.N.magnitude * bottomBody.friction * -bottomBody.obj.transform.right).magnitude
                    > Vector3.Project(bottomBody.FrBody, topBody.obj.transform.right).magnitude - _threshold)
                {
                    bottomBody.FrPlane = -Vector3.Project(bottomBody.FrBody, bottomBody.obj.transform.right);
                    rb.velocity = Vector2.zero;
                }

                else bottomBody.FrPlane = bottomBody.N.magnitude * bottomBody.friction * -bottomBody.obj.transform.right;

            }

            if (bottomBody.mg.magnitude < _threshold) bottomBody.mgArrow.Disable();
            else bottomBody.mgArrow.Enable();

            if (bottomBody.N.magnitude < _threshold) bottomBody.nArrow.Disable();
            else bottomBody.nArrow.Enable();

            if (bottomBody.P.magnitude < _threshold) bottomBody.pArrow.Disable();
            else bottomBody.pArrow.Enable();

            if (bottomBody.FrBody.magnitude < _threshold) bottomBody.frBodyArrow.Disable();
            else bottomBody.frBodyArrow.Enable();

            if (bottomBody.FrPlane.magnitude < _threshold) bottomBody.frPlaneArrow.Disable();
            else bottomBody.frPlaneArrow.Enable();

            if ((bottomBody.mg + bottomBody.N + bottomBody.FrPlane + bottomBody.FrBody + bottomBody.P).magnitude 
                < _threshold) 
                bottomBody.maArrow.Disable();
            else bottomBody.maArrow.Enable();

            if ((topBody.mg + topBody.N + topBody.Fr).magnitude > _threshold) 
                rb.AddForce(topBody.mg + topBody.N + topBody.Fr); ;


        }

        public void UpdateArrows()
        {
            topBody.maArrow.SetVector(topBody.mg + topBody.N + topBody.Fr);
            topBody.mgArrow.SetVector(topBody.mg);
            topBody.nArrow.SetVector(topBody.N);
            topBody.frArrow.SetVector(topBody.Fr);
            if (stopped) topBody.vArrow.SetVector(topBody.obj.GetComponent<TopBodyHandler>().Impulse);
            else topBody.vArrow.SetVector(topBody.obj.GetComponent<Rigidbody2D>().velocity);
        }
    }

}
