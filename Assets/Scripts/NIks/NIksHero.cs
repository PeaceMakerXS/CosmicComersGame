using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NIksHero : MonoBehaviour
{

    public float invincibilityDuration = 2f; // Длительность неуязвимости после столкновения с препятствием
    public float blinkInterval = 0.2f; // Интервал между миганиями

    private bool isInvincible = false; // Флаг, указывающий, находится ли игрок в состоянии неуязвимости
    private bool canPassThroughObstacles = false; // Флаг, указывающий, может ли игрок пролетать сквозь препятствия
    private Renderer playerRenderer; // Компонент Renderer игрока   

    public float speed = 10f; // Скорость движения самолетика
    public float speed2 = 10f;
    private float minY; 
    private float maxY;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        minY = -4f;
        maxY = 4f;

        playerRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isInvincible)
            {
                StartCoroutine(BlinkingRoutine());
                StartCoroutine(InvincibilityRoutine());
                StartCoroutine(PassThroughObstaclesRoutine());
            }
        }
    }

    private IEnumerator BlinkingRoutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < invincibilityDuration)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        playerRenderer.enabled = true; // Включаем отображение игрока после окончания мигания
    }

    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    private IEnumerator PassThroughObstaclesRoutine()
    {
        canPassThroughObstacles = true;

        yield return new WaitForSeconds(invincibilityDuration);

        canPassThroughObstacles = false;
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
