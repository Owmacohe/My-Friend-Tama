using System;
using UnityEngine;

public class TamagotchiController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float statsSpeed = 0.005f;
    [SerializeField] float firstStatThreshold = 0.5f;
    [SerializeField] float secondStatThreshold = 0.1f;
    [SerializeField] Transform foodBar, happinessBar, disciplineBar;

    [Header("Sprite")]
    [SerializeField] SpriteRenderer tamaSprite;
    [SerializeField] Sprite[] tamaEvolutions;
    
    [Header("Flashing")]
    [SerializeField] SpriteRenderer screen;
    [SerializeField] float flashingSpeed = 5;
    [SerializeField, ColorUsage(true, true)] Color foodColour, happinessColour, disciplineColour;
    [SerializeField] Vector3 awayPosition, awayRotation, frontPosition, frontRotation;
    [SerializeField] float tamaSlideSpeed = 0.01f;
    
    public Tamagotchi tama;
    int timesEvolved;
    
    readonly int emissionColor = Shader.PropertyToID("_EmissionColor");
    Color defaultColour, flashingColour;
    bool isFlashing;
    float flashingStartTime;
    bool isSliding, slideUp;
    float slideCurrent;
    
    bool isHungry, isStarving;
    bool isSad, isMiserable;
    bool isRude, isWicked;
    
    void Start()
    {
        tama = new Tamagotchi(null);
        tamaSprite.sprite = tamaEvolutions[timesEvolved];
        
        defaultColour = screen.material.GetColor(emissionColor);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isSliding = true;
            slideUp = !slideUp;
            slideCurrent = 0;
        }
    }

    void FixedUpdate()
    {
        if (tama.UpdateStats(statsSpeed))
        {
            tamaSprite.sprite = tamaEvolutions[++timesEvolved];
        }

        CheckStat(tama.Food, new []{ isHungry, isStarving }, foodColour);
        CheckStat(tama.Happiness, new []{ isSad, isMiserable }, happinessColour);
        CheckStat(tama.Discipline, new []{ isRude, isWicked }, disciplineColour);

        SetStatBar(foodBar, tama.Food);
        SetStatBar(happinessBar, tama.Happiness);
        SetStatBar(disciplineBar, tama.Discipline);

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

        if (isSliding)
        {
            float lerpAmount = slideCurrent + tamaSlideSpeed;
            
            if (slideUp && !transform.localPosition.Equals(frontPosition))
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, frontPosition, lerpAmount);
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, frontRotation, lerpAmount);
            }
            else if (!slideUp && !transform.localPosition.Equals(awayPosition))
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, awayPosition, lerpAmount);
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, awayRotation, lerpAmount);
            }
            else
            {
                isSliding = false;
            }

            slideCurrent += tamaSlideSpeed;
        }
    }

    /// <summary>
    /// Sets the tamagotchi's current screen colour
    /// </summary>
    /// <param name="col">The colour to be set (must be an HDR colour)</param>
    void SetScreenColour(Color col)
    {
        screen.material.SetColor(emissionColor, col);
    }

    /// <summary>
    /// Compares a given stat's against thresholds to determine tamagotchi conditions and trigger external scripts
    /// </summary>
    /// <param name="stat">The value to be checked</param>
    /// <param name="conditions">The reference-passed booleans to be possibly turned on/off</param>
    /// <param name="flash">The colour to flash if the stat is too low</param>
    void CheckStat(float stat, bool[] conditions, Color flash)
    {
        if (stat <= firstStatThreshold)
        {
            if (stat > secondStatThreshold)
            {
                if (!conditions[0])
                {
                    if (!isFlashing)
                    {
                        flashingColour = flash;
                        isFlashing = true;
                        flashingStartTime = Time.time;   
                    }

                    conditions[0] = true;
                    
                    // TODO: call environment script (initial ping)
                }

                if (conditions[1])
                {
                    conditions[1] = false;
                }
            }
            else if (stat <= secondStatThreshold && !conditions[1])
            {
                // TODO: call environment script (dire ping)

                conditions[1] = true;
            }
        }
        else
        {
            if (conditions[0])
            {
                conditions[0] = false;
                
                isFlashing = false;
                SetScreenColour(defaultColour);
            }
            
            if (conditions[1])
            {
                conditions[1] = false;
            }
        }
    }

    /// <summary>
    /// Sets the scale and position of stat bars in the tamagotchi UI
    /// </summary>
    /// <param name="bar">The bar Transform to set</param>
    /// <param name="stat">The stat value to base it on</param>
    void SetStatBar(Transform bar, float stat)
    {
        bar.localScale = new Vector3(stat, 1, 1);
        bar.localPosition = new Vector3((0.4f * stat) - 0.4f, -0.35f, 0);
    }
}