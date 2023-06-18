using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    private Scene currentScene;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        if (!player)
        {
            switch (currentScene.name)
            {
                case "EarthLevel":
                    player = FindObjectOfType<DanilHero>().transform;
                    break;

                case "MoonLevel":
                    player = FindObjectOfType<Hero>().transform;
                    break;
                case "SunLevel":
                    player = FindObjectOfType<NIksHero>().transform;
                    break;
            }

        }
    }

    private void Update()
    {
        pos = player.position;
        if (currentScene.name == "MoonLevel")
        {
            pos.x = player.position.x + 9f;
        }
        pos.z = -10f;

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }
}