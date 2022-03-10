using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHeadHandler : MonoBehaviour
{
    [SerializeField] private GameObject bodyParent;

    public void Update()
    {
        transform.localPosition = bodyParent.transform.localScale + (new Vector3(-1, 0, 0)) * bodyParent.transform.localScale.x;
    }
}
