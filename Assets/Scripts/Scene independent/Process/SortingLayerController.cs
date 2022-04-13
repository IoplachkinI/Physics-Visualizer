using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingLayerController : MonoBehaviour
{
    [SerializeField] private string sortingLayerName;
    [SerializeField] private int sortingOrder;

    private void OnEnable()
    {
        if (TryGetComponent(out MeshRenderer mesh))
        {
            mesh.sortingLayerID = SortingLayer.NameToID(sortingLayerName);
            mesh.sortingOrder = sortingOrder;
        }

    }
}
