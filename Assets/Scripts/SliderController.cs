using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private float angle;
    private float mass;
    private float friction;
    private Vector2 impulse;

    [SerializeField]
    private List<Slider> sliders;

    [SerializeField]
    private Text angleT, frictionT, impulseT;

    [SerializeField]
    private TextMesh massT;

    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private GameObject planeParent;

    [SerializeField]
    private ForcesController forcesC;

    public void DisableSliders()
    {
        foreach (Slider slider in sliders) slider.interactable = false;
    }

    public void EnableSliders()
    {
        foreach (Slider slider in sliders) slider.interactable = true;
    }

    public float GetFriction()
    {
        return friction;
    }

    public Vector2 GetImpulse()
    {
        return impulse;
    }

    public void UpdateAngle(Slider sender)
    {
        angle = -sender.value;
        Quaternion target = Quaternion.Euler(0, 0, angle);
        planeParent.transform.rotation = target;
        body.transform.rotation = target;
        angleT.text = string.Format("”√ŒÀ Õ¿ ÀŒÕ¿: {0:f0}", Mathf.Abs(angle));
    }

    public void UpdateMass(Slider sender)
    {
        int val = (int)sender.value;
        if (val >= 100)
        {
            mass = val - 99f;
            massT.text = string.Format("m={0:f0}Í„", mass);
        }
        if (val < 100)
        {
            mass = val / 100f;
            massT.text = string.Format("m={0:f2}Í„", mass);
        }
        body.mass = mass;
    }

    public void UpdateFriction(Slider sender)
    {
        forcesC.friction = sender.value;
        frictionT.text = string.Format(" Œ›‘‘. “–≈Õ»ﬂ: {0:f2}", sender.value);
    }

    public void UpdateImpulse(Slider sender)
    {
        impulseT.text = string.Format("Õ¿◊¿À‹Õ¿ﬂ — Œ–Œ—“‹: {0:f0} Ï/Ò", sender.value);
        impulse = body.transform.right * sender.value;
    }

}
