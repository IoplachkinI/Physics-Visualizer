using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcesController : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private ArrowHandler mgArrow;
    [SerializeField] private ArrowHandler nArrow;
    [SerializeField] private ArrowHandler frArrow;
    [SerializeField] private ArrowHandler vArrow;
    [SerializeField] private ArrowHandler maArrow;
    private Vector2 mg = Vector2.one;
    private Vector2 N = Vector2.one;
    private Vector2 Fr = Vector2.one;

    public bool stopped { get; set; } = true;
    public float friction = 0f;

    public void UpdateAll()
    {
        UpdateForces();
        UpdateArrows();
    }

    private void OnEnable()
    {
        UpdateAll();
    }

    private void FixedUpdate()
    {
        UpdateForces();
    }

    private void Update()
    {
        UpdateArrows();
    }

    public void UpdateForces()
    {
        Rigidbody2D rb = body.GetComponent<Rigidbody2D>();
        mg = Vector2.down * Mathf.Abs(rb.mass * Physics.gravity.y);
        N = -Vector3.Project(mg, body.transform.up);
        Fr = N.magnitude * friction * -Vector3.Project(rb.velocity.normalized, body.transform.right);
        
        if (stopped && body.GetComponent<BodyHandler>().Impulse.magnitude > 0.001f)
            Fr = N.magnitude * friction * -Vector3.Project(body.GetComponent<BodyHandler>().Impulse.normalized, body.transform.right);

        else if (stopped || rb.velocity.magnitude < 0.2f)
        {
            if (N.magnitude * friction > Vector3.Project(mg, body.transform.right).magnitude - 0.001f)
            {
                Fr = -Vector3.Project(mg, body.transform.right);
                rb.velocity = Vector2.zero;
            }

            else Fr = N.magnitude * friction * -body.transform.right;
        }

        if ((mg + N + Fr).magnitude / rb.mass < 0.01f)
        {
            Debug.Log("No forces?");
            return;
        }
        rb.AddForce(mg + N + Fr);
    }

    public void UpdateArrows()
    {
        maArrow.UpdateArrow(mg + N + Fr);
        mgArrow.UpdateArrow(mg);
        nArrow.UpdateArrow(N);
        frArrow.UpdateArrow(Fr);
        if (stopped) vArrow.UpdateArrow(body.GetComponent<BodyHandler>().Impulse);
        else vArrow.UpdateArrow(body.GetComponent<Rigidbody2D>().velocity);
    }
}
