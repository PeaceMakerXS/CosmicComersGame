using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassEnemy : JumpingEnemy
{
    private void Start()
    {
        jumpforce = 5f;
        lives = 3;
    }
}
