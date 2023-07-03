using System.Collections;
using UnityEngine;

public class SnakeLavaEnemy : SnakeEnemy
{
    protected override void Start()
    {
        base.Start();
        lives = 4;
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
        sprite.color = new Color(EarthLevelConstants.EnemyHitColors.SnakeLava.firstColor,
            EarthLevelConstants.EnemyHitColors.SnakeLava.secondColor,
            EarthLevelConstants.EnemyHitColors.SnakeLava.thirdColor);
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }
}
