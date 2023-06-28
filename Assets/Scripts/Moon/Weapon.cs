using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity
{
    public GameObject[] Bullets;
    public Transform shotPoint;

    private List<GameObject> Weapons = new List<GameObject>();

    public void Shoot()
    {
        var bullet = Instantiate(Bullets[0], shotPoint);
        Weapons.Add(bullet);
    }
    public void Update()
    {
        if (Weapons.Count > 0)
        {
            for (int i = 0; i< Weapons.Count; i++)
            {
                if (Hero.Instance.transform.position.x < Weapons[i].transform.position.x - 10f)
                {
                    Destroy(Weapons[i]);
                    Weapons.RemoveAt(i);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (GameObject weapon in Weapons) //не считает коллизию
        {
            if (collision.gameObject.CompareTag("Square") && collision.gameObject.CompareTag("Suricsan"))
            {
                Debug.Log("Удаляем))))))))))");
                Destroy(weapon);
                Weapons.Remove(weapon);
                break;
            }
        }
    }
}
