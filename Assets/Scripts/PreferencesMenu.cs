using UnityEngine;
using UnityEngine.UI;

public class PreferencesMenu : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Text musicPercentField;
    [SerializeField] private string musicVolumeKey;

    private float volume;

    public void Awake()
    {
        volume = PlayerPrefs.GetFloat(musicVolumeKey, 0.5f);
        musicSlider.value = volume;
        music.volume = volume;
        musicPercentField.text = Mathf.Round(volume * 100) + "%";
    }

    public void SoundVolumeChange() {
        volume = musicSlider.value;
        PlayerPrefs.SetFloat(musicVolumeKey, volume);
        music.volume = volume;
        musicPercentField.text = Mathf.Round(volume * 100) + "%";
        PlayerPrefs.Save();
    }
}
