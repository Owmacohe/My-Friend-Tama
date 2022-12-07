using System;
using TMPro;
using UnityEngine;

public class SubtitleController : MonoBehaviour
{
    TMP_Text text;

    [HideInInspector] public bool isPopulated;
    [HideInInspector] public bool isTutorial;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void Populate(string subtitle, bool tutorial)
    {
        isTutorial = tutorial;
        
        text.text = subtitle;
        isPopulated = true;
    }

    public void Clear()
    {
        isPopulated = false;
        text.text = "";
    }
}