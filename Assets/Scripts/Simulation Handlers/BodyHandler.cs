using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BodyHandler : MonoBehaviour
{

    private Rigidbody2D rb;

    public void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StopSimulation()
    {
        rb.position = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    public void StartSimulation(Vector2 impulse)
    {
        rb.AddForce(impulse);
    }

    public void UpdateMass(int mass)
    {
        rb.mass = mass;
    }

}
