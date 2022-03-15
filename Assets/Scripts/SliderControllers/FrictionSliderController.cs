using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrictionSliderController : MonoBehaviour
{
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject block;

    public void OnEnable()
    {
        UpdateFriction();
    }

    public void UpdateFriction()
    {
        float fric = slider.GetComponent<Slider>().value;
        block.GetComponent<Collider2D>().sharedMaterial.friction = fric;
        block.GetComponent<Rigidbody2D>().sharedMaterial.friction = fric;
        text.GetComponent<Text>().text = string.Format(" Œ›‘‘. “–≈Õ»ﬂ: {0:f2}", fric);
    }

}
