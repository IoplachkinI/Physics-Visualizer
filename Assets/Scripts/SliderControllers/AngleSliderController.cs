using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleSliderController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D plane;
    [SerializeField] private Rigidbody2D block;
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject text;

    public void OnEnable()
    {
        UpdateAngle();
    }

    public void UpdateAngle()
    {
        float angle = -slider.GetComponent<Slider>().value;
        Quaternion target = Quaternion.Euler(0, 0, angle);
        plane.transform.rotation = target;
        block.transform.rotation = target;
        text.GetComponent<Text>().text = string.Format("”√ŒÀ Õ¿ ÀŒÕ¿: {0:f0}", Mathf.Abs(angle));
    }

}
