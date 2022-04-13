using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static string curSceneName;
    public void SwitchToName(string name)
    {
        StartCoroutine(LoadSceneAsync(name));
        curSceneName = name;
    }

    private IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);

        while (!op.isDone)
        {
            yield return null;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (curSceneName == "Menu") Application.Quit();
            else StartCoroutine(LoadSceneAsync("Menu"));
        }
    }
}
