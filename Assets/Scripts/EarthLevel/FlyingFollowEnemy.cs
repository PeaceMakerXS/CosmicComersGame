using System.Collections;
using UnityEngine;

public class FlyingFollowEnemy : Entity
{
    [SerializeField] private float speed = 2.5f;

    private Transform player;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lives = 4;
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

    public override void GetDamage(int damage)
    {
        lives-=damage;
        StartCoroutine(OnHit());

        if (lives < 1)
        {
            Die();
        }
    }
}
