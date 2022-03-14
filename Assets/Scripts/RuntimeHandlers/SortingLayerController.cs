using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerController : MonoBehaviour
{
    [SerializeField] private string sortingLayerName;
    [SerializeField] private int sortingOrder;

    private void OnEnable()
    {
        GetComponent<MeshRenderer>().sortingLayerID = SortingLayer.NameToID(sortingLayerName);
        GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
    }
}
