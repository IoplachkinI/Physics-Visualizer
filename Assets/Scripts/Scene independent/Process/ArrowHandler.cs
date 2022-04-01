using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{
    [SerializeField] private GameObject body;

    [SerializeField] private GameObject head;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject target;

    public float threshold = 0f;
    public float k = 0.1f;
    public float maxLen = 0f;
    public float minLen = 1f;
    public float textRotation = 0f;
    public float textDistance = 1.5f;

    private bool isVisible = true;

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
    }

    public void UpdateArrow(Vector2 vector)
    {
        vector = vector.normalized * minLen + vector * k;
        if (maxLen > 0.01f && vector.magnitude > maxLen) vector = vector.normalized * maxLen;

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

        text.transform.localPosition = head.transform.localPosition + 
            Quaternion.Euler(0, 0, textRotation) * (head.GetComponent<SpriteRenderer>().size.x * textDistance * head.transform.right);

    }


}
