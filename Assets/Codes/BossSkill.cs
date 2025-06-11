using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public GameObject fallingObjectPrefab;
    public int numberOfObjects = 3;
    public float spawnHeight = 5f;
    public float horizontalSpread = 2f;
    public float cameraShakeDuration = 0.2f;
    public float cameraShakeMagnitude = 0.3f;

    private Transform player;
    private Camera mainCamera;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
    }

    
    public void OnJumpImpact()
    {
        StartCoroutine(ShakeCamera());

        SpawnFallingObjectsAbovePlayer();
    }

    void SpawnFallingObjectsAbovePlayer()
    {
        if (player == null || fallingObjectPrefab == null) return;

        for (int i = 0; i < numberOfObjects; i++)
        {
            float xOffset = Random.Range(-horizontalSpread, horizontalSpread);
            Vector3 spawnPos = new Vector3(
                player.position.x + xOffset,
                player.position.y + spawnHeight,
                0f
            );

            Instantiate(fallingObjectPrefab, spawnPos, Quaternion.identity);
        }
    }

    System.Collections.IEnumerator ShakeCamera()
    {
        Vector3 originalPos = mainCamera.transform.position;

        float elapsed = 0f;
        while (elapsed < cameraShakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * cameraShakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * cameraShakeMagnitude;

            mainCamera.transform.position = new Vector3(
                originalPos.x + offsetX,
                originalPos.y + offsetY,
                originalPos.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPos;
    }
}
