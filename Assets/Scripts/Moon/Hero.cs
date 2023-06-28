using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Hero : Entity
{
    [SerializeField] private int lives = 3;
    [SerializeField] private float jumpforce = 10f;
    private bool IsGrounded = false;

    public int score = 0;
    bool dead = false;

    private bool isMoving = false;
    [SerializeField] float MoveSpeed = 23f;

    Vector3 startPos;
    Vector3 endPos;

    private Rigidbody2D rb;
    public SpriteRenderer sprite;
    private Animator anim;

    public static Hero Instance { get; set;}

    private GameObject[] Squares;

    public Weapon gun;

    private CosmicStaes State
    {
        get { return (CosmicStaes)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike")) GetDamage();

        foreach (GameObject square in Squares)
        {
            if (square.GetComponent<Collider2D>().bounds.Intersects(collision.collider.bounds))
            {
                if (square.transform.position.y > transform.position.y)
                {
                    GetDamage();

                }
                if (square.transform.position.x > transform.position.x + 1)
                {
                    startPos = transform.position;
                    endPos = startPos - transform.right * 2;
                    StartCoroutine(Move(startPos, endPos));
                }
            }
        }

        if (collision.gameObject.CompareTag("Barrel"))
        {
            startPos = transform.position;
            endPos = startPos - transform.right * 2;
            StartCoroutine(Move(startPos, endPos));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();

        Instance = this;
    }

    private void FixedUpdate()
    {
        Squares = GameObject.FindGameObjectsWithTag("Square");
        gun = FindAnyObjectByType<Weapon>();
        CheckGround();
    }

    private void Update()
    {
        if (!dead)
        {
            if (!IsGrounded && !isMoving)
                State = CosmicStaes.jump;

            if (IsGrounded && !isMoving)
            {
                State = CosmicStaes.idle;
                Jump();
            }
            if (Input.GetButtonDown("Jump") && !isMoving)
            {
                State = CosmicStaes.move;
                startPos = transform.position;
                endPos = startPos + transform.right * 3;

                StartCoroutine(Move(startPos, endPos));
                gun.Shoot();
            }
            if (transform.position.y < -100)
                Die();
        }
    }

    private IEnumerator Move(Vector3 startPos, Vector3 endPos)
    {
        if (isMoving) yield break;
        isMoving = true;

        float distanceCovered = 0f;

        while (distanceCovered < Vector3.Distance(startPos, endPos))
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, MoveSpeed * Time.deltaTime);
            distanceCovered = Vector3.Distance(startPos, transform.position);

            yield return null;
        }

        isMoving = false;
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpforce;
        isMoving= false;
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        IsGrounded = collider.Length > 1;
    }
    public override void GetDamage()
    {
        lives --;
        Debug.Log(lives);
        if (!dead) { StartCoroutine(GetHit()); }
        if (lives <= 0)
        {
            dead= true;
            State = CosmicStaes.dead;
        }
    }
    private IEnumerator GetHit()
    {
        sprite.color = Color.magenta;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }

}

public enum CosmicStaes
{
    idle,
    move,
    jump,
    dead
}