using UnityEngine;
using UnityEngine.UI;

public class EarthLevelUIController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Transform player;
    [SerializeField] private Transform moneyCountText;
    [SerializeField] private Transform livesCountText;

    private DanilHero danilHero;

    private void Awake()
    {
        pausePanel.SetActive(false);
        losePanel.SetActive(false);

        if (player)
        {
            danilHero = player.GetComponent<DanilHero>();
        }
    }

    private void FixedUpdate()
    {
        if (!player)
        {
            Invoke("GameOver", 1);
        }

        else if ( danilHero.health < 1)
        {
            Invoke("GameOver", 1);
        }

        else if (danilHero.suitPartsCollected == GenerationConstants.suitPartsCount)
        {
            Invoke("GameOver", 1);
        }

        else
        {
            moneyCountText.GetComponent<Text>().text = danilHero.coinsCollected.ToString();
            livesCountText.GetComponent<Text>().text = danilHero.health.ToString();
        }
    }

    public void PauseOn()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void GameOver()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
