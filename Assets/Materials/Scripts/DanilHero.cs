using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DanilHero : DanilEntity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int health;
    [SerializeField] private float jumpForce = 7f;
    private bool isGrounded = false;

    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;

    public Joystick joystick;
    public static DanilHero Instance { get; set; }

    void Awake()
    {
        lives = 5;
        health = lives;
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {

        if (joystick.Horizontal != 0)
        {
            Move();
        }

        if (isGrounded & joystick.Vertical > 0.5f)
        {
            Jump();
        }

        if (health > lives)
        {
            health = lives;
        }
    }

    private void Move() 
    {
        Vector3 direction = transform.right * joystick.Horizontal;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0;
    }

    private void Jump()
    {
        rigidBody.velocity = Vector2.up * jumpForce;
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        isGrounded = colliders.Length > 1;
    }

    public override void GetDamage()
    {
        health--;
        Debug.Log(health);
    }
}
