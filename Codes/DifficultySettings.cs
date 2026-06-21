using UnityEngine;

public static class DifficultySettings
{
    public static float difficultyMultiplier = 1f;
    public static bool classicMode = true;

    public static void SetClassic()
    {
        classicMode = true;
        difficultyMultiplier = 2f;

        PlayerPrefs.SetInt("ClassicMode", 1);
        PlayerPrefs.SetFloat("Difficulty", difficultyMultiplier);
    }

    public static void SetEasy()
    {
        classicMode = false;
        difficultyMultiplier = 1.1f;

        Save();
    }

    public static void SetNormal()
    {
        classicMode = false;
        difficultyMultiplier = 1.5f;

        Save();
    }

    public static void SetHard()
    {
        classicMode = false;
        difficultyMultiplier = 3f;

        Save();
    }

    static void Save()
    {
        PlayerPrefs.SetInt("ClassicMode", 0);
        PlayerPrefs.SetFloat("Difficulty", difficultyMultiplier);
    }

    public static void Load()
    {
        classicMode = PlayerPrefs.GetInt("ClassicMode", 1) == 1;
        difficultyMultiplier = PlayerPrefs.GetFloat("Difficulty", 1f);
    }
}
