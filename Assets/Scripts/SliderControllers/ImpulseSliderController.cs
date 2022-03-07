using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImpulseUpSliderController : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject slider;

    public void ChangeImpulse()
    {
        block.GetComponent<Rigidbody2D>().AddForce(slider.GetComponent<Slider>().value * (-block.transform.forward), ForceMode2D.Impulse);
    }
}
