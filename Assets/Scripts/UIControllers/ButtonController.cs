using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private MenuHandler menuHandler;
    [SerializeField] private SettingsButtonHandler settingsHandler;
    [SerializeField] private PauseButtonHandler pauseHandler;
    [SerializeField] private List<GameObject> runtimeDisplays;
    [SerializeField] private List<GameObject> pauseDisplays;
    [SerializeField] private GameObject block;
    private bool gamePaused = true;
    private bool menuOpened = false;

    public void OnEnable()
    {
        foreach (GameObject obj in runtimeDisplays) obj.SetActive(false);
        foreach (GameObject obj in pauseDisplays) obj.SetActive(true);
        block.GetComponent<Rigidbody2D>().isKinematic = true;
        settingsHandler.EnableButton();
        pauseHandler.Pause();
    }

    public void SettingsButtonPressed()
    {
        if (!gamePaused) return;

        if (menuOpened)
        {
            menuOpened = false;
            settingsHandler.Close();
            menuHandler.Close();
        }
        else
        {
            menuOpened = true;
            settingsHandler.Open();
            menuHandler.Open();
        }

    }

    public void PauseButtonPressed()
    {
        if (gamePaused)
        {
            if (menuOpened)
            {
                menuOpened = false;
                settingsHandler.Close();
                menuHandler.Close();
            }
            gamePaused = false;
            foreach (GameObject obj in runtimeDisplays) obj.SetActive(true);
            foreach (GameObject obj in pauseDisplays) obj.SetActive(false);
            block.GetComponent<Rigidbody2D>().isKinematic = false;
            settingsHandler.DisableButton();
            pauseHandler.Play();
        }

        else 
        {
            Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
            gamePaused = true;
            foreach (GameObject obj in runtimeDisplays) obj.SetActive(false);
            foreach (GameObject obj in pauseDisplays) obj.SetActive(true);
            block.transform.position = Vector3.zero;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            settingsHandler.EnableButton();
            pauseHandler.Pause();
        }

    }

}
