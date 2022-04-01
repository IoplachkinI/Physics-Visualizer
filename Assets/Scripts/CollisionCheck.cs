using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision enter\n");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision stay\n");
    }
}
