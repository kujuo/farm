using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public class CombatLevelManager : MonoBehaviour
{
    public int numberOfEnemies;
    public List<Item> itemsRecieved; //items recieved when level ends
    public string levelCompleteSceneName;
    private bool loaded = false;
    public int loade = 0;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerController.instance.ApplyEffects();
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //itemsRecieved.Add(Item.itemName = 'WatermelonSeed');
    }

    // Update is called once per frame
    void Update()
    {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (numberOfEnemies <= 0)
        {
            Time.timeScale = 0;
            addItemWhenLevelEnd(itemsRecieved);
            LoadSceneManager load = FindObjectOfType<LoadSceneManager>();
            if (!loaded) { load.load(levelCompleteSceneName, true); }
            loaded = true;

            //add all items

        }
    }

    public void playerDeath()
    {
        LoadSceneManager load = FindObjectOfType<LoadSceneManager>();
        load.load("HomeBase");
    }

    public void addItemWhenLevelEnd(List<Item> itemList)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            InventoryManagerNew.Instance.Add(itemList[i]);
        }
    }


}
