using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene3
{
    public class SliderController : MonoBehaviour
    {

        [SerializeField] private List<Slider> sliders;
        [SerializeField] private Text angleT, frictionBodyT, frictionWedgeT;
        [SerializeField] private GameObject body, wedge;
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

        public void SetAngle(Slider sender)
        {
            BodyHandler bodyH = body.GetComponent<BodyHandler>();
            float angle = sender.value;

            bodyH.Rotate(Quaternion.Euler(0, 0, angle));
            wedge.GetComponent<WedgeHandler>().SetAngle(angle);
            bodyH.Impulse =
                Vector3.Project(bodyH.Impulse, body.transform.right).normalized
                * bodyH.Impulse.magnitude;

            angleT.text = string.Format("”√ŒÀ  À»Õ¿: {0:f0}∞", angle);
            fc.UpdateAll();
        }

        public void SetMassBody(Slider sender)
        {
            float val = sender.value;
            if (val >= 100f) val -= 99f;
            else if (val < 100f) val /= 100f;

            body.GetComponent<BodyHandler>().SetMass(val);
            fc.UpdateAll();
        }

        public void SetMassWedge(Slider sender)
        {
            float val = sender.value;
            if (val >= 100f) val -= 99f;
            else if (val < 100f) val /= 100f;

            wedge.GetComponent<WedgeHandler>().SetMass(val);
            fc.UpdateAll();

        }

        public void SetFrictionBody(Slider sender)
        {
            fc.body.friction = sender.value;
            frictionBodyT.text = string.Format(" Œ›‘‘»÷»≈Õ“ “–≈Õ»ﬂ Ã≈∆ƒ” ¡ÀŒ ŒÃ »  À»ÕŒÃ: {0:f2}", sender.value);
            fc.UpdateAll();
        }

        public void SetFrictionWedge(Slider sender)
        {
            fc.wedge.friction = sender.value;
            frictionWedgeT.text = string.Format(" Œ›‘‘»÷»≈Õ“ “–≈Õ»ﬂ Ã≈∆ƒ”  À»ÕŒÃ » œÀŒ— Œ—“‹ﬁ: {0:f2}", sender.value);
            fc.UpdateAll();
        }

        public void SetImpulse(Slider sender)
        {
            body.GetComponent<BodyHandler>().Impulse = sender.value * body.transform.right;
            fc.UpdateAll();
        }

        public void SetTimeScale(Slider sender)
        {
            timeH.SetTimeScale(sender.value / 10f);
        }

    }
}
