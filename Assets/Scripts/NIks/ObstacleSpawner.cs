using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner: MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Массив префабов препятствий
    public float spawnInterval = 2f; // Интервал спавна препятствий
    public float obstacleSpeed = 5f; // Скорость движения препятствий

    private Transform playerTransform; // Ссылка на компонент Transform игрока

    private void Start()
    {
        playerTransform = FindObjectOfType<NIksHero>().transform;

        // Запускаем функцию SpawnObstacle через заданный интервал времени
        InvokeRepeating("SpawnObstacle", 0f, spawnInterval);
    }

    private void SpawnObstacle()
    {
        // Выбираем случайный индекс префаба препятствия
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        float playerX = playerTransform.transform.position.x;
        // Вычисляем позицию спавна на основе позиции игрока и случайного смещения
        Vector3 spawnPosition = new Vector3(playerX*1.5f, -1f);

        // Создаем препятствие из выбранного префаба на вычисленной позиции спавна
        GameObject obstacle = Instantiate(obstaclePrefabs[randomIndex], spawnPosition, Quaternion.identity);
        //obstacle.transform.position = new Vector3(playerX*4f, -1f);
        // Назначаем скорость движения препятствия
        obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(-obstacleSpeed, 0f);

        // Уничтожаем препятствие через некоторое время
        Destroy(obstacle, 4f);
    }
}
