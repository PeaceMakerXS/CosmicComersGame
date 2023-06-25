using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareEnemy : JumpingEnemy
{
    int k = 0;
    bool littlejump = false;
    protected override void Update()
    {
        base.Update();

        if (IsGrounded && !littlejump)
        {
            k++;
            if (k == 3)
            {
                jumpforce = 3;
                littlejump = true;
                k = 0;
            }
        }
        if (IsGrounded && littlejump)
        {
            k++;
            if (k == 2)
            {
                jumpforce = 10;
                littlejump = false;
                k = 0;
            }
        }
        Debug.Log(k);
    }
}
