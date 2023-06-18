using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public GameObject obstaclePrefab; // Префаб препятствия
    public float spawnInterval = 2f; // Интервал между появлением препятствий
    public float spawnRange = 4f; // Диапазон появления препятствий по оси Y

    private float timer;

    private void Update()
    {
        // Увеличиваем таймер
        timer += Time.deltaTime;

        // Если прошло достаточно времени, генерируем препятствие
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f; // Сбрасываем таймер
        }
    }

    private void SpawnObstacle()
    {
        // Вычисляем случайную позицию по оси Y
        float randomY = Random.Range(-spawnRange, spawnRange);

        // Создаем препятствие на заданной позиции
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
