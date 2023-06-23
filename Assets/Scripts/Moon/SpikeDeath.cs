using UnityEngine;

public class SpikeDeath : Entity
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Die();
    }
}
