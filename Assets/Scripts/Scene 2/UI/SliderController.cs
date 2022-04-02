using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scene2
{

    public class SliderController : MonoBehaviour
    {
        [SerializeField] private List<Slider> sliders;
        [SerializeField] private Text anglePlaneT, angleBodyT;
        [SerializeField] private GameObject body;
        [SerializeField] private GameObject planeParent;
        [SerializeField] private Scene2.ForcesController forcesC;
        [SerializeField] private TimeHandler timeH;

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
            float angle = -sender.value;
            Quaternion target = Quaternion.Euler(0, 0, angle);
            angleBodyT.text = string.Format("ÓÃÎË ÏÎËÅÒÀ: {0:f0}", Mathf.Abs(angle));
            bodyH.Impulse = target * bodyH.Impulse;
            forcesC.UpdateAll();
        }

        public void SetPlaneAngle(Slider sender)
        {
            float angle = -sender.value;
            Quaternion target = Quaternion.Euler(0, 0, angle);
            anglePlaneT.text = string.Format("ÓÃÎË ÍÀÊËÎÍÀ ÏËÎÑÊÎÑÒÈ: {0:f0}", Mathf.Abs(angle));
            planeParent.transform.rotation = target;
            body.transform.position = target * body.transform.position;

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
            body.GetComponent<BodyHandler>().Impulse = sender.value * body.transform.right;
            forcesC.UpdateAll();
        }

        public void SetTimeScale(Slider sender)
        {
            timeH.SetTimeScale(sender.value / 10f);
        }
    }
}
