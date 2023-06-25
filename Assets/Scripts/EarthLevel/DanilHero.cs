using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DanilHero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private int health;
    
    private bool isGrounded = false;

    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private Animator _animator;

    public Joystick joystick;
    public static DanilHero Instance { get; set; }

    private States State
    {
        get { return (States)_animator.GetInteger("state"); }
        set { _animator.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        health = 20;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded)
        {
            State = States.idle;
        }

        if (joystick.Horizontal != 0)
        {
            Run();
        }

        if (isGrounded & joystick.Vertical > 0.5f)
        {
            Jump();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetDamage();
        }
    }
    private void Run() 
    {
        if (isGrounded)
        {
            State = States.run;
        }

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

        if (!isGrounded && rigidBody.velocity.y < 0)
        {
            State = States.fall;
        }
        else if (!isGrounded)
        {
            State = States.jump;
        }

    }

    public override void GetDamage()
    {
        health--;
        Debug.Log("Hero:" + health);

        if (health < 1)
        {
            Die();
        }
    }

    public override void Die()
    {
        State = States.death;
        base.Die();
    }
}

public enum States
{
    idle,
    run,
    jump,
    fall,
    death
}
