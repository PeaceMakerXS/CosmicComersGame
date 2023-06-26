using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private GameOver gameManager;

    private void Start()
    {
        // Получаем ссылку на GameManagerScript
        gameManager = GameObject.Find("GameManager").GetComponent<GameOver>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Логика, когда самолетик сталкивается с врагом
            Debug.Log("Самолетик столкнулся с врагом!");
            gameManager.EndGame(); // Вызов метода EndGame() для завершения игры
        }
    }
}
