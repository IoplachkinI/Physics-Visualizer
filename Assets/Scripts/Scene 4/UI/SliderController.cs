using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene4
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private List<Slider> sliders;
        [SerializeField] private Text fricBodyT, fricPlaneT;
        [SerializeField] private GameObject topBody;
        [SerializeField] private GameObject bottomBody;
        [SerializeField] private ForcesController forcesC;
        [SerializeField] private TimeHandler timeH;

        public void DisableSliders()
        {
            foreach (Slider slider in sliders) slider.interactable = false;
        }

        public void EnableSliders()
        {
            foreach (Slider slider in sliders) slider.interactable = true;
        }

        public void SetFrictionBody(Slider sender)
        {
            forcesC.topBody.friction = sender.value;
            fricBodyT.text = string.Format(" Œ›‘‘. “–≈Õ»ﬂ Ã≈∆ƒ” ¡ÀŒ ¿Ã»: {0:f2}", sender.value);
            forcesC.UpdateAll();
        }

        public void SetFrictionPlane(Slider sender)
        {
            forcesC.bottomBody.friction = sender.value;
            fricPlaneT.text = string.Format(" Œ›‘‘. “–≈Õ»ﬂ Ã≈∆ƒ” œÀŒ— Œ—“‹ﬁ » ¡ÀŒ ŒÃ: {0:f2}", sender.value);
            forcesC.UpdateAll();
        }

        public void SetMassTop(Slider sender)
        {
            topBody.GetComponent<TopBodyHandler>().SetMass(sender.value / 10f);
            forcesC.UpdateAll();
        }

        public void SetMassBottom(Slider sender)
        {

            bottomBody.GetComponent<BottomBodyHandler>().SetMass(sender.value / 10f);
            forcesC.UpdateAll();
        }

        public void SetImpulseTop(Slider sender)
        {
            topBody.GetComponent<TopBodyHandler>().Impulse = new Vector2(sender.value / 10f, 0);
            forcesC.UpdateAll();
        }

        public void SetImpulseBottom(Slider sender)
        {
            bottomBody.GetComponent<BottomBodyHandler>().Impulse = new Vector2(sender.value / 10f, 0);
            forcesC.UpdateAll();
        }

        public void SetTimeScale(Slider sender)
        {
            timeH.SetTimeScale(sender.value / 10f);
        }

    }
}
