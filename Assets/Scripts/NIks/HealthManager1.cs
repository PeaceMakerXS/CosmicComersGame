using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3; // Максимальное количество жизней
    public GameObject heartPrefab; // Префаб сердечка
    public Transform heartsContainer; // Ссылка на контейнер сердечек

    private int currentHealth; // Текущее количество жизней

    private void Start()
    {
        currentHealth = maxHealth;

        // Создаем и отображаем сердечки
        for (int i = 0; i < maxHealth; i++)
        {
            Instantiate(heartPrefab, heartsContainer);
        }
    }

    // Удаление одной жизни
    public void RemoveLife()
    {
        if (currentHealth > 0)
        {
            currentHealth--;

            // Удаляем последнее сердечко из контейнера
            Destroy(heartsContainer.GetChild(currentHealth).gameObject);
        }
    }

    // Добавление одной жизни
    public void AddLife()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
        }
    }      
}