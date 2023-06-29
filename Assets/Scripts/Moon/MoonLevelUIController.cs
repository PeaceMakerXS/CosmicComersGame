using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoonLevelUIController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject touchButton;

    private Hero hero;
    private bool levelIsReached;

    private DynamicGeneration obj;

    private void Awake()
    {
        pausePanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);

        hero = FindObjectOfType<Hero>();

        obj = FindObjectOfType<DynamicGeneration>();
    }

    private void FixedUpdate()
    {
        if (!hero)
        {
            Invoke("GameOver", 0.5f);
        }

        else if (hero.health < 1)
        {
            Invoke("GameOver", 0.5f);
        }

        else if (obj.details_amount == 6)
        {
            Win();
        }
    }

    public void PauseOn()
    {
        touchButton.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        Time.timeScale = 1;
        touchButton.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("MoonLevel");
        Time.timeScale = 1;
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void NextLevelLoad()
    {
        SceneManager.LoadScene("SunLevel");
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

        if (PlayerPrefs.GetInt("levelsReached", 0) < 1)
        {
            PlayerPrefs.SetInt("levelsReached", 1);
            PlayerPrefs.Save();
        }

        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
