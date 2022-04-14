using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene9
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private List<Slider> sliders;
        [SerializeField] private GameObject body;
        [SerializeField] private Text lengthT, coeffT;
        [SerializeField] private ForcesController fc;
        [SerializeField] private TimeHandler timeH;

        public void OnEnable()
        {
            foreach(Slider slider in sliders){
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


        public void SetLength(Slider sender)
        {
            body.GetComponent<BodyHandler>().SetLength(sender.value / 100f);
            lengthT.text = string.Format("ДЛИНА ПРУЖИНЫ В НЕЙТРАЛЬНОМ СОСТОЯНИИ: {0:f2} м", sender.value / 100f);
            fc.UpdateAll();
        }

        public void SetCoefficient(Slider sender)
        {
            fc.k = sender.value * 1000f;
            coeffT.text = string.Format("КОЭФФИЦИЕНТ УПРУГОСТИ ПРУЖИНЫ: {0:f0} Н/м", sender.value * 1000f);
            fc.UpdateAll();
        }

        public void SetTimeScale(Slider sender)
        {
            timeH.SetTimeScale(sender.value / 10f);
        }

    }
}

