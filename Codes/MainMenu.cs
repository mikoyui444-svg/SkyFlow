using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Загрузка основной игры
    public void PlayGame()
    {

        SceneManager.LoadScene("GamePlayScene_backup 1"); // Название твоей сцены с игрой
    }

    // Информация о игре
    public void OpenAbout()
    {
        SceneManager.LoadScene("About us"); // О нас
    }
    public void CloseAbout()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Выход из игры
    public void ExitGame()
    {
        Application.Quit();
    }
}
