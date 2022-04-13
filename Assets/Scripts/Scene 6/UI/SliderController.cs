using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene6
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

        public void SetMassLeft(Slider sender)
        {
            topBody.GetComponent<BodyHandler>().SetMass(sender.value / 10f);
            forcesC.UpdateAll();
        }

        public void SetMassRight(Slider sender)
        {

            bottomBody.GetComponent<BodyHandler>().SetMass(sender.value / 10f);
            forcesC.UpdateAll();
        }


        public void SetTimeScale(Slider sender)
        {
            timeH.SetTimeScale(sender.value / 10f);
        }

    }
}
