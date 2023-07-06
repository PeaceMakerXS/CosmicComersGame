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
            foreach (var weapon in Weapons)
            {
                if (weapon)
                {
                    if (Hero.Instance.transform.position.x < weapon.transform.position.x - 10f)
                    {
                        Destroy(weapon);
                        Weapons.Remove(weapon);
                        break;
                    }
                }
            }
        }
    }
}
