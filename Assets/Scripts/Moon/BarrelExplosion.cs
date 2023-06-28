using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarrelExplosion : Entity
{
    public Transform hero;
    public GameObject barrel;

    public GameObject Explosion;
    public GameObject SquareExplosion;
    private GameObject[] Explosions;

    private GameObject[] Squares;


    private void Awake()
    {
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
        if (collision.gameObject.CompareTag("Suricsan"))
        {
            Invoke("Boom", 2);
        }

    }

    void Boom()
    {
        if (Mathf.Abs(hero.transform.position.x - barrel.transform.position.x) < 5)
        {
            Hero.Instance.GetDamage();
        }

        if (Squares.Length>0)
            for (int i = 0; i< Squares.Length; i++)
            {
                if (Mathf.Abs(Squares[i].transform.position.x - barrel.transform.position.x) < 5)
                {
                    Destroy(Squares[i]);
                    Instantiate(SquareExplosion);
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
}
