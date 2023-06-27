using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemy : JumpingEnemy
{
    int k = 0;
    bool littlejump = false;
    private GameObject player;
    [SerializeField] float MoveSpeed = 23f;
    new int lives = 1;

    public static JumpingEnemy Instance { get; set; }

    Suricsan suricsan;
    private void Start()
    {
        Instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        suricsan = FindAnyObjectByType<Suricsan>();
    }

    protected override void Update()
    {
        base.Update();

        if (IsGrounded && !littlejump)
        {
            k++;
            if (k >= 3)
            {
                jumpforce = 3;
                littlejump = true;
                k = 0;
            }
        }
        if (IsGrounded && littlejump)
        {
            k++;
            if (k >= 3)
            {
                jumpforce = 10;
                littlejump = false;
                k = 0;
            }
        }

        if (lives == 0) { Die(); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suricsan")) 
        { 
            lives -= suricsan.damage; 
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.x > player.transform.position.x+1)
                StartCoroutine(Move());
        }               
    }
    private IEnumerator Move()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + transform.right * 3;

        float distanceCovered = 0f;

        while (distanceCovered < Vector3.Distance(startPos, endPos))
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, MoveSpeed * Time.deltaTime);
            distanceCovered = Vector3.Distance(startPos, transform.position);

            yield return null;
        }
    }
}
