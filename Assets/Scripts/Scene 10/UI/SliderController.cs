using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene10
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private List<Slider> sliders;
        [SerializeField] private GameObject body;
        [SerializeField] private Text lengthT, coeffT, offsetT;
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

        public void SetMass(Slider sender)
        {
            body.GetComponent<BodyHandler>().SetMass(sender.value);
            fc.UpdateAll();
        }


        public void SetLength(Slider sender)
        {
            body.GetComponent<BodyHandler>().SetLength(sender.value / 100f);
            lengthT.text = string.Format("ƒÀ»Õ¿ œ–”∆»Õ€ ¬ Õ≈…“–¿À‹ÕŒÃ —Œ—“ŒﬂÕ»»: {0:f2} Ï", sender.value / 100f);
            fc.UpdateAll();
        }

        public void SetCoefficient(Slider sender)
        {
            fc.k = sender.value * 10f;
            coeffT.text = string.Format(" Œ›‘‘»÷»≈Õ“ ”œ–”√Œ—“» œ–”∆»Õ€: {0:f0} Õ/Ï", sender.value * 10f);
            fc.UpdateAll();
        }

        public void SetOffset(Slider sender)
        {
            body.GetComponent<BodyHandler>().SetOffset(sender.value / 100f);
            offsetT.text = string.Format("Õ¿◊¿À‹ÕŒ≈ —Ã≈Ÿ≈Õ»≈ Œ“ œŒÀŒ∆≈Õ»ﬂ –¿¬ÕŒ¬≈—»ﬂ: {0:f0}%", sender.value);
        }

        public void SetTimeScale(Slider sender)
        {
            timeH.SetTimeScale(sender.value / 10f);
        }

    }
}

