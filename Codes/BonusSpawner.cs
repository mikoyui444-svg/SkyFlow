using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [Header("Bonus Chances")]
    public float classicChance = 0.1f;
    public float easyChance = 0.3f;
    public float normalChance = 0.08f;
    public float hardChance = 0.04f;
    public float progressMultiplier = 0.05f;

    public Transform player;

    public GameObject coinPrefab;
     public GameObject coinPrefab2;
    public GameObject shieldPrefab;
    public GameObject healthPrefab;
    public GameObject rocketBallPrefab;
    public GameObject bonusPrefab;

    private float lastSpawnY = 0f;
    public float spawnAhead = 8f;

    private float difficultyMultiplier = 1f;
    private float progress = 0f;

    public void SetBonusDifficulty(float baseMultiplier, float prog)
    {
        difficultyMultiplier = baseMultiplier;
        progress = prog;
    }

    void Update()
    {
        if (player.position.y + spawnAhead > lastSpawnY)
            SpawnBonus();
    }

    void SpawnBonus()
    {
        float spawnY = lastSpawnY + Random.Range(1.8f, 3f);
        float spawnX = Random.Range(-2f, 2f);
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        float chance = 0f;

        if (DifficultySettings.classicMode)
    chance = classicChance * progress;
        else if (difficultyMultiplier < 0.5f)
    chance = easyChance + progressMultiplier * progress;
        else if (difficultyMultiplier < 1.0f)
    chance = normalChance * progress;
        else
    chance = hardChance * progress;


        if (Random.value < chance)
        {
            int type = Random.Range(0, 6);
            GameObject prefab = type switch
            {
                0 => coinPrefab,
                1 => shieldPrefab,
                2 => healthPrefab,
                3 => rocketBallPrefab,
                4 => bonusPrefab,
                5 => coinPrefab2,
                6 => coinPrefab
            };
            Instantiate(prefab, spawnPos, Quaternion.identity);
        }

        lastSpawnY = spawnY;
    }
} 