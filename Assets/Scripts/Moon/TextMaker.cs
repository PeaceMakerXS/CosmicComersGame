using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextMaker : MonoBehaviour
{
    public Text textComponent;
    public Text textComponent2;

    private DynamicGeneration obj;
    void Start()
    {
        obj = FindObjectOfType<DynamicGeneration>();
    }

    void Update()
    {
        textComponent.text = ":"+Convert.ToString(obj.stars_amount);
        textComponent2.text = Convert.ToString(obj.details_amount)+"/6";
    }
}
