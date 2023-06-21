using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : Entity
{
    public Transform hero;
    public GameObject barrel;

    public GameObject Explosion;
    public Transform explousionParent;
    private List<GameObject> Explosions = new List<GameObject>();

    private void Awake()
    {
        if (!hero)
            hero = FindObjectOfType<Hero>().transform;
    }

    private void Update()
    {
        CheckExpl();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Invoke("Boom", 2);
            Debug.Log("вот и пришел тот час...");
        }
    }

    void Boom()
    {
        if (hero.transform.position.x - barrel.transform.position.x < 5)
        {
            Hero.Instance.GetDamage();
            Debug.Log("вот и пришел тот час 2...");
        }

        var explosionRef = Instantiate(Explosion,explousionParent);
        explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Explosions.Add(explosionRef); //они не добавляются????
        Destroy(this.gameObject);
    }

    private void CheckExpl()
    {
        Debug.Log("ну должно!");
        Debug.Log(Explosions.Count);
        if (Explosions.Count > 1)
        {
            Debug.Log("ну должно!2");
            Destroy(Explosions[0]);
            Explosions.RemoveAt(0);
        }
    }
}
