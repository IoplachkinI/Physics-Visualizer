using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene6
{
    public class RopeHandler : MonoBehaviour
    {
        [SerializeField] private GameObject body;

        private void Update()
        {
            LineRenderer lr = GetComponent<LineRenderer>();
            lr.SetPosition(1, new Vector3(lr.GetPosition(0).x, body.transform.position.y, 0));
        }
    }
}
