using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    private float xStart;
    private bool isOpened = false;

    [SerializeField]
    private float xEnd, duration;

    [SerializeField] LeanTweenType ease;

    private void Start()
    {
        xStart = gameObject.GetComponent<RectTransform>().anchoredPosition.x;
    }

    public void SetOpened()
    {
        if (isOpened) return;
        LeanTween.moveX(gameObject.GetComponent<RectTransform>(), xEnd, duration).setEase(ease).setIgnoreTimeScale(true);
        isOpened = true;
    }

    public void SetClosed()
    {
        if (!isOpened) return;
        LeanTween.moveX(gameObject.GetComponent<RectTransform>(), xStart, duration).setEase(ease).setIgnoreTimeScale(true);
        isOpened = false;
    }

}
