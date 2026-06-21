using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner Instance;

    [Header("Platform Prefabs")]
    public GameObject platformPrefab;
    public GameObject fadePrefab;
    public GameObject movingPrefab;
    public GameObject movingPrefab2;
    public GameObject ShipsPrefab;
    public GameObject TriggerPrefab;
    public GameObject PlatformHalfShipsPrefab;
    public GameObject PlatformHalfShipsPrefab2;

    public Transform player;

    [Header("Distance & Offset")]
    public float baseDistance = 2.4f;
    public float offset = 0.8f;
    public float minDistance = 1.5f;
    public float maxDistance = 3f;

    [Header("X Range")]
    public float minX = -2.2f;
    public float maxX = 2.2f;

    [Header("Spawn Ahead")]
    public float spawnAhead = 8f;

    [Header("Danger Chance (общая сложность)")]
    [Range(0f, 1f)] public float dangerChance = 0.35f;

    [Header("Special Chances")]
    [Range(0f, 0.2f)] public float triggerChance = 0.03f; // 🔥 редкая платформа

    private float lastY = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < 8; i++)
            Spawn();
    }

    void Update()
    {
        if (player.position.y + spawnAhead > lastY)
            Spawn();
    }

    void Spawn()
    {
        GameObject prefab = GetRandomPlatform();

        float distance = Random.Range(baseDistance - offset, baseDistance + offset);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        Vector3 pos = new Vector3(
            Random.Range(minX, maxX),
            lastY + distance,
            0f
        );

        Instantiate(prefab, pos, Quaternion.identity);
        lastY = pos.y;
    }

    GameObject GetRandomPlatform()
    {
        float rand = Random.value;

        // 🔴 1) Сначала проверяем Trigger (редкий ивент)
        if (rand < triggerChance)
            return TriggerPrefab;

        rand -= triggerChance;

        // 🔥 2) Опасные платформы
        if (rand < dangerChance * 0.25f) return movingPrefab;
        if (rand < dangerChance * 0.45f) return movingPrefab2;
        if (rand < dangerChance * 0.65f) return fadePrefab;
        if (rand < dangerChance * 0.85f) return ShipsPrefab;
        if (rand < dangerChance * 0.50f) return PlatformHalfShipsPrefab;
        if (rand < dangerChance * 0.55f) return PlatformHalfShipsPrefab2;

        // 🟢 3) Обычная платформа
        return platformPrefab;
    }

    public void SetDifficulty(float baseMultiplier, float progress)
    {
        baseDistance = Mathf.Lerp(1.7f, 2f, baseMultiplier);
        offset = Mathf.Lerp(0.6f, 1.2f, baseMultiplier);

        dangerChance = Mathf.Lerp(0.25f, 0.6f, baseMultiplier)
                     + Mathf.Lerp(0f, 0.25f, progress);

        dangerChance = Mathf.Clamp(dangerChance, 0f, 0.8f);

        if (DifficultySettings.difficultyMultiplier < 1.5f) // Easy
        {
          baseDistance = 2f;
          offset = 0.6f;
          dangerChance = 0.15f; // меньше опасных платформ
        }
        else // Hard / Classic
        {
    baseDistance = Mathf.Lerp(2f, 2.2f, baseMultiplier);
    offset = Mathf.Lerp(0.6f, 1.2f, baseMultiplier);
    dangerChance = Mathf.Lerp(0.25f, 0.6f, baseMultiplier) + Mathf.Lerp(0f, 0.25f, progress);
       }
    }
}
