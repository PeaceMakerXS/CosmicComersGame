using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEditor.SearchService;

public class JumpingEnemy : Entity
{
    [SerializeField] protected float jumpforce;

    protected SpriteRenderer sprite;
    protected Rigidbody2D rb;

    protected bool IsGrounded = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void FixedUpdate()
    {
        CheckGround();
    }

    protected virtual void Update()
    {
        if (IsGrounded)
            Jump();
    }

    protected void Jump()
    {
        rb.velocity = Vector2.up * jumpforce;
    }

    protected virtual void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        IsGrounded = collider.Length > 1;
    }
}