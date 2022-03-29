using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcesController : MonoBehaviour
{
    [SerializeField] private ButtonController bc;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private ArrowHandler mgArrow;
    [SerializeField] private ArrowHandler nArrow;
    [SerializeField] private ArrowHandler frArrow;
    [SerializeField] private ArrowHandler vArrow;
    [SerializeField] private ArrowHandler maArrow;
    private Vector2 mg = Vector2.one;
    private Vector2 N = Vector2.one;
    private Vector2 Fr = Vector2.one;

    public float friction = 0f;

    public void OnEnable()
    {
        UpdateForces();
    }

    public void FixedUpdate()
    {
        UpdateForces();
    }

    private void UpdateForces()
    {
        float angle = -Mathf.Deg2Rad * body.transform.rotation.eulerAngles.z;
        mg = Vector2.down * Mathf.Abs(body.mass * Physics.gravity.y);
        N = mg.magnitude * Mathf.Cos(angle) * body.transform.up.normalized;
        Fr = N.magnitude * friction * Vector3.Project(-body.velocity.normalized, body.transform.right);

        if (N.magnitude * friction >= mg.magnitude * Mathf.Sin(angle) - 0.01f && body.velocity.magnitude < 0.2005f)
        {
            Fr = -Vector3.Project(mg, body.transform.right);
            body.velocity = Vector2.zero;
        }
        else
        {
            body.AddForce(mg + N + Fr);
        }

        maArrow.vector = mg;
        mgArrow.vector = mg;
        nArrow.vector = N;
        frArrow.vector = Fr;
        vArrow.vector = body.GetComponent<Rigidbody2D>().velocity;

    }
}
