using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLevelUIController : LevelUIController
{
    private NIksHero niksHero;
    
    private void Start()
    {
        niksHero = player.GetComponent<NIksHero>();
    }
    private void FixedUpdate()
    {
        if (!player)
        {
            Invoke("GameOver", 0.5f);
        }

        else if (niksHero.lives < 1)
        {
            Invoke("GameOver", 0.5f);
        }
    }
}
