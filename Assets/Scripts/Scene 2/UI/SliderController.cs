using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene2
{

    public class SliderController : MonoBehaviour
    {
        [SerializeField] private List<Slider> sliders;
        [SerializeField] private Text anglePlaneT, angleBodyT, heightT;
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


        public void SetBodyAngle(Slider sender)
        {
            BodyHandler bodyH = body.GetComponent<BodyHandler>();
            float angle = sender.value;
            Quaternion target = Quaternion.Euler(0, 0, angle);
            angleBodyT.text = string.Format("”√ŒÀ œŒÀ≈“¿: {0:f0}∞", Mathf.Abs(angle));
            bodyH.ImpulseDirRel = target * Vector2.right;
            body.GetComponent<BodyHandler>().ImpulseDir = Quaternion.FromToRotation(Vector2.right, body.transform.right) * body.GetComponent<BodyHandler>().ImpulseDirRel;
            forcesC.UpdateArrows();
        }

        public void SetPlaneAngle(Slider sender)
        {
            float angle = -sender.value;
            Quaternion target = Quaternion.Euler(0, 0, angle);
            anglePlaneT.text = string.Format("”√ŒÀ Õ¿ ÀŒÕ¿ œÀŒ— Œ—“»: {0:f0}∞", Mathf.Abs(angle));
            planeParent.transform.rotation = target;
            body.transform.rotation = target;
            body.GetComponent<BodyHandler>().ImpulseDir = Quaternion.FromToRotation(Vector2.right, body.transform.right) * body.GetComponent<BodyHandler>().ImpulseDirRel;
            body.GetComponent<BodyHandler>().SetRotatedDefStartingPos(target);
            forcesC.UpdateArrows();

        }

        public void SetMass(Slider sender)
        {
            float val = sender.value;
            if (val >= 100f) val -= 99f;
            else if (val < 100f) val /= 100f;

            body.GetComponent<BodyHandler>().SetMass(val);
            forcesC.UpdateArrows();
        }

        public void SetImpulse(Slider sender)
        {
            body.GetComponent<BodyHandler>().ImpulseMag = sender.value;
            forcesC.UpdateArrows();
        }

        public void SetTimeScale(Slider sender)
        {
            timeH.SetTimeScale(sender.value / 10f);
        }

        public void setHeight(Slider sender)
        {
            body.GetComponent<BodyHandler>().StartingPos = body.GetComponent<BodyHandler>().GetRotatedDefStartingPos() + new Vector2(0, sender.value / 100f);
            heightT.text = string.Format("Õ¿◊¿À‹Õ¿ﬂ \n¬€—Œ“¿: {0:f2} Ï", sender.value / 100f);
            forcesC.UpdateArrows();
        }
    }
}
