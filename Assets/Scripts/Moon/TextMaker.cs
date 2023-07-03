using System;
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

    void FixedUpdate()
    {
        textComponent.text = ":"+Convert.ToString(obj.stars_amount);
        textComponent2.text = Convert.ToString(obj.details_amount)+"/6";
    }
}
