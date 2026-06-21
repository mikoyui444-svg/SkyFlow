using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Transform player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    float startY;
    float maxHeight;
    int score;
    int bestScore;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        startY = player.position.y;
        maxHeight = startY;

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "" + bestScore;
        scoreText.text = "0";
    }

    void Update()
    {
        if (player.position.y > maxHeight)
        {
            maxHeight = player.position.y;
            score = Mathf.FloorToInt(maxHeight - startY);
            scoreText.text = score.ToString();
            DifficultyManager.Instance.UpdateDifficulty(score);
        }
    }

    public void SaveBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
        }
    }
}
