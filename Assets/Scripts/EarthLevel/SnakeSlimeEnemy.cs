using UnityEngine;

public class SnakeSlimeEnemy : Entity
{
    [SerializeField] private int lives;

    private Animator _animator;
    private Collider2D _collider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        lives = 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lives > 0 && collision.gameObject.CompareTag("Player"))
        {
            GetDamage();
        }
    }

    public override void GetDamage()
    {
        lives--;
        Debug.Log("SnakeEnemy:" + lives);

        if (lives < 1)
        {
            Die();
        }
    }

    public override void Die()
    {
        _collider.isTrigger = true;
        _animator.SetTrigger("death");
    }

}
