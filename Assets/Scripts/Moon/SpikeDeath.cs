using UnityEngine;

public class SpikeDeath : Entity
{
    public GameObject Explosion;
    private GameObject[] Explosions;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
            var explosionRef = Instantiate(Explosion);
            explosionRef.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void Update()
    {
        Explosions = GameObject.FindGameObjectsWithTag("Explosion");
    }
}
