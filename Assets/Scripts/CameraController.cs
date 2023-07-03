using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private Vector3 pos;
    private string currentSceneName;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player)
        {
            pos = player.position;

            if (currentSceneName == "MoonLevel")
            {
                pos.x = player.position.x + 9f;
            }

            else if (currentSceneName == "SunLevel")
            {
                pos.y = 0;
                pos.x = player.position.x+9f;
            }

            pos.z = -10f;

            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
        }
    }
}