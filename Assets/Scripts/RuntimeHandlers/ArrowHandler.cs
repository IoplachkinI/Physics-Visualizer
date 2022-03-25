using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{
    [SerializeField] private GameObject body;

    [SerializeField] private GameObject head;
    [SerializeField] private List<GameObject> additionalItems;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject target;
    public float threshold = 0f;
    public float maxLen = 12f;
    public float minLen = 1f;
    public float k = 0.1f;

    [HideInInspector]
    public Vector2 vector = Vector2.one;

    private bool isVisible = true;

    public void OnEnable()
    {
        UpdateArrow();
    }

    public void Disable()
    {
        isVisible = false;

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            -100f
            );
        text.transform.position = new Vector3(
            text.transform.position.x,
            text.transform.position.y,
            -100f
            );
        foreach (GameObject item in additionalItems) item.transform.position = new Vector3(
            item.transform.position.x,
            item.transform.position.y,
            -100f
            );
    }

    public void Enable()
    {
        isVisible = true;

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            0f
            );
        text.transform.position = new Vector3(
            text.transform.position.x,
            text.transform.position.y,
            0f
            );
        foreach (GameObject item in additionalItems) item.transform.position = new Vector3(
            item.transform.position.x,
            item.transform.position.y,
            0f
            );
    }

    public void UpdateArrow()
    {
        vector = vector.normalized * minLen + vector * k;
        if (isVisible && vector.magnitude < threshold)
        {
            Disable();
            return;
        }
        if (!isVisible && vector.magnitude > threshold) Enable();
        if (!isVisible) return;

        if (vector.magnitude > maxLen) vector = vector.normalized * maxLen;

        head.transform.localPosition = vector;
        head.transform.rotation = Quaternion.FromToRotation(Vector2.up, vector);

        body.transform.rotation = Quaternion.FromToRotation(Vector2.up, vector);
        body.GetComponent<SpriteRenderer>().size = new Vector2(
            body.GetComponent<SpriteRenderer>().size.x,
            vector.magnitude);

        text.transform.position = (head.transform.position - transform.position)
            * 0.5f + target.transform.position;

    }

    public void Update()
    {
        UpdateArrow();        
    }
}
