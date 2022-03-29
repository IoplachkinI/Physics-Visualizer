using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Sprite pauseIcon, playIcon;

    public void SetPlaySprite()
    {
        gameObject.GetComponent<Image>().sprite = playIcon;
    }

    public void SetPauseSprite()
    {
        gameObject.GetComponent<Image>().sprite = pauseIcon;
    }


}
