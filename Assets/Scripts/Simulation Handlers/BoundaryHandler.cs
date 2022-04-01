using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryHandler : MonoBehaviour
{
    [SerializeField] private string _tag;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag)) collision.gameObject.transform.position = Vector3.zero;
    }

}
