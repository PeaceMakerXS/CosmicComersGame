using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFollowEnemy : Entity
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private int lives;

    private Transform player;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lives = 5;
    }

    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lives > 0 && collision.gameObject.CompareTag("Player"))
        {
            GetDamage();
        }
    }

    private void Move()
    {
        if (player)
        {
            float lastPositionX = transform.position.x;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            sprite.flipX = (lastPositionX - transform.position.x) > 0;
        }
    }

    public override void GetDamage()
    {
        lives--;
        Debug.Log("FlyingEnemy:" + lives);

        if (lives < 1)
        {
            Die();
        }
    }
}
