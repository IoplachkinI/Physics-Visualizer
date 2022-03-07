using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrictionSliderController : MonoBehaviour
{
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject text;
    [SerializeField] private PhysicsMaterial2D mat;

    public void OnEnable()
    {
        ChangeFriction();
    }

    public void ChangeFriction()
    {
        float fric = slider.GetComponent<Slider>().value / 100f;
        mat.friction = fric;
        text.GetComponent<Text>().text = string.Format(" Œ›‘‘. “–≈Õ»ﬂ: {0:f2}", fric);
    }

}
