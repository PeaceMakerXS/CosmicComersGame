using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassEnemy : JumpingEnemy
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
        jumpforce = 3f;
        lives = 2;
    }

    protected override void Update()
    {
        base.Update();

        if (IsGrounded)
        {
            State = States.idle;
        }

        if (lives == 0)
        {
            State = States.death;
            Invoke("Die", 1);
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
        Debug.Log("GrassEnemy:" + lives.ToString());
    }

    protected override void CheckGround()
    {
        base.CheckGround();

        if (!IsGrounded)
        {
            State = States.jump;
        }
    }

    public enum States
    {
        idle,
        jump,
        death
    }
}
