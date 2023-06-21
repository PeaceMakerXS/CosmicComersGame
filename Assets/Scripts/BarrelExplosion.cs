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

    private GameObject explosionRef;

    private void Awake()
    {
        if (!hero)
            hero = FindObjectOfType<Hero>().transform;
    }

    private void Update()
    {
        
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

        explosionRef = Instantiate(Explosion,explousionParent);
        explosionRef.transform.localPosition = new Vector3(barrel.transform.position.x, barrel.transform.position.y, barrel.transform.position.z);
        Explosions.Add(explosionRef); //они не добавляются????
        Debug.Log(Explosions.Count);
        Destroy(this.gameObject);
        Invoke("CheckExpl", 1);
    }

    private void CheckExpl()
    {
        Destroy(explosionRef);

        Debug.Log("ну должно!");
        Destroy(Explosions[0]);
        Explosions.RemoveAt(0);

    }
}
