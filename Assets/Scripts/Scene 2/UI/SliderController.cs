using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene2
{

    public class SliderController : MonoBehaviour
    {
        [SerializeField] private List<Slider> sliders;
        [SerializeField] private Text anglePlaneT, angleBodyT, distT;
        [SerializeField] private GameObject body;
        [SerializeField] private GameObject planeParent;
        [SerializeField] private ForcesController forcesC;
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


        public void SetImpulseAngle(Slider sender)
        {
            BodyHandler bh = body.GetComponent<BodyHandler>();
            float angle = sender.value;
            angleBodyT.text = string.Format("”√ŒÀ œŒÀ≈“¿: {0:f0}∞", Mathf.Abs(angle));
            bh.impulseDirRel = Quaternion.Euler(0, 0, angle) * Vector2.right;
            bh.UpdateImpulse();
            forcesC.UpdateAll();
        }

        public void SetPlaneAngle(Slider sender)
        {
            float angle = -sender.value;
            BodyHandler bh = body.GetComponent<BodyHandler>();
            anglePlaneT.text = string.Format("”√ŒÀ Õ¿ ÀŒÕ¿ œÀŒ— Œ—“»: {0:f0}∞", Mathf.Abs(angle));
            planeParent.transform.rotation = Quaternion.Euler(0, 0, angle);
            bh.UpdateStartingPos();
            bh.UpdateImpulse();
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

        public void SetImpulse(Slider sender)
        {
            BodyHandler bh = body.GetComponent<BodyHandler>();
            bh.impulseMag = sender.value;
            bh.UpdateImpulse();
            forcesC.UpdateAll();
        }

        public void SetTimeScale(Slider sender)
        {
            timeH.SetTimeScale(sender.value / 10f);
        }

        public void SetDistance(Slider sender)
        {
            body.GetComponent<BodyHandler>().SetDistance(sender.value / 100f);
            distT.text = string.Format("–¿——“ŒﬂÕ»≈ Œ“ “≈À¿ ƒŒ œÀŒ— Œ—“»: {0:f1} Ï", sender.value / 100f);
            forcesC.UpdateAll();
        }
    }
}
