using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{
    [SerializeField] private GameObject body;

    [SerializeField] private GameObject head;
    [SerializeField] private GameObject text;

    public float threshold = 0f;
    public float k = 0.1f;
    public float maxLen = 0f;
    public float minLen = 1f;
    public float textRotation = 0f;
    public float textDistance = 1.5f;

    private bool isVisible = true;
    private Vector2 vector = Vector2.zero;

    public bool IsVisible()
    {
        return isVisible;
    }

    public bool IsMaxed()
    {
        return maxLen - vector.magnitude < 0.001f;
    }

    public void Disable()
    {
        if (!isVisible) return;
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
        if (isVisible) return;
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

    public Vector2 GetVector()
    {
        return vector;
    }

    public void SetVector(Vector2 _vector)
    {
        _vector = _vector.normalized * minLen + _vector * k;
        if (maxLen > 0.01f && _vector.magnitude > maxLen) _vector = _vector.normalized * maxLen;
        vector = _vector;

        if (threshold > 0.0001f && isVisible && _vector.magnitude < minLen + threshold)
        {
            Disable();
            return;
        }
        if (threshold > 0.0001f && !isVisible && _vector.magnitude > minLen + threshold) Enable();
        if (!isVisible) return;

        head.transform.localPosition = _vector;
        head.transform.rotation = Quaternion.FromToRotation(Vector2.up, _vector);

        body.transform.rotation = Quaternion.FromToRotation(Vector2.up, _vector);
        body.GetComponent<SpriteRenderer>().size = new Vector2(
            body.GetComponent<SpriteRenderer>().size.x,
            _vector.magnitude);
        Vector3 right = head.transform.right;
        if (right == Vector3.right && head.transform.up != Vector3.up) right = -right;

        text.transform.localPosition = head.transform.localPosition + 
            Quaternion.Euler(0, 0, textRotation) * new Vector2(head.GetComponent<SpriteRenderer>().size.x * textDistance, 0);

    }


}
