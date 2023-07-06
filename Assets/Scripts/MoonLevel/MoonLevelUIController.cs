using System;
using UnityEngine;
using UnityEngine.UI;

public class MoonLevelUIController : LevelUIController
{
    [Header("Components")]
    [SerializeField] private Text starsAmountText;
    [SerializeField] private Text suitPartsAmountText;

    private Hero hero;
    private DynamicGeneration obj;
    

    private void Start()
    {
        hero = player.GetComponent<Hero>();
        obj = FindObjectOfType<DynamicGeneration>();
    }

    private void FixedUpdate()
    {
        starsAmountText.text = Convert.ToString(obj.stars_amount);
        suitPartsAmountText.text = Convert.ToString(obj.details_amount) + "/6";

        if (!player)
        {
            Invoke("GameOver", 0.5f);
        }

        else if (hero.health < 1)
        {
            Invoke("GameOver", 0.5f);
        }

        else if (!levelIsReached && obj.details_amount == 6)
        {
            Win();
        }
    }
}
