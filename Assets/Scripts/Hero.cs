using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : Entity
{
    [SerializeField] private int lives = 3;
    [SerializeField] private float jumpforce = 10f;
    private bool IsGrounded = false;

    public int score = 0;

    private bool isMoving = false;
    [SerializeField] float MoveDistance = 3f;
    [SerializeField] float MoveSpeed = 23f;

    Vector3 startPos;
    Vector3 endPos;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    public static Hero Instance { get; set;}

    private CosmicStaes State
    {
        get { return (CosmicStaes)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Столкновение");
            startPos = transform.position;
            endPos = startPos - transform.right * 3;

            // Перемещаем игрока в обратном направлении
            if (!isMoving)
                StartCoroutine(Move(startPos, endPos));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        Instance = this;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        Debug.Log(isMoving);
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
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        IsGrounded = collider.Length > 1;
    }

    public override void GetDamage()
    {
        lives --;
        //Debug.Log(lives);
    }
}

public enum CosmicStaes
{
    idle,
    move,
    jump
}