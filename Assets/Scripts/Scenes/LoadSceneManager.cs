using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public String currScene;
    public int currSceneNum;
    public GameObject player;
    public GameObject playerUI;
    private AsyncOperation sceneAsync;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("HomeBase", LoadSceneMode.Additive);
        currScene = "HomeBase";
        currSceneNum = 1;
        //StartCoroutine(test());
    }

    public void load(string sceneName)
    {
        StartCoroutine(loadScene(sceneName));
    }

    IEnumerator loadScene(string sceneName)
    {
        GameObject[] oldObjects = SceneManager.GetSceneAt(currSceneNum).GetRootGameObjects();
        foreach (GameObject obj in oldObjects)
        {
            obj.SetActive(false);
        }
        AsyncOperation scene;
        if (sceneName == "HomeBase")
        {
            SceneManager.UnloadSceneAsync(currScene);
            currScene = sceneName;
            currSceneNum = 1;
        }
        else
        {
            currScene = sceneName;
            currSceneNum = 2;
            scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            scene.allowSceneActivation = true;
            sceneAsync = scene;
            while (scene.progress < 1f)
            {
                Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
                yield return null;
            }
        }
        //AsyncOperation scene = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        //scene.allowSceneActivation = false;
        //sceneAsync = scene;

        //Wait until we are done loading the scene


        OnFinishedLoadingAllScene();
    }

    private void OnFinishedLoadingAllScene()
    {
        GameObject[] objects = SceneManager.GetSceneAt(currSceneNum).GetRootGameObjects();
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneAt(currSceneNum));
        player.SetActive(true);
        bool worked = SceneManager.SetActiveScene(SceneManager.GetSceneAt(currSceneNum));
        Debug.Log(worked);
    }

}
