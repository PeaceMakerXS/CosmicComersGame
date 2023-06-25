using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GrassEnemy : JumpingEnemy
{
    private Animator _animator;
    private Collider2D _collider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        jumpforce = 3f;
        lives = 4;
    }

    protected override void FixedUpdate()
    {
        if (lives > 0)
        {
            CheckGround();
        }
    }

    protected override void Update()
    {
        if (lives > 0 && IsGrounded)
        {
            Jump();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lives > 0 && collision.gameObject.CompareTag("Player"))
        {
            GetDamage();
        }
    }

    private IEnumerator OnHit()
    {
        sprite.color = new Color(EarthLevelConstants.EnemyHitColors.GrassBlock.firstColor,
            EarthLevelConstants.EnemyHitColors.GrassBlock.secondColor,
            EarthLevelConstants.EnemyHitColors.GrassBlock.thirdColor);
        yield return new WaitForSeconds(0.3f);
        sprite.color = new Color(1f, 1f, 1f);
    }

    public override void GetDamage()
    {
        lives--;
        Debug.Log("GrassEnemy:" + lives);
        StartCoroutine(OnHit());

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
