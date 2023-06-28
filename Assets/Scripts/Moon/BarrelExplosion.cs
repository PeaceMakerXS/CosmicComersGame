using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class BarrelExplosion : Entity
{
    public Transform hero;
    public GameObject barrel;
    private SpriteRenderer sprite;

    public GameObject Explosion;
    public GameObject SquareExplosion;
    private GameObject[] Explosions;

    private GameObject[] Squares;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (!hero)
            hero = FindObjectOfType<Hero>().transform;
    }

    private void Update()
    {
        Squares = GameObject.FindGameObjectsWithTag("Square");
        Explosions = GameObject.FindGameObjectsWithTag("Explosion");
        CheckExpl();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Invoke("Boom", 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suricsan"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(GetHit());
            Invoke("Boom", 1);
        }
    }

    void Boom()
    {
        if (Mathf.Abs(hero.transform.position.x - barrel.transform.position.x) < 3)
        {
            Hero.Instance.GetDamage();
        }

        if (Squares.Length>0)
            for (int i = 0; i< Squares.Length; i++)
            {
                if (Mathf.Abs(Squares[i].transform.position.x - barrel.transform.position.x) < 5)
                {
                    var expl = Instantiate(SquareExplosion);
                    expl.transform.localPosition = new Vector3(Squares[i].transform.position.x, Squares[i].transform.position.y, 0);
                    Destroy(Squares[i]);
                }
            }

        var explosionRef = Instantiate(Explosion);
        explosionRef.transform.localPosition = new Vector3(barrel.transform.position.x, barrel.transform.position.y, barrel.transform.position.z);
        Destroy(this.gameObject);
    }

    private void CheckExpl()
    {
        if(Explosions.Length >5)
        {
            Destroy(Explosions[0]);
        }
    }

    private IEnumerator GetHit()
    {
        sprite.color = Color.magenta;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
