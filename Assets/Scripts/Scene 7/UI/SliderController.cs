using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene7
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private List<Slider> sliders;
        [SerializeField] private GameObject body;
        [SerializeField] private TextMesh ft;
        [SerializeField] private ForcesController fc;
        [SerializeField] private TimeHandler th;

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

            body.GetComponent<BodyHandler>().SetMass(sender.value / 10f);
            fc.UpdateAll();
        }


        public void SetForce(Slider sender)
        {
            fc.F = new Vector2(0f, sender.value / 10f);
            ft.text = string.Format("F={0:f1}Í", sender.value / 10f);
            fc.UpdateAll();
        }


        public void SetTimeScale(Slider sender)
        {
            th.SetTimeScale(sender.value / 10f);
        }

    }
}

