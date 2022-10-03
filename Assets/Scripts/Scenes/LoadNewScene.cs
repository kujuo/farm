using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewScene : MonoBehaviour
{
    public string sceneNameToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadSceneManager load = FindObjectOfType<LoadSceneManager>();
            load.load(sceneNameToLoad);
        }
    }
}
