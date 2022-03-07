using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImpulseSliderController : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject sliderUp;
    [SerializeField] private GameObject sliderDown;
    public Vector2 forceUp;
    public Vector2 forceDown;

    public void ChangeImpulse()
    {
        //block.GetComponent<Rigidbody2D>().AddForce(slider.GetComponent<Slider>().value * (-block.transform.forward), ForceMode2D.Impulse);
    }
}
