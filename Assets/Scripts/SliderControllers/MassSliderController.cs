using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MassSliderController : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject displayText;

    private void OnEnable()
    {
        ChangeBlockMass();
    }

    public void ChangeBlockMass()
    {
        float val = slider.GetComponent<Slider>().value;
        float mass = 1;
        if (val >= 100)
        {
            mass = val - 99;
            displayText.GetComponent<TextMesh>().text = string.Format("m={0:f0}Í„", mass);
        }
        if (val < 100)
        {
            mass = val / 100f;
            displayText.GetComponent<TextMesh>().text = string.Format("m={0:f2}Í„", mass);
        }
        block.GetComponent<Rigidbody2D>().mass = mass;
    }

}
