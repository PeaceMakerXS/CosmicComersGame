using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("GrassEnemy:" + lives);

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
