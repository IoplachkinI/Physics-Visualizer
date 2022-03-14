using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopButtonHandler : MonoBehaviour
{
    public void DisableButton()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }

    public void EnableButton()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }
}
