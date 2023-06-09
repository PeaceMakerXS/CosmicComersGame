using System.Collections;
using UnityEngine;


public class DanilHero : Entity
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] public int health;

    [SerializeField] private float attackRange;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackAnimationDurarion;
 
    public int coinsCollected;
    public int suitPartsCollected;

    private bool isGrounded;
    private bool isAttacking;
    private bool isRecharged;

    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private Animator _animator;
    private Vector3 direction;

    public LayerMask enemy;
    public Transform attackPosition;
    public Joystick joystick;
    public static DanilHero Instance { get; set; }

    public States State
    {
        get { return (States)_animator.GetInteger("state"); }
        set { _animator.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
    }

    private void Start()
    {
        speed = EarthLevelConstants.Player.initialSpeed;
        jumpForce = EarthLevelConstants.Player.initialJumpForce;
        health = EarthLevelConstants.Player.initialHealth;

        attackRange = EarthLevelConstants.Player.initialAttackRange;
        attackCoolDown = EarthLevelConstants.Player.initialAttackCoolDown;
        attackDamage = EarthLevelConstants.Player.initialAttackDamage;
        attackAnimationDurarion = EarthLevelConstants.Player.attackAnimationDuration;


        coinsCollected = 0;
        suitPartsCollected = 0;

        isAttacking = false;
        isRecharged = true;

        direction = transform.right;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (health > 0)
        {
            if (!isAttacking && isGrounded)
            {
                State = States.idle;
                if (joystick.Vertical > 0.5f)
                {
                    Jump();
                }
            }

            if (!isAttacking && joystick.Horizontal != 0)
            {
                Run();
            }
            /*
            if (Input.GetButton("Jump"))
            {
                Attack();
            }*/
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy"){
            GetDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        switch (gameObject.tag)
        {
            case "Coin":
                coinsCollected++;
                Destroy(gameObject); 
                break;

            case "GravitationSuite":
                suitPartsCollected++;
                Destroy(gameObject);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    private void Run() 
    {
        if (isGrounded)
        {
            State = States.run;
        }

        direction = transform.right * joystick.Horizontal;

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

        if (!isAttacking && !isGrounded && rigidBody.velocity.y < 0)
        {
            State = States.fall;
        }

        else if (!isAttacking && !isGrounded)
        {
            State = States.jump;
        }
    }

    public void Attack()
    {
        if (isRecharged)
        {
            isAttacking = true;
            isRecharged = false;
            State = States.attack;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++)
        {
            float enemyXCoord = colliders[i].gameObject.transform.position.x;
            if ((direction.x < 0 && enemyXCoord <= transform.position.x) || (direction.x > 0 && enemyXCoord >= transform.position.x))
            {
                colliders[i].GetComponent<Entity>().GetDamage(attackDamage);
            }
        }
    }

    private IEnumerator OnHit()
    {
        sprite.color = new Color(EarthLevelConstants.Player.HitColors.firstColor,
            EarthLevelConstants.Player.HitColors.secondColor,
            EarthLevelConstants.Player.HitColors.thirdColor);
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(attackAnimationDurarion);
        OnAttack();
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        isRecharged = true;
    }

    public override void GetDamage(int damage)
    {
        health -= damage;
        StartCoroutine(OnHit());

        if (health < 1)
        {
            Die();
        }
    }

    public override void Die()
    {
        State = States.death;
    }
}

public enum States
{
    idle,
    run,
    jump,
    fall,
    death,
    attack
}
