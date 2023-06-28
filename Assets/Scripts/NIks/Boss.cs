using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private NIksHero player;

    public float topBoundary = 2.5f; // Верхняя граница
    public float bottomBoundary = -2.5f; // Нижняя граница


    public float verticalMovementDistance = -2f; // Расстояние вертикального движения босса
    public float verticalMovementDuration = 2f; // Длительность вертикального движения босса
    public float verticalMovementInterval = 1f; // Интервал между вертикальными движениями босса


    private Camera mainCamera;
    private float cameraWidth;

    public float movementSpeed = 5f; // Скорость движения босса
    public int maxHealth = 100; // Максимальное количество здоровья босса

    public GameObject bulletPrefab; // Префаб пули
    public Transform bulletSpawnPoint; // Точка выстрела пули
    public float bulletSpeed = 10f; // Скорость полета пули

    public GameObject laserPrefab; // Префаб лазера
    public Transform laserSpawnPoint; // Точка выстрела лазера
    public float laserDuration = 2f; // Длительность существования лазера
    public float laserInterval = 5f; // Интервал между выстрелами лазера

    private int currentHealth; // Текущее количество здоровья босса
    private bool isSecondPhase = false; // Флаг, указывающий, находится ли босс во второй фазе
    Collision2D collision;

    /*private void Awake()
    {
        
    }*/

    private void Start()
    {
        player = FindObjectOfType<NIksHero>();

        mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
        

        currentHealth = maxHealth;
        StartCoroutine(ShootBulletsRoutine());
        StartCoroutine(PhaseTransitionRoutine());
        StartCoroutine(VerticalMovementRoutine());
        
        winPanel.SetActive(false);
    }

    private void Update()
    {
        // Двигаем босса вправо
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    private IEnumerator ShootBulletsRoutine()
    {
        while (currentHealth != 50)
        {
            // Стреляем пулей
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = -transform.right * bulletSpeed;

            float playerX = player.transform.position.x;
            // Удаляем пули, вышедшие за камеру
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (GameObject bulletObject  in bullets)
            {
                if (bulletObject.transform.position.x < (playerX - cameraWidth))
                {
                    Destroy(bulletObject );
                }
            }

            yield return new WaitForSeconds(1f); // Интервал между выстрелами пуль
        }
    }

    private IEnumerator ShootLaserRoutine()
    {
        while (currentHealth > 10)
        {
            // Помечаем область для лазера
            GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f); // Задержка перед выстрелом лазера

            // Выпускаем два лазера
            for (int i = 0; i < 2; i++)
            {
                GameObject laserInstance = Instantiate(laserPrefab, laserSpawnPoint.position, Quaternion.identity);
                laserInstance.transform.localScale = new Vector3(-1f, 1f, 1f); // Здесь изменено направление лазера
                Destroy(laserInstance, laserDuration);
            }

            yield return new WaitForSeconds(laserInterval); // Интервал между выстрелами лазера
        }
    }



    private IEnumerator PhaseTransitionRoutine()
    {
        yield return new WaitForSeconds(50f); // Время до перехода во вторую фазу
        isSecondPhase = true;

        StartCoroutine(ShootLaserRoutine());

        yield return new WaitForSeconds(40f); // Время до перехода в финальную фазу
        StartCoroutine(ShootLaserRoutine());
    }


    private IEnumerator VerticalMovementRoutine()
    {
        while (true)
        {
            // Перемещение вверх
            yield return MoveVertically(transform.position.y + verticalMovementDistance, verticalMovementDuration);

            // Перемещение вниз
            yield return MoveVertically(transform.position.y - verticalMovementDistance, verticalMovementDuration);

            yield return new WaitForSeconds(verticalMovementInterval);
        }
    }






    private IEnumerator MoveVertically(float targetY, float duration)
    {
        float elapsedTime = 0f;
        float startY = transform.position.y;

        while (elapsedTime < duration)
        {
            float newY = Mathf.Lerp(startY, targetY, elapsedTime / duration);
            newY = Mathf.Clamp(newY, bottomBoundary, topBoundary);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Дополнительная проверка, чтобы позиция не превышала нижнюю границу
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, bottomBoundary, topBoundary), transform.position.z);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            currentHealth -= 1;
            Debug.Log(currentHealth);
        }
        if (currentHealth <= 0)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void TakeDamage()
    {
        /*if (collision.CompareTag("Suricsan"))
        {
            currentHealth -= 1;
        }
        */


    }

    private void Die()
    {
        // Босс умер, выполните необходимые действия (например, активируйте победный экран)
        Destroy(gameObject);
    }
}
