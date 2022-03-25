using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcesController : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private ArrowHandler mgArrow;
    [SerializeField] private ArrowHandler nArrow;
    [SerializeField] private ArrowHandler frArrow;
    [SerializeField] private ArrowHandler vArrow;
    private Vector2 mg = Vector2.one;
    private Vector2 N = Vector2.one;
    private Vector2 Fr = Vector2.one;

    public float friction = 0f;

    public void OnEnable()
    {
        UpdateForces();
        UpdateVectors();
    }

    public void FixedUpdate()
    {
        UpdateForces();
    }

    public void Update()
    {
        UpdateVectors();   
    }

    private void UpdateForces()
    {
        Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
        float angle = -Mathf.Deg2Rad * block.transform.rotation.eulerAngles.z;
        mg = Vector2.down * Mathf.Abs(rb.mass * Physics.gravity.y);
        N = mg.magnitude * Mathf.Cos(angle) * block.transform.up.normalized;
        Fr = N.magnitude * friction * Vector3.Project(-rb.velocity.normalized, block.transform.right);

        if (N.magnitude * friction >= mg.magnitude * Mathf.Sin(angle) - 0.01f && rb.velocity.magnitude < 0.2005f)
        {
            Fr = -Vector3.Project(mg, block.transform.right);
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.AddForce(mg + N + Fr);
        }

    }

    public void UpdateVectors()
    {
        mgArrow.vector = mg;
        nArrow.vector = N;
        frArrow.vector = Fr;
        vArrow.vector = block.GetComponent<Rigidbody2D>().velocity;
    }
}
