using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    private AngleSliderController angle;
    private MassSliderController mass;
    private FrictionSliderController friction;
    private ImpulseSliderController impulse;
    [SerializeField] private ForcesController forces;

    private void OnEnable()
    {
        angle = GetComponent<AngleSliderController>();
        mass = GetComponent<MassSliderController>();
        friction = GetComponent<FrictionSliderController>();    
        impulse = GetComponent<ImpulseSliderController>();
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
}
