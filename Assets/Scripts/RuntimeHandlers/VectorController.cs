using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorController : MonoBehaviour
{
    private float EPSILON = 0.00001f;

    [SerializeField] private GameObject block;
    [SerializeField] private SpriteRenderer gravity;
    [SerializeField] private SpriteRenderer reaction;
    [SerializeField] private SpriteRenderer friction;
    [SerializeField] private SpriteRenderer velocity;

    [SerializeField] private List<GameObject> reactionGroup;
    [SerializeField] private List<GameObject> frictionGroup;

    public float minLen = 3f;
    public float k = 0.012f;

    private void OnEnable()
    {
        UpdateForces();
    }

    public void Update()
    {
        UpdateForces();
    }

    public void UpdateForces()
    {
        Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
        float mg = Mathf.Abs(rb.mass * Physics.gravity.y);
        float N = mg * Mathf.Cos(Mathf.Deg2Rad * block.transform.rotation.eulerAngles.z);
        float fr = N * rb.sharedMaterial.friction;
        float v = (rb.velocity.y < -EPSILON) ? rb.velocity.magnitude : - rb.velocity.magnitude;
        v = (v > 100f + EPSILON) ? 100f : v;
        v = (v < -100f - EPSILON) ? -100f : v;

        if (N <= EPSILON) foreach (GameObject f in reactionGroup) f.SetActive(false);
        else foreach (GameObject f in reactionGroup) f.SetActive(true);

        if (fr <= EPSILON) foreach (GameObject f in frictionGroup) f.SetActive(false);
        else foreach (GameObject f in frictionGroup) f.SetActive(true);

        gravity.size = new Vector2(gravity.size.x, minLen + mg * k);
        reaction.size = new Vector2(reaction.size.x, minLen + N * k);
        friction.size = new Vector2(friction.size.x, minLen + fr * k);
        velocity.size = new Vector2(velocity.size.x, v * k * 10f);

    }
}
