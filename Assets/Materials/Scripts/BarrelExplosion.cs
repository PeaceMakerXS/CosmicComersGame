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
        Explosions.Add(explosionRef);
        Destroy(this.gameObject);
        Invoke("DestroyExpl", 2); //не вызывается
    }

    void DestroyExpl()
    {
        Debug.Log("а хули тогд");
        Destroy(Explosions[0]);
        Explosions.RemoveAt(0);
    }
}
