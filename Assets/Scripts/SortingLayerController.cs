using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerController : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> meshes;

    private void OnEnable()
    {
        foreach(MeshRenderer mesh in meshes)
        {
            mesh.sortingLayerID = SortingLayer.NameToID("Displays");
        }
    }
}
