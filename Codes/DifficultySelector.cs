using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;
    public Button classicButton;

    public Image easyCheck;
    public Image normalCheck;
    public Image hardCheck;
    public Image classicCheck;

    private void Start()
    {
        // Скрываем все галочки
        HideAllChecks();

        // Загружаем сохранённый режим
        string savedMode = PlayerPrefs.GetString("DifficultyMode", "Normal");
        switch(savedMode)
        {
            case "Easy": SelectEasy(); break;
            case "Normal": SelectNormal(); break;
            case "Hard": SelectHard(); break;
            case "Classic": SelectClassic(); break;
        }

        // Подписка на кнопки
        easyButton.onClick.AddListener(SelectEasy);
        normalButton.onClick.AddListener(SelectNormal);
        hardButton.onClick.AddListener(SelectHard);
        classicButton.onClick.AddListener(SelectClassic);
    }

    void HideAllChecks()
    {
        easyCheck.enabled = false;
        normalCheck.enabled = false;
        hardCheck.enabled = false;
        classicCheck.enabled = false;
    }

    public void SelectEasy()
    {
        HideAllChecks();
        easyCheck.enabled = true;
        PlayerPrefs.SetString("DifficultyMode", "Easy");
        PlayerPrefs.SetFloat("DifficultyMultiplier", 1.1f);
        PlayerPrefs.Save();
    }

    public void SelectNormal()
    {
        HideAllChecks();
        normalCheck.enabled = true;
        PlayerPrefs.SetString("DifficultyMode", "Normal");
        PlayerPrefs.SetFloat("DifficultyMultiplier", 1.5f);
        PlayerPrefs.Save();
    }

    public void SelectHard()
    {
        HideAllChecks();
        hardCheck.enabled = true;
        PlayerPrefs.SetString("DifficultyMode", "Hard");
        PlayerPrefs.SetFloat("DifficultyMultiplier", 3f);
        PlayerPrefs.Save();
    }

    public void SelectClassic()
    {
        HideAllChecks();
        classicCheck.enabled = true;
        PlayerPrefs.SetString("DifficultyMode", "Classic");
        PlayerPrefs.SetFloat("DifficultyMultiplier", 2f); // можешь менять отдельно
        PlayerPrefs.Save();
    }
}
