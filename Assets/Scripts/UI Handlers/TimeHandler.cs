using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] Text text;
    private float time = 0f;
    private bool update = false;

    private void OnEnable()
    {
        UpdateText();
    }

    private void Update()
    {
        if (update) UpdateTime();
        UpdateText();
    }

    public void StartTime()
    {
        update = true;
        Time.timeScale = 1f;
    }

    public void PauseTime()
    {
        update = false;
        Time.timeScale = 0f;
    }

    public void StopTime()
    {
        time = 0f;
        update = false;
        Time.timeScale = 0f;
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
