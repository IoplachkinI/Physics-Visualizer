using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDisplayController : MonoBehaviour
{
    [SerializeField] private GameObject velocityText;
    [SerializeField] private GameObject block;

    private void Update()
    {
        velocityText.GetComponent<TextMesh>().text = string.Format("V={0:f1}ì/ñ", block.GetComponent<Rigidbody2D>().velocity.magnitude);        
    }
}
