using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene8
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private List<Slider> sliders;
        [SerializeField] private GameObject body;
        [SerializeField] private Text heightT;
        [SerializeField] private ForcesController fc;
        [SerializeField] private TimeHandler timeH;

        public void OnEnable()
        {
            foreach (Slider slider in sliders)
            {
                slider.onValueChanged.Invoke(0);
            }
        }
        public void DisableSliders()
        {
            foreach (Slider slider in sliders) slider.interactable = false;
        }

        public void EnableSliders()
        {
            foreach (Slider slider in sliders) slider.interactable = true;
        }

        public void SetDensity(Slider sender)
        { 
            body.GetComponent<BodyHandler>().SetMass(sender.value * 100f);
            fc.UpdateAll();
        }


        public void SetHeight(Slider sender)
        {
            body.GetComponent<BodyHandler>().SetHeight(sender.value / 100f);
            heightT.text = string.Format("НАЧАЛЬНАЯ ВЫСОТА (ОТ ДНА): {0:f2} м", sender.value / 100f);
            fc.UpdateAll();
        }


        public void SetTimeScale(Slider sender)
        {
            timeH.SetTimeScale(sender.value / 10f);
        }

    }
}

