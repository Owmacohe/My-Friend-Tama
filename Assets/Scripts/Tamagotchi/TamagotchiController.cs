using System;
using UnityEngine;

public class TamagotchiController : MonoBehaviour
{
    [SerializeField]
    float statsSpeed = 0.005f;
    [SerializeField]
    float firstStatThreshold = 0.5f;
    [SerializeField]
    float secondStatThreshold = 0.1f;
    [SerializeField]
    MeshRenderer screen;
    [SerializeField]
    float flashingSpeed = 5;
    [SerializeField, ColorUsage(true, true)]
    Color foodColour, happinessColour, disciplineColour;
    
    Tamagotchi tama;
    
    readonly int emissionColor = Shader.PropertyToID("_EmissionColor");
    Color defaultColour, flashingColour;
    bool isFlashing;
    float flashingStartTime;
    
    bool isHungry, isStarving;
    bool isSad, isMiserable;
    bool isRude, isWicked;
    
    void Start()
    {
        tama = new Tamagotchi(null);
        defaultColour = screen.material.GetColor(emissionColor);
    }

    void FixedUpdate()
    {
        tama.UpdateStats(statsSpeed);
        
        // TODO: update evolution sprite
        
        UpdateStat(tama.Food, new []{ isHungry, isStarving }, foodColour);
        UpdateStat(tama.Happiness, new []{ isSad, isMiserable }, happinessColour);
        UpdateStat(tama.Discipline, new []{ isRude, isWicked }, disciplineColour);
        
        // TODO: update stat bars

        if (isFlashing)
        {
            float elapsedTime = Time.time - flashingStartTime;
            float maxOffset = flashingSpeed / 2f;
            float elapsedTimeOffset = elapsedTime < maxOffset ? elapsedTime : maxOffset;
            
            SetScreenColour(
                Color.Lerp(
                    defaultColour, 
                    flashingColour, 
                    Mathf.Sin(Time.time * (flashingSpeed + elapsedTimeOffset))
                )
            );
        }
    }

    void SetScreenColour(Color col)
    {
        screen.material.SetColor(emissionColor, col);
    }

    void UpdateStat(float stat, bool[] thresholds, Color flash)
    {
        if (stat <= firstStatThreshold)
        {
            if (stat > secondStatThreshold)
            {
                if (!thresholds[0])
                {
                    if (!isFlashing)
                    {
                        flashingColour = flash;
                        isFlashing = true;
                        flashingStartTime = Time.time;   
                    }

                    thresholds[0] = true;
                    
                    // TODO: call environment script (initial ping)
                }

                if (thresholds[1])
                {
                    thresholds[1] = false;
                }
            }
            else if (stat <= secondStatThreshold && !thresholds[1])
            {
                // TODO: call environment script (dire ping)

                thresholds[1] = true;
            }
        }
        else
        {
            if (thresholds[0])
            {
                thresholds[0] = false;
                
                isFlashing = false;
                SetScreenColour(defaultColour);
            }
            
            if (thresholds[1])
            {
                thresholds[1] = false;
            }
        }
    }
}