using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleController : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] TMP_Text text;

    [HideInInspector] public bool isPopulated;
    [HideInInspector] public bool isTutorial;

    public void Populate(string subtitle, bool tutorial)
    {
        background.enabled = true;
        
        isTutorial = tutorial;
        
        text.text = subtitle;
        isPopulated = true;
    }

    public void Clear()
    {
        background.enabled = false;
        isPopulated = false;
        text.text = "";
    }
}