using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Массив префабов препятствий
    public GameObject[] obstaclePrefabsDown;
    public float spawnInterval = 1f; // Интервал спавна препятствий
    public float despawnOffset = 1f; // Отступ для удаления препятствий

    public float offset;
    private Camera mainCamera;
    private float cameraWidth;
    private NIksHero player;

    private void Start()
    {
        offset = 6f;
        mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;

        player = FindObjectOfType<NIksHero>();

        StartCoroutine(SpawnObstacleRoutine());
    }

    private IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObstacle()
    {
        float cameraHeight = mainCamera.orthographicSize;

        float spawnX = player.transform.position.x + cameraWidth+offset;
        float spawnY = Random.Range(-cameraHeight, cameraHeight);
        //Vector2 spawnPosition = new Vector2(spawnX, -1f);

        GameObject randomPrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject randomPrefabDown = obstaclePrefabsDown[Random.Range(0, obstaclePrefabsDown.Length)];

        if (randomPrefab == obstaclePrefabs[0])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -0.7f);
            GameObject obstacle = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        }
        else if (randomPrefab == obstaclePrefabs[1])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -0.65f);
            GameObject obstacle = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        }
        else if (randomPrefab == obstaclePrefabs[2])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -0.1f);
            GameObject obstacle = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        }
        else if (randomPrefab == obstaclePrefabs[3])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -2.3f);
            GameObject obstacle = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        }

////////////////////////////////////////////Down///////////////////////////////////////////////////////////
        
        if (randomPrefabDown == obstaclePrefabsDown[0])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -4f);
            GameObject obstacle = Instantiate(randomPrefabDown, spawnPosition, Quaternion.identity);
        }
        else if (randomPrefabDown == obstaclePrefabsDown[1])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -4f);
            GameObject obstacle = Instantiate(randomPrefabDown, spawnPosition, Quaternion.identity);
        }
        else if (randomPrefabDown == obstaclePrefabsDown[2])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -4f);
            GameObject obstacle = Instantiate(randomPrefabDown, spawnPosition, Quaternion.identity);
        }
        else if (randomPrefabDown == obstaclePrefabsDown[3])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -4f);
            GameObject obstacle = Instantiate(randomPrefabDown, spawnPosition, Quaternion.identity);
        }
    }

    private void Update()
    {
        float playerX = player.transform.position.x;

        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.transform.position.x < (playerX - cameraWidth - despawnOffset))
            {
                Destroy(obstacle);
            }
        }
        
    }
}
