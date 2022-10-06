using UnityEngine;

public class UILoad : MonoBehaviour
{
    public void goHome()
    {
        LoadSceneManager.instance.load("HomeBase");
        Time.timeScale = 1;
    }
}
