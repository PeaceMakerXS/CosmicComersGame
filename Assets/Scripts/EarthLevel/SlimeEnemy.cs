using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : JumpingEnemy
{
    private Animator anim;
    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        jumpforce = 6f;
        lives = 3;
    }

    protected override void Update()
    {
        base.Update();

        if (lives == 0)
        {
            State = States.death;
            Invoke("Die", 1);
        }

        if (IsGrounded)
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
        Debug.Log("SlimeEnemy:" + lives.ToString());
    }

    public enum States
    {
        idle,
        death
    }
}
