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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lives = 5;
    }

    void Update()
    {
        Move();
    }

    private IEnumerator OnHit()
    {
        sprite.color = new Color(EarthLevelConstants.EnemyHitColors.Bee.firstColor,
            EarthLevelConstants.EnemyHitColors.Bee.secondColor,
            EarthLevelConstants.EnemyHitColors.Bee.thirdColor);
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
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
        StartCoroutine(OnHit());

        if (lives < 1)
        {
            Die();
        }
    }
}
