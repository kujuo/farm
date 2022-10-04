using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatLevelManager : MonoBehaviour
{
    public int numberOfEnemies;
    // Start is called before the first frame update
    void Start()
    {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (numberOfEnemies <= 0)
        {
            LoadSceneManager load = FindObjectOfType<LoadSceneManager>();
            load.load("HomeBase");
        }
    }

    public void playerDeath()
    {
        LoadSceneManager load = FindObjectOfType<LoadSceneManager>();
        load.load("HomeBase");
    }


}
