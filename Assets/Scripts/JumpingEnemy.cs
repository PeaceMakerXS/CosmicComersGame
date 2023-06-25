using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEditor.SearchService;

public class JumpingEnemy : Entity
{
    [SerializeField] protected float jumpforce;
    [SerializeField] protected int lives;

    protected SpriteRenderer sprite;
    protected Rigidbody2D rb;

    protected bool IsGrounded = false;
    public static JumpingEnemy Instance { get; set; }


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        Instance = this;
    }

    protected virtual void FixedUpdate()
    {
        CheckGround();
    }

    protected virtual void Update()
    {
        if (IsGrounded)
            Jump();
        if (lives == 0)
            Die();
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