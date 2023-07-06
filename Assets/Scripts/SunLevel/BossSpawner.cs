using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public float bossSpawnDelay = 15f;
    private NIksHero player;
    private Camera mainCamera;
    private float cameraWidth;
    public float offset;
    

    private void Start()
    {
        Invoke("SpawnBoss", bossSpawnDelay);
        mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
        offset = 6f;
        
        player = FindObjectOfType<NIksHero>();
    }

    private void SpawnBoss()
    {
        float spawnX = player.transform.position.x + cameraWidth;
        //Instantiate(bossPrefab, transform.position, Quaternion.identity);
        Vector2 spawnPosition = new Vector2(spawnX, 0f);
        GameObject obstacle = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
}
