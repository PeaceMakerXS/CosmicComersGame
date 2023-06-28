using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SquareEnemy : JumpingEnemy
{
    int k = 0;
    bool littlejump = false;
    private GameObject player;
    [SerializeField] float MoveSpeed = 23f;
    new int lives = 1;

    public static JumpingEnemy Instance { get; set; }

    public GameObject Explosion;
    private GameObject[] Explosions;

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
        Explosions = GameObject.FindGameObjectsWithTag("Explosion");

        CheckExpl();

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

        if (lives == 0) 
        { 
            Die();
            var explosionRef = Instantiate(Explosion);
            explosionRef.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
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
    private void CheckExpl()
    {
        if (Explosions.Length > 3)
        {
            Destroy(Explosions[0]);
        }
    }
}
