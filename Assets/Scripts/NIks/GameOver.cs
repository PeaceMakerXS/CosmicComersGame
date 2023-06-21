using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public void EndGame()
    {
        // Логика завершения игры
        Debug.Log("Игра завершена!");
        SceneManager.LoadScene("MainMenu"); // Загрузка сцены с экраном проигрыша
    }
}
