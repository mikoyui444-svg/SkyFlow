using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public LayerMask platformLayer;

    public GameObject movingPrefab;
    public GameObject fadePrefab;
    public GameObject bonusPrefab;

    public void ReplacePlatforms()
    {
        Collider2D[] platforms = Physics2D.OverlapCircleAll(Vector2.zero, 10000f, platformLayer);

        foreach (Collider2D p in platforms)
        {
            Vector3 pos = p.transform.position;
            Destroy(p.gameObject);

            GameObject prefab = GetRandomPlatform();
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    GameObject GetRandomPlatform()
    {
        float r = Random.value;
        if (r < 0.4f) return movingPrefab;
        if (r < 0.7f) return fadePrefab;
        return bonusPrefab;
    }
}
