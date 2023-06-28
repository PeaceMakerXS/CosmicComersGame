using UnityEngine;

public class SnakeEnemy : Entity
{
    protected SpriteRenderer sprite;
    private Animator _animator;
    private Collider2D _collider;

    protected void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    public override void Die()
    {
        _collider.isTrigger = true;
        _animator.SetTrigger("death");
    }

}
