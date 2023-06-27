using UnityEngine;

public class DeathLine : MonoBehaviour
{
    private Entity deathEntity;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        deathEntity = collision.gameObject.GetComponent<Entity>();
        if (deathEntity)
        {
            deathEntity.Die();
        }
    }

}
