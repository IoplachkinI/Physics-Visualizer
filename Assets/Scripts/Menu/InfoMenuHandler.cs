using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenuHandler : MonoBehaviour
{
    private bool menuOpen = false;

    public void MenuOpen()
    {
        if (menuOpen) return;
        menuOpen = true;
        gameObject.SetActive(true);
    }

    public void MenuClose()
    {
        if (!menuOpen) return;
        menuOpen = false;
        gameObject.SetActive(false);
    }
}
