using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private bool followTargetRotation = false;
    [SerializeField] private bool followZAxis = false;

    void Update()
    {
        Vector3 p = target.transform.position;
        if (followZAxis) transform.position = p;
        else transform.position = new Vector3(p.x, p.y, transform.position.z);

        if (followTargetRotation) transform.rotation = target.transform.rotation;
    }
}
