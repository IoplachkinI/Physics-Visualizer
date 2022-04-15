using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene7
{
    public class ObjectsHandler : MonoBehaviour
    {
        [SerializeField] private GameObject body, pulley, plane;
        [SerializeField] private LineRenderer left, mid, right;
        [SerializeField] private GameObject fArrow;
        [SerializeField] private Vector2 dist;

        private void OnEnable()
        {
            dist = pulley.transform.position - body.transform.position;
        }

        public void SetHighestPulleyPos()
        {
            float ceilLevel = plane.transform.position.y - plane.GetComponent<SpriteRenderer>().bounds.extents.y;
            float pulleyHeight = pulley.GetComponent<SpriteRenderer>().bounds.size.y;
            body.transform.position = new Vector3(0, ceilLevel, 0)
                - new Vector3(0, pulleyHeight / 2f, 0)
                - (mid.GetPosition(0) - mid.GetPosition(1));
            Update();
        }

        public bool PulleyInBounds()
        {
            float pulleyHeight = pulley.GetComponent<SpriteRenderer>().bounds.size.y;
            float ceilLevel = plane.transform.position.y - plane.GetComponent<SpriteRenderer>().bounds.extents.y;
            return (pulley.transform.position.y < ceilLevel - pulleyHeight / 2f - 0.001f);
        }

        private void Update()
        {
            pulley.transform.position = body.transform.position + (Vector3)dist;

            left.SetPosition(0, new Vector3(left.GetPosition(0).x, pulley.transform.position.y, left.GetPosition(0).z));

            right.SetPosition(1, right.GetPosition(1)
                + new Vector3(right.GetPosition(0).x, pulley.transform.position.y, right.GetPosition(0).z) 
                - right.GetPosition(0));
            right.SetPosition(0, new Vector3(right.GetPosition(0).x, pulley.transform.position.y, right.GetPosition(0).z));

            mid.SetPosition(0, pulley.transform.position);
            mid.SetPosition(1, body.transform.position);

            fArrow.transform.position = new Vector3(right.GetPosition(1).x, right.GetPosition(1).y, fArrow.transform.position.z);
        }
    }
}
