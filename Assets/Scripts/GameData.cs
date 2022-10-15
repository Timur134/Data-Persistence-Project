using UnityEngine;

public class GameData : MonoBehaviour
{
    [HideInInspector]
    public string UserName;
    [HideInInspector]
    public string HighestScoreOwnerName;

    [HideInInspector]
    public int HighestScore;

    [HideInInspector]
    public static GameData Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
