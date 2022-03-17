using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplayHandler : MonoBehaviour
{
    [SerializeField] Text text;
    private float time = 0f;
    [HideInInspector] public bool updateTime = false;

    private void OnEnable()
    {
        text.text = string.Format("t={0:f2}ñ", time);
    }

    private void Update()
    {
        if (updateTime) UpdateTime();
        UpdateText();
    }

    public void ResetTime()
    {
        time = 0f;
    }

    private void UpdateText()
    {
        text.text = string.Format("t={0:f2}ñ", time);
    }

    private void UpdateTime()
    {
        time += Time.deltaTime;
    }

}
