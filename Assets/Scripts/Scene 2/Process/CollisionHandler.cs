using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollisionHandler : MonoBehaviour
{
    public bool tookOff { get; set; } = false;
    public string _tag;
    [SerializeField] private ProcessController pc;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_tag))
        {
            tookOff = true;
            Debug.Log("started!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tookOff && collision.gameObject.CompareTag(_tag))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            pc.PauseNoResume.Invoke();
            Debug.Log("Check (zb)");
        }
    }
}
