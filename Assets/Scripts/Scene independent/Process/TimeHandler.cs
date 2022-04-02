using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] float TimeLimit = 60f;
    private ProcessController pc;
    private float time = 0f;
    private bool update = false;
    private float defFixedDelta;
    public float timeScale {get; set;} = 1f;

    private void OnEnable()
    {
        defFixedDelta = Time.fixedDeltaTime;
        pc = GetComponent<ProcessController>();
        UpdateText();
    }

    private void Update()
    {
        if (update) UpdateTime();
        UpdateText();
    }

    public void _Start()
    {
        Time.timeScale = timeScale;
        update = true;
    }

    public void _Pause()
    {
        Time.timeScale = 0f;
        update = false;
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
        if (time >= TimeLimit) pc.PauseNoResume.Invoke();
    }

    public void SetTimeScale(float scale)
    {
        Time.fixedDeltaTime = defFixedDelta * scale;
        timeScale = scale;
    }

}
