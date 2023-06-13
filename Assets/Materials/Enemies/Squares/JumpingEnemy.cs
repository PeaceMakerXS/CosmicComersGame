using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Entity
{
    [SerializeField] private float jumpforce = 17f;
    [SerializeField] private int lives = 3;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    private bool IsGrounded = false;
    public static JumpingEnemy Instance { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        Instance = this;
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpforce;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (IsGrounded)
            Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            //Die();
        }
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        IsGrounded = collider.Length > 1;
    }
}