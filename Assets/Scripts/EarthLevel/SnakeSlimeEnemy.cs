using UnityEngine;

public class SnakeSlimeEnemy : Entity
{
    [SerializeField] private int lives;

    private Animator anim;

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        lives = 5;
    }

    private void Update()
    {
        if (lives == 0)
        {
            State = States.death;
            Invoke("Die", 1);
        }

        else
        {
            State = States.idle;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetDamage();
        }
    }

    public override void GetDamage()
    {
        lives--;
        Debug.Log("SlimeSnakeEnemy:" + lives.ToString());
    }

    public enum States
    {
        idle,
        death
    }
}
