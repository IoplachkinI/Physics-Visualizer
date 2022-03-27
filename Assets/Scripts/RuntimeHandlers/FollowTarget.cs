using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool followTargetRotation = false;
    [SerializeField] private bool followZAxis = false;
    [SerializeField] private bool offsetRelative = true;
    [SerializeField] private float xOffset = 0;
    [SerializeField] private float yOffset = 0;

    void Update()
    {
        Vector3 p = target.transform.position;
        if (followZAxis) transform.position = p;
        else transform.position = new Vector3(p.x, p.y, transform.position.z);
        if (offsetRelative)
        {
            transform.position += target.transform.right * xOffset + target.transform.up * yOffset;
        }
        else transform.position += new Vector3(xOffset, yOffset, 0);

        if (followTargetRotation) transform.rotation = target.transform.rotation;
    }
}
