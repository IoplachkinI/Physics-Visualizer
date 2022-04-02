using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PauseButtonHandler : MonoBehaviour
{
    [SerializeField] private Sprite pauseSprite, playSprite, pauseSpriteDisabled, playSpriteDisabled;
    private bool isPlaySprite = true;

    public void Disable()
    {
        if (isPlaySprite) gameObject.GetComponent<Image>().sprite = playSpriteDisabled;
        else gameObject.GetComponent<Image>().sprite = pauseSpriteDisabled;
        GetComponent<Button>().interactable = false;
    }

    public void Enable()
    {
        if (isPlaySprite) gameObject.GetComponent<Image>().sprite = playSprite;
        else gameObject.GetComponent<Image>().sprite = pauseSprite;
        GetComponent<Button>().interactable = true;
    }

    public void SetPlaySprite()
    {
        gameObject.GetComponent<Image>().sprite = playSprite;
        isPlaySprite = true;
    }

    public void SetPauseSprite()
    {
        gameObject.GetComponent<Image>().sprite = pauseSprite;
        isPlaySprite = false;
    }


}
