using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : JumpingEnemy
{
    private Animator _animator;
    private Collider2D _collider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        jumpforce = 6f;
        lives = 2;
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

    private IEnumerator OnHit()
    {
        sprite.color = new Color(EarthLevelConstants.EnemyHitColors.SlimeBlock.firstColor,
            EarthLevelConstants.EnemyHitColors.SlimeBlock.secondColor,
            EarthLevelConstants.EnemyHitColors.SlimeBlock.thirdColor);
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }

    public override void GetDamage(int damage)
    {
        lives -= damage;
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
