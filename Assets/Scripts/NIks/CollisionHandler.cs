using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;
    [SerializeField] private float health;
    [SerializeField] private float lives;

    //private GameOver gameManager;

    private void Start()
    {
        // Получаем ссылку на GameManagerScript
        //gameManager = GameObject.Find("GameManager").GetComponent<GameOver>();
    }

    private void Awake()
    {
        lives = 3;
        health = lives;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.CompareTag("Bullet"));
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Bullet"))
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
            if(health> lives)
                health=lives;

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
        }


    }
}
