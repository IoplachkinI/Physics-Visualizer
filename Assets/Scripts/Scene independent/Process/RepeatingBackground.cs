using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    public GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;
    public float scrollSpeed;
    private bool resetting = false;

    private void OnEnable()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }
    public void LoadChildObjects(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().enabled = true;
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;
        int childrenNeededW = (int)Mathf.Ceil(screenBounds.x * 6 / objectWidth);
        int childrenNeededH = (int)Mathf.Ceil(screenBounds.y * 6 / objectHeight);

        GameObject objCloneRow = new GameObject();
        objCloneRow.transform.position = obj.transform.position;

        GameObject objClone = Instantiate(obj);

        for (int i = 0; i <= childrenNeededW; i++)
        {
            GameObject c = Instantiate(objClone);
            c.transform.SetParent(objCloneRow.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = "Tile " + i;
        }

        for (int i = 0; i <= childrenNeededH; i++)
        {
            GameObject c = Instantiate(objCloneRow);
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objectHeight * i, obj.transform.position.z);
            c.name = "Tile row " + i;
        }

        Destroy(objClone);
        Destroy(objCloneRow);
        obj.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void RepositionChildObjects(GameObject parent)
    {
        float halfObjectWidth = 0f;
        float halfObjectHeight = 0f;

        GameObject firstRowChild = null;
        GameObject lastRowChild = null;

        if (parent.transform.childCount < 1) return;
        
        GameObject[] rows = new GameObject[parent.transform.childCount];
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = parent.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i].transform.childCount < 1) return;

            GameObject[] objects = new GameObject[rows[i].transform.childCount];


            for (int j = 0; j < objects.Length; j++)
            {
                objects[j] = rows[i].transform.GetChild(j).gameObject;
            }

            GameObject firstObj = objects[0];
            GameObject lastObj = objects[objects.Length - 1];

            if (i == 0) firstRowChild = firstObj;
            else if (i == rows.Length - 1) lastRowChild = firstObj;

            halfObjectWidth = lastObj.GetComponent<SpriteRenderer>().bounds.extents.x - choke / 2f;
            halfObjectHeight = lastObj.GetComponent<SpriteRenderer>().bounds.extents.y - choke / 2f;
            if (transform.position.x + screenBounds.x > lastObj.transform.position.x + halfObjectWidth)
            {
                firstObj.transform.SetAsLastSibling();
                firstObj.transform.position = new Vector3(lastObj.transform.position.x + halfObjectWidth * 2, lastObj.transform.position.y, lastObj.transform.position.z);
            }
            else if (transform.position.x - screenBounds.x < firstObj.transform.position.x - halfObjectWidth)
            {
                lastObj.transform.SetAsFirstSibling();
                lastObj.transform.position = new Vector3(firstObj.transform.position.x - halfObjectWidth * 2, firstObj.transform.position.y, firstObj.transform.position.z);
            }
        }

        GameObject firstRow = rows[0];
        GameObject lastRow = rows[rows.Length - 1];

        if (transform.position.y + screenBounds.y > lastRowChild.transform.position.y + halfObjectHeight)
        {
            firstRow.transform.SetAsLastSibling();
            firstRow.transform.position = new Vector3(lastRow.transform.position.x, lastRowChild.transform.position.y + halfObjectHeight * 2, lastRow.transform.position.z);
        }
        else if (transform.position.y - screenBounds.y < firstRowChild.transform.position.y - halfObjectHeight)
        {
            lastRow.transform.SetAsFirstSibling();
            lastRow.transform.position = new Vector3(firstRow.transform.position.x, firstRowChild.transform.position.y - halfObjectHeight * 2, firstRow.transform.position.z);
        }

    }

    public void ResetBackground()
    {
        foreach (GameObject obj in levels)
        {
            foreach (Transform t in obj.GetComponentsInChildren<Transform>())
            {
                if (t.gameObject == obj) continue;
                Destroy(t.gameObject);
            }
            obj.transform.DetachChildren();
        }

        foreach (GameObject obj in levels) LoadChildObjects(obj);

    }
    private void LateUpdate()
    {
        Debug.Log(levels[0].transform.childCount);
        foreach (GameObject obj in levels)
        {
            RepositionChildObjects(obj);
        }
    }
}
