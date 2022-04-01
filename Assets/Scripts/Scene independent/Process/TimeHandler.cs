using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] Text text;
    private float time = 0f;
    private bool update = false;
    private float defFixedDelta;
    public float timeScale {get; set;} = 1f;

    private void OnEnable()
    {
        defFixedDelta = Time.fixedDeltaTime;
        UpdateText();
    }

    private void Update()
    {
        if (update) UpdateTime();
        UpdateText();
    }

    public void _Start()
    {
        update = true;
        Time.timeScale = timeScale;
    }

    public void _Pause()
    {
        update = false;
        Time.timeScale = 0f;
    }

    public void _Stop()
    {
        time = 0f;
        update = false;
    }

    private void UpdateText()
    {
        text.text = string.Format("t={0:f2}ñ", time);
    }

    private void UpdateTime()
    {
        time += Time.deltaTime;
    }

    public void UpdateTimeScale(float scale)
    {
        timeScale = scale;
        Time.fixedDeltaTime = defFixedDelta * scale;
    }

}
