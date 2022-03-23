using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrictionSliderController : MonoBehaviour
{
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject text;

    public void OnEnable()
    {
        UpdateFriction();
    }

    public void UpdateFriction()
    {
        float fric = slider.GetComponent<Slider>().value;
        text.GetComponent<Text>().text = string.Format(" Œ›‘‘. “–≈Õ»ﬂ: {0:f2}", fric);
    }

    public float GetFriction()
    {
        return slider.GetComponent<Slider>().value;
    }

}
