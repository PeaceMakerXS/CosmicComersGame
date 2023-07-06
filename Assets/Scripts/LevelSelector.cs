using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button[] levels;

    private void Start()
    {
        if (PlayerPrefs.GetInt("AllLevelsEnabled", 0) == 0)
        {
            int maxReachedLevel = PlayerPrefs.GetInt("levelsReached", 0);

            for (int i = 0; i < levels.Length; i++)
            {
                if (i > maxReachedLevel)
                {
                    levels[i].interactable = false;
                }
            }
        }
    }

    public void SelectLevel(int numberInBuild)
    {
        SceneManager.LoadScene(numberInBuild);
    }

}
