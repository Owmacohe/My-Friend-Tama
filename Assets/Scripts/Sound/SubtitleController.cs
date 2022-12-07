using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleController : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] TMP_Text text;
    [SerializeField] Color tutorialColour, streamerColour;

    [HideInInspector] public bool isPopulated;
    [HideInInspector] public bool isTutorial;

    public void Populate(string subtitle, bool tutorial)
    {
        Color temp = tutorial ? tutorialColour : streamerColour;
        
        background.enabled = true;
        background.color = new Color(temp.r, temp.g, temp.b, 0.8f);
        
        isTutorial = tutorial;
        
        text.text = subtitle;
        Color.RGBToHSV(temp, out var H, out var S, out var V);
        text.color = Color.HSVToRGB(H, S/3f, V);

        isPopulated = true;
    }

    public void Clear()
    {
        background.enabled = false;
        isPopulated = false;
        text.text = "";
    }
}