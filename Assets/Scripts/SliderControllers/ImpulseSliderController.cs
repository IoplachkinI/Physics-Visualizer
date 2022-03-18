using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImpulseSliderController : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject block;
    private Vector3 impulse = Vector3.zero;

    private void OnEnable()
    {
        UpdateImpulse();
    }

    public Vector3 GetImpulse()
    {
        return impulse;
    }

    public void UpdateImpulse()
    {
        text.GetComponent<Text>().text = string.Format("Õ¿◊¿À‹Õ¿ﬂ — Œ–Œ—“‹: {0:f0} Ï/Ò", slider.value);
        impulse = block.transform.right * slider.value;
    }

}
