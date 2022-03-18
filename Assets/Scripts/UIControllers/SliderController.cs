using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private AngleSliderController angle;
    private MassSliderController mass;
    private FrictionSliderController friction;
    private ImpulseSliderController impulse;
    [SerializeField] private ForcesController forces;
    [SerializeField] private List<Slider> sliders;

    private void OnEnable()
    {
        angle = GetComponent<AngleSliderController>();
        mass = GetComponent<MassSliderController>();
        friction = GetComponent<FrictionSliderController>();    
        impulse = GetComponent<ImpulseSliderController>();
        foreach (Slider slider in sliders) slider.interactable = true;
    }

    public void UpdateAngle()
    {
        angle.UpdateAngle();
        impulse.UpdateImpulse();
        forces.UpdateVectors();
    }

    public void UpdateMass()
    {
        mass.UpdateMass();
        forces.UpdateVectors();
    }

    public void UpdateFriction()
    {
        friction.UpdateFriction();
        forces.UpdateVectors();
    }

    public void UpdateImpulse()
    {
        impulse.UpdateImpulse();
    }

    public void DisableSliders()
    {
        foreach (Slider slider in sliders) slider.interactable = false;
    }

    public void EnableSliders()
    {
        foreach (Slider slider in sliders) slider.interactable = true;
    }
}
