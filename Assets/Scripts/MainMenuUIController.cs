using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject prefsPanel;
    [SerializeField] private AudioSource music;
    [SerializeField] private Toggle unlockingToggle;
    [SerializeField] private string musicVolumeKey;

    private void Awake()
    {
        music.volume = PlayerPrefs.GetFloat(musicVolumeKey, 0.5f);
        prefsPanel.SetActive(false);
    }

    public void PreferencesPanelOn()
    {
        prefsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PrefencesPanelOff()
    {
        prefsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void UnlockinToggleController()
    {
        PlayerPrefs.SetInt("AllLevelsEnabled", Convert.ToInt32(unlockingToggle.isOn));
        PlayerPrefs.Save();
    }
}
