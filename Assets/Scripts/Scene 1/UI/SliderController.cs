using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene1
{
    public class SliderController : MonoBehaviour
    {

        [SerializeField] private List<Slider> sliders;
        [SerializeField] private Text angleT, frictionT;
        [SerializeField] private GameObject body;
        [SerializeField] private GameObject planeParent;
        [SerializeField] private Scene1.ForcesController forcesC;
        [SerializeField] private TimeHandler timeH;

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
            float angle = -sender.value;
            Quaternion target = Quaternion.Euler(0, 0, angle);
            planeParent.transform.rotation = target;
            body.transform.rotation = target;
            angleT.text = string.Format("”√ŒÀ Õ¿ ÀŒÕ¿: {0:f0}", Mathf.Abs(angle));
            bodyH.Impulse =
                Vector3.Project(bodyH.Impulse, body.transform.right).normalized *
                bodyH.Impulse.magnitude;
            forcesC.UpdateAll();
        }

        public void SetMass(Slider sender)
        {
            float val = sender.value;
            if (val >= 100f) val -= 99f;
            else if (val < 100f) val /= 100f;

            body.GetComponent<BodyHandler>().SetMass(val);
            forcesC.UpdateAll();
        }

        public void SetFriction(Slider sender)
        {
            forcesC.friction = sender.value;
            frictionT.text = string.Format(" Œ›‘‘. “–≈Õ»ﬂ: {0:f2}", sender.value);
            forcesC.UpdateAll();
        }

        public void SetImpulse(Slider sender)
        {
            body.GetComponent<BodyHandler>().Impulse = sender.value * body.transform.right;
            forcesC.UpdateAll();
        }

        public void SetTimeScale(Slider sender)
        {
            timeH.SetTimeScale(sender.value / 10f);
        }

    }
}
