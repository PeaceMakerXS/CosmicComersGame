using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NIksHero : MonoBehaviour
{
    public float speed = 5f; // Скорость движения самолетика
    public float speed2 = 10f;
    private float minY; 
    private float maxY;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        minY = -0.5f;
        maxY = 7.5f;
    }

    private void Awake()
    {
            
    }

    private void Update()
    {
        // Получаем ввод от пользователя по горизонтали и вертикали
        //float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Создаем вектор направления на основе ввода
        Vector2 movement = new Vector2(speed, moveVertical*speed2);

        // Нормализуем вектор направления, чтобы самолетик не двигался быстрее по диагонали
        //movement.Normalize();

        // Применяем силу движения к Rigidbody2D
        rb.velocity = movement;
        
        if ((transform.position.y > maxY)){
            transform.position = new Vector2(transform.position.x, maxY);
        }
        if (transform.position.y < minY) {
            transform.position = new Vector2(transform.position.x, minY);
        }

    }
}
