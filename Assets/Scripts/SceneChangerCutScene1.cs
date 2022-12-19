using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangerCutScene1 : MonoBehaviour
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
        yield return new WaitForSeconds(22.2f);
        operation.allowSceneActivation = true;
    }

}
