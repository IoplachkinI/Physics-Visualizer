using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleSliderController : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject text;

    public void OnEnable()
    {
        ChangeAngle();
    }

    public void ChangeAngle()
    {
        float angle = -slider.GetComponent<Slider>().value / 100f;
        Quaternion target = Quaternion.Euler(0, 0, angle);
        plane.transform.rotation = target;
        block.transform.rotation = target;
        text.GetComponent<Text>().text = string.Format("”√ŒÀ Õ¿ ÀŒÕ¿: {0:f1}", Mathf.Abs(angle));
    }

}
