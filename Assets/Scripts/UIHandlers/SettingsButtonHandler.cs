using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonHandler : MonoBehaviour
{
    public LeanTweenType ease;

    public float rotateDuration;
    public float rotateAngle;


    public void Open()
    {
        LeanTween.rotate(gameObject, new Vector3(0, 0, rotateAngle), rotateDuration).setEase(ease).setIgnoreTimeScale(true);
    }

    public void Close()
    {
        LeanTween.rotate(gameObject, new Vector3(0, 0, 0), rotateDuration).setEase(ease).setIgnoreTimeScale(true);
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
