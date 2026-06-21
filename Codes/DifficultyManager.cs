using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    [Header("References")]
    public PlatformSpawner platformSpawner;
    public BonusSpawner bonusSpawner;
    public PlayerMovement player;

    [Header("Audio")]
    public AudioSource levelSound;

    [Header("LevelUp Texts")]
    public GameObject[] levelUpTexts;
    public float levelTextTime = 12f;

    public int difficultyLevel { get; private set; } = 0;
    public float difficultyLevelProgress = 0f;

    void Awake()
    {
        Instance = this;
        levelSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        DifficultySettings.Load();
    }

    public void UpdateDifficulty(int score)
    {
        // ==== Уровни сложности для LevelUp ====
        if (score >= 300 && difficultyLevel == 0) { LevelUp(1); }
        else if (score >= 700 && difficultyLevel == 1) { LevelUp(2); }
        else if (score >= 1000 && difficultyLevel == 2) { LevelUp(3); }
        else if (score >= 1500 && difficultyLevel == 3) { LevelUp(4); }
        else if (score >= 2000 && difficultyLevel == 4) { LevelUp(5); }
        else if (score >= 2500 && difficultyLevel == 5) { LevelUp(6); }

        // ==== Прогресс сложности по очкам ====
        difficultyLevelProgress = Mathf.Clamp(score / 2000f, 0f, 1f);

        // ==== Передаём платформам и бонусам ====
        if (platformSpawner != null)
            platformSpawner.SetDifficulty(DifficultySettings.difficultyMultiplier, difficultyLevelProgress);

        if (bonusSpawner != null)
            bonusSpawner.SetBonusDifficulty(DifficultySettings.difficultyMultiplier, difficultyLevelProgress);
    }

    void LevelUp(int level)
    {
        difficultyLevel = level;
        if (levelSound != null) levelSound.Play();
        if (levelUpTexts.Length >= level && levelUpTexts[level - 1] != null)
        {
            levelUpTexts[level - 1].SetActive(true);
            Invoke(nameof(HideLevelText), levelTextTime);
        }

        // Увеличиваем скорость игрока
        player.speed += 0.4f * DifficultySettings.difficultyMultiplier;
        if (player.TryGetComponent(out BetterJump bj))
        {
            bj.fallMultiplier += 0.3f * DifficultySettings.difficultyMultiplier;
            bj.lowJumpMultiplier += 0.2f * DifficultySettings.difficultyMultiplier;
        }
    }

    void HideLevelText()
    {
        foreach (var txt in levelUpTexts)
            txt.SetActive(false);
    }
}
