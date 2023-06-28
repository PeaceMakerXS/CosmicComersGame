using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    
    public GameObject Buton_start;
    public GameObject Buton_pouse;
    public GameObject Buton_menu;
    public GameObject Buton_restart;
    public GameObject Buton_nextLevel;
    
    public GameObject suit;
    public GameObject lose;
    public Image mask;

    private Hero player;
    public bool give;
    private int i_2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Hero>();
        Buton_start.SetActive(false);
        Buton_menu.SetActive(false);
        Buton_restart.SetActive(false);
        Buton_nextLevel.SetActive(false);
        //suit.color = new Color(0, 0, 0, 0);
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pause(int a)
    {

        if (a == 1)
        {
            //Pause
            Time.timeScale = 0;
            mask.color = new Color(0, 0, 0, 0.35f);
            //Buton_pouse.SetActive(false);
            Buton_start.SetActive(true);
            Buton_menu.SetActive(true);


        }
        if (a == 0)
        {
            //start
            mask.color = new Color(0, 0, 0, 0);
            Time.timeScale = 1;
            Buton_menu.SetActive(false);
            Buton_start.SetActive(false);
            Buton_pouse.SetActive(true);
        }
    }
    public void Lose_Win(int a)//меню проигрыша
    {
        if (a == 1)
        {
            Buton_restart.SetActive(true);
            lose.SetActive(true);
            Buton_menu.SetActive(true);
            mask.color = new Color(0, 0, 0, 0.35f);
            Time.timeScale = 0;
        }
        if (a == 2)
        {
            Time.timeScale = 0;
            Buton_nextLevel.SetActive(true) ;
            suit.SetActive(true);
            mask.color = new Color(0, 0, 0, 0.35f);
        }
    }
    public void Restart(int i)//кнопка ркстарта
    {
        if (i == 1)
        {
            SceneManager.UnloadScene("MoonLevel");
            SceneManager.LoadScene("MoonLevel");
            Time.timeScale = 1;

        }

    }
    public void scane_sw(int i)
    {
        if (i == 1)//главное меню
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1;

        }
        if (i == 2)//следующий уровень
        {
            SceneManager.LoadScene("SunLevel");
            Time.timeScale = 1;

        }

    }
}
