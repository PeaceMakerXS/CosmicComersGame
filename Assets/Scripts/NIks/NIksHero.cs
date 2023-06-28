using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NIksHero : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;
    [SerializeField] private float health;
    [SerializeField] private float lives;

    [SerializeField] private GameObject losePanel;

    private EdgeCollider2D edgeCollider;
    private Weapon2 govno;
    public static NIksHero Instance { get; set;}

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
        // Получаем ссылку на компонент Edge Collider 2D на объекте игрока
        edgeCollider = GetComponent<EdgeCollider2D>();
        govno = FindAnyObjectByType<Weapon2>();
    }


    private void Awake()
    {
        Instance = this;
        lives = 3;
        health = lives;
        
        losePanel.SetActive(false);



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.CompareTag("Bullet"));
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.tag == "Bullet")
        {
            health--;
            lives--;
            // Логика, когда самолетик сталкивается с врагом
            Debug.Log("Самолетик столкнулся с врагом!");
            if (health < 0)
            {
                health = 0;
                // Здесь можно вызвать метод для завершения игры
                // gameManager.EndGame();
            }
            if(health == 0)
            {
                losePanel.SetActive(true);
                Time.timeScale = 0;
            }

            for (int i =0; i < hearts.Length; i++)
            {
                if (i < health)
                    hearts[i].sprite = aliveHeart;
                else
                    hearts[i].sprite = deadHeart;

                if (i < lives)
                    hearts[i].enabled = true;
                else
                    hearts[i].enabled = false;
            }   

            if (!isInvincible)
            {
                StartCoroutine(BlinkingRoutine());
                StartCoroutine(InvincibilityRoutine());
                StartCoroutine(PassThroughObstaclesRoutine());
                edgeCollider.enabled = false;
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
        

        yield return new WaitForSeconds(invincibilityDuration);
        edgeCollider.enabled = true;
        
    }

    private IEnumerator PassThroughObstaclesRoutine()
    {
        canPassThroughObstacles = true;

        yield return new WaitForSeconds(invincibilityDuration);

        canPassThroughObstacles = false;
    }


    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            govno.Shoot(); // Вызываем метод стрельбы
        }

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
