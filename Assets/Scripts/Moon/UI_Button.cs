using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button : MonoBehaviour
{
    
    public GameObject Buton_start;
    public GameObject Buton_pouse;
    public GameObject mask;
    private Hero player;
    public bool give;
    private int i_2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Hero>();
        Buton_start.SetActive(false);

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
            //Buton_pouse.SetActive(false);
            Buton_start.SetActive(true);


        }
        if (a == 0)
        {
            //start
            Time.timeScale = 1;
            Buton_start.SetActive(false);
            Buton_pouse.SetActive(true);
        }
    }

}
