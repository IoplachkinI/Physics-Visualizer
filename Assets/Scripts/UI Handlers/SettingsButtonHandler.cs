using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonHandler : MonoBehaviour
{
    [SerializeField]
    private LeanTweenType ease;

    [SerializeField]
    private float rotateDuration, angle;

    private bool isOpened = false;


    public void SetOpened()
    {
        if (isOpened) return;
        LeanTween.rotate(gameObject, new Vector3(0, 0, angle), rotateDuration).setEase(ease).setIgnoreTimeScale(true);
        isOpened = true;
    }

    public void SetClosed()
    {
        if (!isOpened) return;
        LeanTween.rotate(gameObject, new Vector3(0, 0, 0), rotateDuration).setEase(ease).setIgnoreTimeScale(true);
        isOpened = false;
    }

    public void DisableButton()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void EnableButton()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }


}
