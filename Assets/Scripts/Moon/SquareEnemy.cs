using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemy : JumpingEnemy
{
    int k = 0;
    bool littlejump = false;
    private GameObject player;

    [SerializeField] float MoveSpeed = 23f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.x > player.transform.position.x+1)
                StartCoroutine(Move());
        }

        
    }
    private IEnumerator Move()
    {
        Debug.Log("NO_Yes");
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
