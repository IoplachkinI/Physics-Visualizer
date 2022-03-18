using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    private float startX = 0;
    public float endX;
    public float duration;
    public LeanTweenType ease;

    private void OnEnable()
    {
        startX = gameObject.GetComponent<RectTransform>().anchoredPosition.x;
    }

    public void Open()
    {
        LeanTween.moveX(gameObject.GetComponent<RectTransform>(), endX, duration).setEase(ease).setIgnoreTimeScale(true);
    }

    public void Close()
    {
        LeanTween.moveX(gameObject.GetComponent<RectTransform>(), startX, duration).setEase(ease).setIgnoreTimeScale(true);
    }

}
