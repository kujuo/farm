using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager instance;
    public string currScene;
    public int currSceneNum;
    [SerializeField] AudioClip[] audioClips;
    public GameObject player;
    public GameObject playerUI;
    private AsyncOperation sceneAsync;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) return;
        instance = this;
        audioSource = GetComponent<AudioSource>();
        StartGame();
        PlayerController.instance.pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void StartGame()
    {
        currScene = "HomeBase";
        currSceneNum = 1;
        StartCoroutine(loadHome());
    }

    public void load(string sceneName, bool unloadOld = false)
    {
        StartCoroutine(loadScene(sceneName, unloadOld));
    }

    IEnumerator loadHome()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync("HomeBase", LoadSceneMode.Additive);
        scene.allowSceneActivation = true;
        sceneAsync = scene;
        while (scene.progress < 1f)
        {
            yield return null;
        }
        audioSource.clip = audioClips[0];
        audioSource.Play();
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(currSceneNum));
    }

    IEnumerator loadScene(string sceneName, bool unloadOld = false)
    {
        
        AsyncOperation scene;
        if (sceneName == "HomeBase")
        {
           
            audioSource.clip = audioClips[0];
            audioSource.Play();
            SceneManager.UnloadSceneAsync(currScene);
            currScene = sceneName;
            currSceneNum = 1;
        }
        else
        {
            if (unloadOld)
            {
                SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneAt(1));
                SceneManager.UnloadSceneAsync(currScene);
                currSceneNum--;
            }
            GameObject[] oldObjects = SceneManager.GetSceneAt(currSceneNum).GetRootGameObjects();
            foreach (GameObject obj in oldObjects)
            {
                obj.SetActive(false);
            }

            currScene = sceneName;
            if (!unloadOld) currSceneNum ++;
            if (currScene == "NeighborVillage1") audioSource.clip = audioClips[2];
            else if (currScene == "NeighborVillage2") audioSource.clip = audioClips[3];
            else if (currScene == "NeighborVillage3") audioSource.clip = audioClips[4];
            audioSource.Play();
            scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            scene.allowSceneActivation = true;
            sceneAsync = scene;
            while (scene.progress < 1f)
            {
                yield return null;
            }
        }

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
        player.GetComponent<PlayerController>().Reset();
        if (GameObject.Find("CombatLevelManager") != null) PlayerController.instance.ApplyEffects();
        CameraController.instance.Clear();
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(currSceneNum));
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn");
        Vector3 spawnLocation = spawns[spawns.Length-1].transform.position;
        player.transform.position = spawnLocation;
    }

}
