using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveObjectsHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objects;

    [SerializeField]
    private bool justVisibility = false;

    public void EnableAllObjects()
    {
        if (!justVisibility)
        {
            foreach (GameObject obj in objects) obj.SetActive(true);
            return;
        }

        foreach (GameObject obj in objects)
        {
            if (obj.TryGetComponent(out MeshRenderer mesh)) mesh.enabled = true;
            if (obj.TryGetComponent(out Text canvas)) canvas.enabled = true;
        }
    }

    public void DisableAllObjects()
    {
        if (!justVisibility)
        {
            foreach (GameObject obj in objects) obj.SetActive(false);
            return;
        }

        foreach (GameObject obj in objects)
        {
            if (obj.TryGetComponent(out MeshRenderer mesh)) mesh.enabled = false;
            if (obj.TryGetComponent(out Text canvas)) canvas.enabled = false;
        }
    }
}
