using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnSettingsButtonClicked;

    private void OnGUI()
    {
        
        if (OnSettingsButtonClicked != null)
        {
            OnSettingsButtonClicked();
        }
    }

}
