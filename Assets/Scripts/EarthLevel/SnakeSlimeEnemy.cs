using System.Collections;
using UnityEngine;

public class SnakeSlimeEnemy : SnakeEnemy
{
    protected override void Start()
    {
        base.Start();
        lives = 2;
    }

    public override void GetDamage(int damage)
    {
        lives -= damage;
        StartCoroutine(OnHit());

        if (lives < 1)
        {
            Die();
        }
    }

    private IEnumerator OnHit()
    {
        sprite.color = new Color(EarthLevelConstants.EnemyHitColors.SnakeSlime.firstColor,
            EarthLevelConstants.EnemyHitColors.SnakeSlime.secondColor,
            EarthLevelConstants.EnemyHitColors.SnakeSlime.thirdColor);
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }
}
