using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour
{
    public enum State {Running, Paused, Stopped};
    private SliderController sliderC;
    [SerializeField] private GameObject body;
    private State state = State.Stopped;
    private bool menuOpened = false;

    public UnityEvent Start;
    public UnityEvent Pause;
    public UnityEvent Resume;
    public UnityEvent Stop;
    public UnityEvent MenuOpen;
    public UnityEvent MenuClose;

    private void OnEnable()
    {
        Stop.Invoke();
        MenuClose.Invoke();
    }

    public State GetState()
    {
        return state;
    }

    public void SettingsButtonPressed()
    {
        if (menuOpened)
        {
            MenuClose.Invoke();
            menuOpened = false;
        }
        else
        {
            MenuOpen.Invoke();
            menuOpened = true;
        }
    }

    public void StopButtonPressed()
    {
        state = State.Stopped;
        Stop.Invoke();
    }

    public void PauseButtonPressed()
    {
        switch (state)
        {
            case State.Running:
                {
                    state = State.Paused;
                    Pause.Invoke();
                    break;
                }
            case State.Paused:
                {
                    state = State.Running;
                    Resume.Invoke();
                    break;
                }
            case State.Stopped:
                {
                    state = State.Running;
                    Start.Invoke();
                    MenuClose.Invoke();
                    menuOpened = false;
                    break;
                }
        }

    }


}
