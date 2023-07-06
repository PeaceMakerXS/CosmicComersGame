using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthLevelUIController : LevelUIController
{
    [Header("Components")]
    [SerializeField] private Text moneyCountText;
    [SerializeField] private Text livesCountText;
    [SerializeField] private List<Transform> suitPartsPanel = new();

    private DanilHero danilHero;
    private int suitPartsCount;
    private int playerLivesCount;
    private int moneyCount;
      
    private void Start()
    {
        danilHero = player.GetComponent<DanilHero>();
        if (danilHero)
        {
            suitPartsCount = danilHero.suitPartsCollected;
            playerLivesCount = danilHero.health;
            moneyCount = danilHero.coinsCollected;
        }
    }

    private void FixedUpdate()
    {
        if (!player)
        {
            Invoke("GameOver", 0.5f);
        }

        else if (danilHero.health < 1)
        {
            Invoke("GameOver", 0.5f);
        }

        else if (!levelIsReached && danilHero.suitPartsCollected == EarthLevelConstants.Generation.suitPartsCount)
        {
            Win();
        }

        else
        {
            int currentSuitPartsCount = danilHero.suitPartsCollected;
            int currentPlayerLivesCount = danilHero.health;
            int currentMoneyCount = danilHero.coinsCollected;

            if (suitPartsCount != currentSuitPartsCount)
            {
                suitPartsPanel[suitPartsCount].GetComponent<Image>().color = Color.white;
                suitPartsCount = currentSuitPartsCount;
            }

            if (playerLivesCount != currentPlayerLivesCount)
            {
                livesCountText.text = currentPlayerLivesCount.ToString();
                playerLivesCount = currentPlayerLivesCount;
            }

            if (moneyCount != currentMoneyCount)
            {
                moneyCountText.text = currentMoneyCount.ToString();
                moneyCount = currentMoneyCount;
            }

        }
    }

    protected override void GameOver()
    {
        livesCountText.text = "0";
        base.GameOver();
    }

    protected override void Win()
    {
        suitPartsPanel[suitPartsCount].GetComponent<Image>().color = Color.white;
        base.Win();
    }
}
