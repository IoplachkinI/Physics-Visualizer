using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float xOffset = 0f;
    [SerializeField] private float yOffset = 0f;

    [SerializeField] private bool followTargetRotation = false;
    [SerializeField] private bool relativeOffset = true;

    [SerializeField] private bool followXAxis = true;
    [SerializeField] private bool followYAxis = true;
    [SerializeField] private bool followZAxis = false;


    void Update()
    {
        Vector3 tp = target.transform.position;
        if (relativeOffset) tp += target.transform.right * xOffset + target.transform.up * yOffset;
        else tp += new Vector3(xOffset, yOffset, 0);
        Vector3 p = transform.position;
        if (followXAxis) transform.position = new Vector3(tp.x, p.y, p.z);
        p = transform.position;
        if (followYAxis) transform.position = new Vector3(p.x, tp.y, p.z);
        p = transform.position;
        if (followZAxis) transform.position = new Vector3(p.x, p.y, tp.z);

        if (followTargetRotation) transform.rotation = target.transform.rotation;
    }
}
