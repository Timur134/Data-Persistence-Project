using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private void Awake()
    {
        SaveData data = SaveData.Instance;

        SaveData.Score[] scores = data.Scores;

        if(scores.Length > 0)
        {
            SaveData.Score lastScore = scores[scores.Length - 1];

            GameData.Instance.HighestScoreOwnerName = lastScore.name;
            GameData.Instance.HighestScore = lastScore.points;

            UpdateScoreText();
        }
        else
        {
            BestScoreText.text = "Best score:- Name:-";
        }
    }
    
    private void UpdateScoreText()
    {
        BestScoreText.text = $"Best score: {GameData.Instance.HighestScore} Name: {GameData.Instance.HighestScoreOwnerName}";
    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";        
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (m_Points > GameData.Instance.HighestScore)
        {
            GameData.Instance.HighestScore = m_Points;
            GameData.Instance.HighestScoreOwnerName = GameData.Instance.UserName;

            UpdateScoreText();

            SaveData.Instance.AddScore(GameData.Instance.HighestScoreOwnerName, GameData.Instance.HighestScore);
            SaveData.Save();
        }
    }

    public void Exit()
    {
        if (m_Points > GameData.Instance.HighestScore)
        {
            GameData.Instance.HighestScore = m_Points;
            GameData.Instance.HighestScoreOwnerName = GameData.Instance.UserName;

            UpdateScoreText();

            SaveData.Instance.AddScore(GameData.Instance.HighestScoreOwnerName, GameData.Instance.HighestScore);
            SaveData.Save();
        }

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
