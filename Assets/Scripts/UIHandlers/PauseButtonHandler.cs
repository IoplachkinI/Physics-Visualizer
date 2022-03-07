using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonHandler : MonoBehaviour
{
    public Sprite pauseIcon;
    public Sprite playIcon;

    public void Pause()
    {
        gameObject.GetComponent<Image>().sprite = playIcon;
    }

    public void Play()
    {
        gameObject.GetComponent<Image>().sprite = pauseIcon;
    }


}
