using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button[] levels;

    private void Start()
    {
        /*
        int maxReachedLevel = PlayerPrefs.GetInt("levelsReached", 0);

        for (int i = 0; i < levels.Length; i++)
        {
            if (i > maxReachedLevel)
            {
                levels[i].interactable = false;
            }
        }
        */
    }

    public void SelectLevel(int numberInBuild)
    {
        SceneManager.LoadScene(numberInBuild);
    }

}
