using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcesController : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private ArrowHandler mgArrow;
    [SerializeField] private ArrowHandler nArrow;
    [SerializeField] private ArrowHandler frArrow;
    [SerializeField] private ArrowHandler velocity;
    private Vector2 mg;
    private Vector2 N;
    private Vector2 Fr;

    public float minLen = 1f;
    public float k = 0.012f;
    public float friction = 0f;

    public void OnEnable()
    {
        UpdateForces();
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
        if (N.magnitude * friction >= mg.magnitude * Mathf.Sin(angle) - 0.01f && rb.velocity.magnitude < 0.1f)
        {
            Fr = Vector3.Project(-mg, rb.velocity);
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.AddForce(mg + N + Fr);
            Debug.Log("FORCE ADDED\n");
        }
        //Debug.Log(string.Format("{0:f5} {1:f5} {2:f5}\n", mg, N, Fr));

    }

    public void UpdateVectors()
    {
        mgArrow.arrowLen = minLen + k * mg.magnitude;
        nArrow.arrowLen = minLen + k * N.magnitude;
        frArrow.arrowLen = minLen + k * Fr.magnitude;
        velocity.arrowLen = block.GetComponent<Rigidbody2D>().velocity.magnitude;
    }
}
