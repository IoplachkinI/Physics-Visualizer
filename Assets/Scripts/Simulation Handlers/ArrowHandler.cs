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
    public float k = 0.1f;
    public float maxLen = 0f;
    public float minLen = 1f;

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
        if (vector.magnitude > maxLen && maxLen > 0.001f) vector = vector.normalized * maxLen;

        if (isVisible && vector.magnitude < minLen + threshold)
        {
            Disable();
            return;
        }
        if (!isVisible && vector.magnitude > minLen + threshold) Enable();
        if (!isVisible) return;

        head.transform.localPosition = vector;
        head.transform.rotation = Quaternion.FromToRotation(Vector2.up, vector);

        body.transform.rotation = Quaternion.FromToRotation(Vector2.up, vector);
        body.GetComponent<SpriteRenderer>().size = new Vector2(
            body.GetComponent<SpriteRenderer>().size.x,
            vector.magnitude);

        text.transform.position = (transform.position + head.transform.position) / 2f;

    }

    public void Update()
    {
        UpdateArrow();        
    }
}
