using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsHandler : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Block")) collision.gameObject.transform.position = Vector3.zero;
    }

}
