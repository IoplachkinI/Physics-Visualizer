using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{
    [SerializeField] private GameObject body;

    //NOT A HEAD PARENT, but a child, the parent is accessed through the child
    [SerializeField] private GameObject head;
    [SerializeField] private List<GameObject> additionalItems;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject target;
    public float offset = 0f;
    public float threshold = 0f;
    private float EPSILON = 0.00001f;
    public bool followTargetRotation = true;

    public void OnEnable()
    {
        UpdateArrows();
    }

    public void UpdateArrows()
    {
        Vector3 size = body.GetComponent<SpriteRenderer>().size;

        if (Mathf.Abs(size.y) < threshold + EPSILON)
        {
            gameObject.SetActive(false);
            text.SetActive(false);
            foreach (GameObject item in additionalItems) item.SetActive(false);
            return;
        }

        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            text.SetActive(true);
            foreach (GameObject item in additionalItems) item.SetActive(true);
        }

        head.transform.localPosition = size + new Vector3(-size.x, 0, 0);

        if (followTargetRotation) transform.rotation = Quaternion.Euler(0, 0, target.transform.rotation.eulerAngles.z + offset);
        else transform.rotation = Quaternion.Euler(0, 0, offset);

        if (size.y < 0f) head.GetComponent<SpriteRenderer>().flipY = true;
        else head.GetComponent<SpriteRenderer>().flipY = false;

        text.transform.position = (head.transform.position - transform.position)
            * 0.5f + target.transform.position;

    }

    public void Update()
    {
        UpdateArrows();        
    }
}
