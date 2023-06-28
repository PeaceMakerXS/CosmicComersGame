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
            else if ((randomPrefab == obstaclePrefabs[4]) && (randomPrefabDown != obstaclePrefabsDown[2]) && (randomPrefabDown != obstaclePrefabsDown[3])) 
        {
            Vector2 spawnPosition = new Vector2(spawnX, 2.75f);
            GameObject obstacle = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        }
            else if ((randomPrefab == obstaclePrefabs[5]) && (randomPrefabDown != obstaclePrefabsDown[2]) && (randomPrefabDown != obstaclePrefabsDown[3]))
        {
            Vector2 spawnPosition = new Vector2(spawnX, 2.75f);
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
        else if (randomPrefabDown == obstaclePrefabsDown[4])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -4f);
            GameObject obstacle = Instantiate(randomPrefabDown, spawnPosition, Quaternion.identity);
        }
        else if (randomPrefabDown == obstaclePrefabsDown[5])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -4f);
            GameObject obstacle = Instantiate(randomPrefabDown, spawnPosition, Quaternion.identity);
        }
        else if (randomPrefabDown == obstaclePrefabsDown[6])
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
/*

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

    private bool isRestarting = false; // Переменная для отслеживания перезапуска скрипта

    private bool isActive = true; // Флаг активности скрипта
    private Coroutine spawnRoutine; // Ссылка на корутину спавна препятствий
    private float timer = 0f; // Таймер для отслеживания времени

    private void Start()
    {
        offset = 6f;
        mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;

        player = FindObjectOfType<NIksHero>();

        StartSpawnRoutine(); // Запуск корутины спавна препятствий
    }

    private void StartSpawnRoutine()
    {
        spawnRoutine = StartCoroutine(SpawnObstacleRoutine());
    }

    private void StopSpawnRoutine()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private IEnumerator SpawnObstacleRoutine()
    {
        while (isActive)
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
            else if ((randomPrefab == obstaclePrefabs[4]) && (randomPrefabDown != obstaclePrefabsDown[2]) && (randomPrefabDown != obstaclePrefabsDown[3])) 
        {
            Vector2 spawnPosition = new Vector2(spawnX, 2.75f);
            GameObject obstacle = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        }
            else if ((randomPrefab == obstaclePrefabs[5]) && (randomPrefabDown != obstaclePrefabsDown[2]) && (randomPrefabDown != obstaclePrefabsDown[3]))
        {
            Vector2 spawnPosition = new Vector2(spawnX, 2.75f);
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
        else if (randomPrefabDown == obstaclePrefabsDown[4])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -4f);
            GameObject obstacle = Instantiate(randomPrefabDown, spawnPosition, Quaternion.identity);
        }
        else if (randomPrefabDown == obstaclePrefabsDown[5])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -4f);
            GameObject obstacle = Instantiate(randomPrefabDown, spawnPosition, Quaternion.identity);
        }
        else if (randomPrefabDown == obstaclePrefabsDown[6])
        {
            Vector2 spawnPosition = new Vector2(spawnX, -4f);
            GameObject obstacle = Instantiate(randomPrefabDown, spawnPosition, Quaternion.identity);
        }
        timer += spawnInterval;
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

        // Увеличиваем таймер каждый кадр
        timer += Time.deltaTime;

        // Если прошло 10 секунд, останавливаем скрипт
        if (timer >= 10f && isActive && !isRestarting)
        {
            StopSpawnRoutine();
            isActive = false;
            isRestarting = false; // Сбрасываем флаг перезапуска
            timer = 0f; // Сбрасываем таймер
        }
    }
    private IEnumerator RestartSpawnRoutine()
    {
        yield return new WaitForSeconds(10f); // Ждем 10 секунд

        StartSpawnRoutine(); // Запускаем скрипт заново
        isActive = true;
        isRestarting = false; // Сбрасываем флаг перезапуска
        timer = 0f; // Сбрасываем таймер
    }
}

*/