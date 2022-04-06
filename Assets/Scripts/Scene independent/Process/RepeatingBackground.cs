using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    public GameObject[] levels;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke = 0f;
    public float scrollSpeed;
    public float buffer = 1f;

    private void OnEnable()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        ResetBackground();
    }

    public void LoadChildObjects(GameObject obj)
    {
        obj.GetComponent<SpriteRenderer>().enabled = true;
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        float objectHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - choke;
        int childrenNeededW = (int)Mathf.Ceil(screenBounds.x * 10 / objectWidth);
        int childrenNeededH = (int)Mathf.Ceil(screenBounds.y * 10 / objectHeight);

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

            if (transform.position.x + screenBounds.x > lastObj.transform.position.x + halfObjectWidth - buffer
                && transform.position.x - screenBounds.x < lastObj.transform.position.x - halfObjectWidth + buffer)
                return;

            if (transform.position.x + screenBounds.x > firstObj.transform.position.x + halfObjectWidth - buffer
                && transform.position.x - screenBounds.x < firstObj.transform.position.x - halfObjectWidth + buffer)
                return;

            if (transform.position.x + screenBounds.x > lastObj.transform.position.x + halfObjectWidth - buffer)
            {
                for (int j = 0; j < objects.Length - 1; j++)
                {
                    objects[j].transform.position = new Vector3(
                        lastObj.transform.position.x + halfObjectWidth * 2 * (j + 1),
                        lastObj.transform.position.y,
                        lastObj.transform.position.z);
                }
                lastObj.transform.SetAsFirstSibling();
            }

            else if (transform.position.x - screenBounds.x < firstObj.transform.position.x - halfObjectWidth + buffer)
            {
                for (int j = 1; j < objects.Length; j++)
                {
                    objects[j].transform.position = new Vector3(
                        firstObj.transform.position.x - halfObjectWidth * 2 * (objects.Length - j),
                        firstObj.transform.position.y,
                        firstObj.transform.position.z);
                }
                firstObj.transform.SetAsLastSibling();
            }

        }

        GameObject firstRow = rows[0];
        GameObject lastRow = rows[rows.Length - 1];

        if (transform.position.y + screenBounds.y > lastRowChild.transform.position.y + halfObjectHeight - buffer
            && transform.position.y - screenBounds.y < lastRowChild.transform.position.y - halfObjectHeight + buffer)
            return;

        if (transform.position.y + screenBounds.y > firstRowChild.transform.position.y + halfObjectHeight - buffer
            && transform.position.y - screenBounds.y < firstRowChild.transform.position.y - halfObjectHeight + buffer)
            return;

        if (transform.position.y + screenBounds.y > lastRowChild.transform.position.y + halfObjectHeight - buffer)
        {
            for (int i = 0; i < rows.Length - 1; i++)
            {
                rows[i].transform.position = new Vector3(lastRow.transform.position.x,
                lastRowChild.transform.position.y + halfObjectHeight * 2 * (i + 1),
                lastRow.transform.position.z);
            }
            lastRow.transform.SetAsFirstSibling();
        }

        else if (transform.position.y - screenBounds.y < firstRowChild.transform.position.y - halfObjectHeight + buffer)
        {
            for (int i = 1; i < rows.Length; i++)
            {
                rows[i].transform.position = new Vector3(firstRow.transform.position.x,
                firstRowChild.transform.position.y - halfObjectHeight * 2 * (rows.Length - i),
                firstRow.transform.position.z);
            }
            firstRow.transform.SetAsLastSibling();
        }


    }

    public void ResetBackground()
    {
        foreach (GameObject obj in levels)
        {
            foreach (Transform t in obj.transform) Destroy(t.gameObject);
            obj.transform.DetachChildren();
        }

        foreach (GameObject obj in levels) LoadChildObjects(obj);

    }
    private void LateUpdate()
    {
        foreach (GameObject obj in levels)
        {
            RepositionChildObjects(obj);
        }
    }
}
