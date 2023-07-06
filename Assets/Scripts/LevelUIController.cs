using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] protected GameObject pausePanel;
    [SerializeField] protected GameObject losePanel;
    [SerializeField] protected GameObject winPanel;
    [SerializeField] protected GameObject prefsPanel;

    [Header("Buttons")]
    [SerializeField] protected GameObject pauseButton;

    [Header("Components")]
    [SerializeField] protected AudioSource music;
    [SerializeField] protected Transform player;

    [Header("Values")]
    [SerializeField] protected int sceneNumber;
    [SerializeField] protected string musicVolumeKey;

    protected bool levelIsReached;

    private void Awake()
    {
        pausePanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pauseButton.SetActive(true);
        prefsPanel.SetActive(false);

        levelIsReached = false;
        music.volume = PlayerPrefs.GetFloat(musicVolumeKey, 0.5f);
    }

    public void PauseOn()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void PreferencesPanelOn()
    {
        prefsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void PreferencesPanelOff()
    {
        prefsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneNumber);
        Time.timeScale = 1;
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void NextLevelLoad()
    {
        SceneManager.LoadScene(sceneNumber + 1);
        Time.timeScale = 1;
    }

    protected virtual void GameOver()
    {
        pauseButton.SetActive(false);
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    protected virtual void Win()
    {
        levelIsReached = true;

        if (PlayerPrefs.GetInt("levelsReached", 0) < sceneNumber)
        {
            PlayerPrefs.SetInt("levelsReached", sceneNumber);
            PlayerPrefs.Save();
        }

        pauseButton.SetActive(false);
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
