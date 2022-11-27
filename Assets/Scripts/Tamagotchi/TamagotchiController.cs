using System;
using UnityEngine;

public class TamagotchiController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float statsSpeed = 0.002f;
    [SerializeField] float firstStatThreshold = 0.5f;
    [SerializeField] float secondStatThreshold = 0.1f;
    [SerializeField] Transform foodBar, happinessBar, disciplineBar;
    [SerializeField] GameObject foodStat, happinessStat, disciplineStat;
    
    [Header("Flashing")]
    [SerializeField] SpriteRenderer screen;
    [SerializeField] float flashingSpeed = 10;
    [SerializeField, ColorUsage(true, true)] Color foodColour, happinessColour, disciplineColour;
    [SerializeField] Vector3 awayPosition, awayRotation, frontPosition, frontRotation;
    [SerializeField] float tamaSlideSpeed = 0.01f;
    [SerializeField] Light statNeedIndicator;

    PlayerController pc;
    public Tamagotchi tama;
    Animator anim;
    int timesEvolved;

    readonly int emissionColor = Shader.PropertyToID("_EmissionColor");
    Color defaultColour, flashingColour;
    bool isFlashing;
    float currentFlashingSpeed;
    bool isSliding, slideUp, isLookingAtTama;
    float slideCurrent;
    
    bool isHungry, isStarving;
    bool isSad, isMiserable;
    bool isRude, isWicked;
    
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        
        tama = new Tamagotchi();
        anim = GetComponentInChildren<Animator>();
        
        defaultColour = screen.material.GetColor(emissionColor);
        
        foodStat.SetActive(false);
        happinessStat.SetActive(false);
        disciplineStat.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isSliding = true;
            slideUp = !slideUp;
            slideCurrent = 0;

            transform.localEulerAngles = slideUp ? frontRotation : awayRotation;
            
            pc.SetFlashlight(!slideUp, true);

            if (slideUp)
            {
                isLookingAtTama = true;
            }
            else
            {
                isLookingAtTama = false;
            }
        }
    }

    void FixedUpdate()
    {
        tama.UpdateStats(statsSpeed);

        CheckStat(tama.Food, new []{ isHungry, isStarving }, foodColour);
        CheckStat(tama.Happiness, new []{ isSad, isMiserable }, happinessColour);
        CheckStat(tama.Discipline, new []{ isRude, isWicked }, disciplineColour);

        SetStatBar(foodBar, tama.Food);
        SetStatBar(happinessBar, tama.Happiness);
        SetStatBar(disciplineBar, tama.Discipline);

        if (isFlashing)
        {
            float temp = Mathf.Sin(Time.time * currentFlashingSpeed);
            
            SetScreenColour(
                Color.Lerp(
                    defaultColour,
                    flashingColour,
                    temp
                )
            );

            if (!isLookingAtTama)
            {
                if (temp > 0)
                {
                    statNeedIndicator.enabled = true;
                }
                else
                {
                    statNeedIndicator.enabled = false;
                }   
            }
        }

        if (isSliding)
        {
            float lerpAmount = slideCurrent + tamaSlideSpeed;
            
            if (slideUp && !transform.localPosition.Equals(frontPosition))
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, frontPosition, lerpAmount);
            }
            else if (!slideUp && !transform.localPosition.Equals(awayPosition))
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, awayPosition, lerpAmount);
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
                        currentFlashingSpeed = flashingSpeed;
                    }

                    conditions[0] = true;
                }

                if (conditions[1])
                {
                    conditions[1] = false;
                }
            }
            else if (stat <= secondStatThreshold && !conditions[1])
            {
                conditions[1] = true;
                currentFlashingSpeed = flashingSpeed * 2;
            }
        }
        else
        {
            if (conditions[0])
            {
                conditions[0] = false;
                
                isFlashing = false;
                SetScreenColour(defaultColour);
                statNeedIndicator.enabled = false;
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

    public void Evolve()
    {
        tama.Evolve();
        anim.SetTrigger("Evolve");

        int currentAge = (int)tama.Age;
        
        if (currentAge > 0)
        {
            foodStat.SetActive(true);

            if (currentAge > 1)
            {
                happinessStat.SetActive(true);

                if (currentAge > 2)
                {
                    disciplineStat.SetActive(true); 
                }
            }
        }
    }
}