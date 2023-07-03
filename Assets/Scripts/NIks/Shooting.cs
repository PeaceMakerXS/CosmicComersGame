using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка выстрела пули
    public float bulletSpeed = 10f; // Скорость полета пули
    public float maxDistance = 10f; // Максимальное расстояние, на котором пуля будет удалена
    

    private void Update()
    {
        // Проверяем, нажата ли кнопка стрельбы (в данном случае используем клавишу пробел)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(); // Вызываем метод стрельбы
        }
    }

    private void Shoot()
    {
        // Создаем экземпляр пули из префаба
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Получаем компонент Rigidbody2D пули
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        // Задаем скорость движения пули
        bulletRigidbody.velocity = transform.right * bulletSpeed;

        Vector2 startPoint = firePoint.position;
            
        if (Vector2.Distance(transform.position, startPoint) >= maxDistance)
        {
            Destroy(bullet); // Удаление пули, если она улетела достаточно далеко
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            Destroy(bullet); // Удаление пули при столкновении с объектом
        }
    }*/

    
}
