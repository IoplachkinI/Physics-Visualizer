using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{
    [SerializeField] private GameObject body;

    //NOT A HEAD PARENT, but a child, the parent is accessed through the child
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject group;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject target;
    public float offset = 0f;
    public bool followTargetRotation = true;

    public void Update()
    {
        GameObject headParent = head.transform.parent.gameObject;
        Vector3 size = body.GetComponent<SpriteRenderer>().size;
        headParent.transform.localPosition = size + new Vector3(-size.x, 0, 0);
        if (followTargetRotation) group.transform.rotation = Quaternion.Euler(0, 0, target.transform.rotation.eulerAngles.z + offset);
        else group.transform.rotation = Quaternion.Euler(0, 0, offset);
        if (size.y < 0f) head.GetComponent<SpriteRenderer>().flipY = true;
        else head.GetComponent<SpriteRenderer>().flipY = false;
        text.transform.position = (headParent.transform.position - group.transform.position) * 0.75f + target.transform.position;
    }
}
