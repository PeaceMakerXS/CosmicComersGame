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
    new int lives = 2;
    public static JumpingEnemy Instance { get; set; }

    public GameObject Explosion;
    private GameObject[] Explosions;

    Suricsan suricsan;
    private void Start()
    {
        Instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Update()
    {
        base.Update();
        Explosions = GameObject.FindGameObjectsWithTag("Explosion");
        suricsan = FindAnyObjectByType<Suricsan>();

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
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.position.x > player.transform.position.x+1)
                StartCoroutine(Move());
        }               
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suricsan"))
        {
            Destroy(collision.gameObject);
            lives-= suricsan.damage;
            StartCoroutine(GetHit());
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
    private IEnumerator GetHit()
    {
        sprite.color = Color.magenta;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }
}
