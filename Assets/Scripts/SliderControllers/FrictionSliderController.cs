using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrictionSliderController : MonoBehaviour
{
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject text;
    [SerializeField] private ForcesController forces;

    public void OnEnable()
    {
        UpdateFriction();
    }

    public void UpdateFriction()
    {
        float fric = slider.GetComponent<Slider>().value;
        forces.friction = fric;
        text.GetComponent<Text>().text = string.Format(" Œ›‘‘. “–≈Õ»ﬂ: {0:f2}", fric);
    }

}
