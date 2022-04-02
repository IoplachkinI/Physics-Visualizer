using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scene2
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private ProcessController pc;
        [SerializeField] private string _tag;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_tag)) pc.PauseNoResume.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(_tag)) pc.PauseNoResume.Invoke();
        }
    }
}
