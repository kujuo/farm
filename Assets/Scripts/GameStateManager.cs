using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager instance;
    public bool pause;
    public bool isInteracting;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        pause = false;
        isInteracting = false;
    }

    public void Pause(bool pause)
    {
        if (pause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public static GameStateManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<GameStateManager>();
                if (!instance) throw new UnityException("GameStateManager instance not found");
            }
            return instance;
        }
    }
}