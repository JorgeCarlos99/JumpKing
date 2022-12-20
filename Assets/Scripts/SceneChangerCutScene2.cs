using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangerCutScene2 : MonoBehaviour
{
    public float changeTime;
    public string sceneName;

    private void Start()
    {
        StartCoroutine(LoadAsync(sceneName));
    }
 
    IEnumerator LoadAsync(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(4.1f);
        operation.allowSceneActivation = true;
    }
}
