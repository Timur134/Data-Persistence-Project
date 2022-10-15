using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    void Awake()
    {
        SaveData data = SaveData.Instance;

        SaveData.Score[] scores = data.Scores;

        string scoreString;

        if (scores.Length > 0)
        {
            SaveData.Score lastScore = scores[scores.Length - 1];

            scoreString = $"Best score: {lastScore.points} Name: {lastScore.name}\n";

            for (int i = scores.Length - 2; i > -1; i--)
            {
                scoreString += $"Score: {scores[i].points} Name: {scores[i].name}\n";
            }
        }
        else
        {
            scoreString = "-";
        }

        scoreText.text = scoreString;
    }

    public void OnGoBack()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
