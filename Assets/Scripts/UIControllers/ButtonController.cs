using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private enum State {Running, Paused, Stopped};
    private ImpulseSliderController impulseController;
    private SliderController sliderController;
    [SerializeField] private MenuHandler menuHandler;
    [SerializeField] private SettingsButtonHandler settingsHandler;
    [SerializeField] private PauseButtonHandler pauseHandler;
    [SerializeField] private StopButtonHandler stopHandler;
    [SerializeField] private TimeDisplayHandler timeHandler;
    [SerializeField] private List<GameObject> runtimeDisplays;
    [SerializeField] private List<GameObject> stopppedDisplays;
    [SerializeField] private GameObject block;
    private State state;
    private bool menuOpened;

    public void OnEnable()
    {
        impulseController = GetComponent<ImpulseSliderController>();
        sliderController = GetComponent<SliderController>();

        state = State.Stopped;
        menuOpened = false;
        foreach (GameObject obj in runtimeDisplays) obj.SetActive(false);
        foreach (GameObject obj in stopppedDisplays) obj.SetActive(true);
        block.GetComponent<Rigidbody2D>().isKinematic = true;
        settingsHandler.EnableButton();
        
        stopHandler.DisableButton();
        pauseHandler.Pause();
    }

    public void SettingsButtonPressed()
    {
        if (menuOpened)
        {
            menuOpened = false;
            settingsHandler.Close();
            sliderController.DisableSliders();
            menuHandler.Close();
        }
        else
        {
            menuOpened = true;
            settingsHandler.Open();
            sliderController.EnableSliders();
            menuHandler.Open();
        }
    }

    public void StopButtonPressed()
    {
        Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
        state = State.Stopped;
        foreach (GameObject obj in runtimeDisplays) obj.SetActive(false);
        foreach (GameObject obj in stopppedDisplays) obj.SetActive(true);
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        block.transform.position = Vector3.zero;
        pauseHandler.Pause();
        settingsHandler.EnableButton();
        sliderController.EnableSliders();
        stopHandler.DisableButton();
        timeHandler.updateTime = false;
        timeHandler.ResetTime();
        Time.timeScale = 1f;
    }

    public void PauseButtonPressed()
    {
        Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
        switch (state)
        {
            case State.Running:
                {
                    state = State.Paused;
                    pauseHandler.Pause();
                    timeHandler.updateTime = false;
                    Time.timeScale = 0f;
                    break;
                }
            case State.Paused:
                {
                    state = State.Running;
                    pauseHandler.Play();
                    timeHandler.updateTime = true;
                    Time.timeScale = 1f;
                    break;
                }
            case State.Stopped:
                {
                    if (menuOpened)
                    {
                        menuOpened = false;
                        settingsHandler.Close();
                        menuHandler.Close();
                    }
                    state = State.Running;
                    timeHandler.updateTime = true;
                    foreach (GameObject obj in runtimeDisplays) obj.SetActive(true);
                    foreach (GameObject obj in stopppedDisplays) obj.SetActive(false);
                    rb.isKinematic = false;
                    rb.AddForce(impulseController.GetImpulse() * rb.mass, ForceMode2D.Impulse);
                    rb.WakeUp();
                    settingsHandler.DisableButton();
                    sliderController.DisableSliders();
                    stopHandler.EnableButton();
                    pauseHandler.Play();
                    break;
                }
        }

    }


}
