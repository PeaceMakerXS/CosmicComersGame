using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int lives;
    public virtual void GetDamage(int damage)
    {
        lives -= damage;
        if (lives < 1)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
    
}
