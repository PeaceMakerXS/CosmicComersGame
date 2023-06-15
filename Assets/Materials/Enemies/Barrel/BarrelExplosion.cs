using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : Entity
{
    public GameObject hero;
    public GameObject barrel;

    public GameObject Explosion;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Invoke("Test", 3);
            Debug.Log("вот и пришел тот час...");
        }
    }

    void Test()
    {
        if (hero.transform.position.x - barrel.transform.position.x < 5)
        {
            Hero.Instance.GetDamage();
            Debug.Log("вот и пришел тот час 2...");
        }

        var explosionRef = Instantiate(Explosion);
        explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Destroy(this.gameObject);
    }
}
