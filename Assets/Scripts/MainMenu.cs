using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnNameChanged(string name)
    {
        GameData.Instance.UserName = name;
    }

    public void PlayGame()
    {
        if(GameData.Instance.UserName.Length == 0)
        {
            GameData.Instance.UserName = "Unknown";
        }

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ShowScores()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
