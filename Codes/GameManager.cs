using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject GameOverPanel;
    public int continuePrice = 100;

    void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;          // остановка игры
        GameOverPanel.SetActive(true);
        ScoreManager.Instance.SaveBestScore();
    }

    // 🔁 CONTINUE / REROLL
    public void ContinueGame()
  {
    if (!GameManagerCoins.Instance.SpendCoins(continuePrice))
        return;

    GameOverPanel.SetActive(false);
    Time.timeScale = 1f;

    PlayerHealth.Instance.Revive();
  }


    public void Restart()
  { 
    Time.timeScale = 1f;

    // 🔹 Сбрасываем все бусты перед новым запуском уровня
    if (BoostManager.Instance != null)
        BoostManager.Instance.ResetBoosts();

    SceneManager.LoadScene(
        SceneManager.GetActiveScene().buildIndex
    );
  }


    public void BackToMenu()
    {
    Time.timeScale = 1f;

    // 🔹 Сбрасываем бусты при выходе в меню
    if (BoostManager.Instance != null)
        BoostManager.Instance.ResetBoosts();

    SceneManager.LoadScene("MainScene");
    } 
} 