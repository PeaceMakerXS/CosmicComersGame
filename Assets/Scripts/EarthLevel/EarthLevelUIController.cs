using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EarthLevelUIController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    [SerializeField] private Transform player;
    [SerializeField] private Transform moneyCountText;
    [SerializeField] private Transform livesCountText;
    [SerializeField] private List<Transform> suitPartsPanel = new();

    private DanilHero danilHero;
    private int suitPartsCount;
    private int playerLivesCount;
    private int moneyCount;
    private bool levelIsReached;

    private void Awake()
    {
        pausePanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);

        if (player)
        {
            danilHero = player.GetComponent<DanilHero>();
        }
    }

    private void Start()
    {
        if (danilHero) {
            suitPartsCount = danilHero.suitPartsCollected;
            playerLivesCount = danilHero.health;
            moneyCount = danilHero.coinsCollected;
            levelIsReached = false;
        }
    }


    private void FixedUpdate()
    {
        if (!player)
        {
            Invoke("GameOver", 0.5f);
        }

        else if ( danilHero.health < 1)
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

            if (playerLivesCount != danilHero.health)
            {
                livesCountText.GetComponent<Text>().text = currentPlayerLivesCount.ToString();
                playerLivesCount = currentPlayerLivesCount;
            }

            if (moneyCount != danilHero.coinsCollected)
            {
                moneyCountText.GetComponent<Text>().text = currentMoneyCount.ToString();
                moneyCount = currentMoneyCount;
            }
           
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
        winPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void NextLevelLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    private void GameOver()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void Win()
    {
        levelIsReached = true;
        suitPartsPanel[suitPartsCount].GetComponent<Image>().color = Color.white;

        if (PlayerPrefs.GetInt("levelsReached", 0) < 1)
        {
            PlayerPrefs.SetInt("levelsReached", 1);
            PlayerPrefs.Save();
        }

        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
