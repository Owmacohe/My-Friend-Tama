using System;
using UnityEngine;

public class TamagotchiController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float statsSpeed = 0.0005f;
    [SerializeField] float firstStatThreshold = 0.5f;
    [SerializeField] float secondStatThreshold = 0.1f;
    [SerializeField] Transform foodBar, happinessBar, disciplineBar;
    [SerializeField] GameObject foodStat, happinessStat, disciplineStat;
    
    [Header("Flashing")]
    [SerializeField] SpriteRenderer screen;
    [SerializeField] float flashingSpeed = 3;
    [SerializeField, ColorUsage(true, true)] Color foodColour, happinessColour, disciplineColour;
    [SerializeField] Vector3 awayPosition, awayRotation, frontPosition, frontRotation;
    [SerializeField] float tamaSlideSpeed = 0.01f;
    [SerializeField] Light statNeedIndicator;

    [Header("Sounds")]
    [SerializeField] SoundEffectController statNeedSound;
    [SerializeField] SoundEffectController firstThresholdSound, secondThresholdSound;

    [Header("Rounds")]
    [SerializeField] float round1Time = 60;
    [SerializeField] float round2Time = 120;
    [SerializeField] float round3Time = 180;
    public AudioSource roundMusic;
    
    PlayerController pc;
    CheckpointScript cps;
    ChatController cc;
    public Tamagotchi tama;
    Animator anim;
    int timesEvolved;

    readonly int emissionColor = Shader.PropertyToID("_EmissionColor");
    Color defaultColour, flashingColour;
    bool isFlashing;
    float currentFlashingSpeed;

    [HideInInspector] public bool slideUp;
    bool isSliding, isLookingAtTama;
    float slideCurrent;

    bool isUpdatingStats;

    [HideInInspector] public float round1StartTime, round2StartTime, round3StartTime;
    [HideInInspector] public bool hasRound1Started, hasRound2Started, hasRound3Started;
    [HideInInspector] public float pauseStartTime, pauseTime;

    public void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        cps = FindObjectOfType<CheckpointScript>();
        cc = FindObjectOfType<ChatController>();
        
        tama = new Tamagotchi(0.6f);
        
        anim = GetComponentInChildren<Animator>();
        anim.keepAnimatorControllerStateOnDisable = true;
        
        defaultColour = screen.material.GetColor(emissionColor);
        
        foodStat.SetActive(false);
        happinessStat.SetActive(false);
        disciplineStat.SetActive(false);
    }

    void FixedUpdate()
    {
        if (isUpdatingStats && !pc.isDead && !pc.isPaused)
        {
            tama.UpdateStats(statsSpeed);
            
            bool[] foodStats = CheckStat(tama.Food, foodColour);
            bool[] happinessStats = CheckStat(tama.Happiness, happinessColour);
            bool[] disciplineStats = CheckStat(tama.Discipline, disciplineColour);

            if (tama.Food <= 0 || tama.Happiness <= 0 || tama.Discipline <= 0)
            {
                pc.KillPlayer();
                cps.CheckPoint();
                tama.ResetStats();
            }

            if ((int)tama.Age == 1)
            {
                if (foodStats[0])
                {
                    anim.SetBool("IsBabyHungry", true);
                }
                else
                {
                    anim.SetBool("IsBabyHungry", false);
                }
            
                if (happinessStats[0])
                {
                    anim.SetBool("IsBabyMad", true);
                }
                else
                {
                    anim.SetBool("IsBabyMad", false);
                }
                
                if (disciplineStats[0])
                {
                    anim.SetBool("IsBabyDiscipline", true);
                }
                else
                {
                    anim.SetBool("IsBabyDiscipline", false);
                }
            }

            if ((int)tama.Age == 2)
            {
                if (foodStats[0])
                {
                    anim.SetBool("IsKidHungry", true);
                }
                else
                {
                    anim.SetBool("IsKidHungry", false);
                }
            
                if (happinessStats[0])
                {
                    anim.SetBool("IsKidMad", true);
                }
                else
                {
                    anim.SetBool("IsKidMad", false);
                }
                
                if (disciplineStats[0])
                {
                    anim.SetBool("IsKidDiscipline", true);
                }
                else
                {
                    anim.SetBool("IsKidDiscipline", false);
                }
            }

            if ((int)tama.Age == 3)
            {
                if (foodStats[0])
                {
                    anim.SetBool("IsAdultHungry", true);
                }
                else
                {
                    anim.SetBool("IsAdultHungry", false);
                }
            
                if (happinessStats[0])
                {
                    anim.SetBool("IsAdultMad", true);
                }
                else
                {
                    anim.SetBool("IsAdultMad", false);
                }
                
                if (disciplineStats[0])
                {
                    anim.SetBool("IsAdultDiscipline", true);
                }
                else
                {
                    anim.SetBool("IsAdultDiscipline", false);
                }
            }

            if (!foodStats[0] && !happinessStats[0] && !disciplineStats[0])
            {
                firstThresholdSound.Stop();
                    
                isFlashing = false;
                SetScreenColour(defaultColour);
                statNeedIndicator.enabled = false;
            }

            if (!foodStats[1] && !happinessStats[1] && !disciplineStats[1])
            {
                secondThresholdSound.Stop();
            }

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

    public void SlideTama(bool up, bool changeFlashlight = true)
    {
        isSliding = true;
        slideUp = up;
        slideCurrent = 0;

        transform.localEulerAngles = slideUp ? frontRotation : awayRotation;

        if (changeFlashlight)
        {
            pc.SetFlashlight(!slideUp, true);
        }

        if (slideUp)
        {
            isLookingAtTama = true;
        }
        else
        {
            isLookingAtTama = false;
        }
    }

    void PlayStatNeedSound()
    {
        if (isFlashing)
        {
            if (!isLookingAtTama && !statNeedSound.source.isPlaying && !pc.isPaused)
            {
                statNeedSound.Play();   
            }

            Invoke(nameof(PlayStatNeedSound), 1.5f);
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
    /// <param name="flash">The colour to flash if the stat is too low</param>
    bool[] CheckStat(float stat, Color flash)
    {
        bool[] conditions = new bool[2];
        
        if (stat <= firstStatThreshold)
        {
            conditions[0] = true;

            if (!isFlashing)
            {
                isFlashing = true;
                flashingColour = flash;
                currentFlashingSpeed = flashingSpeed;
                firstThresholdSound.Play();
                PlayStatNeedSound();   
            }

            if (stat <= secondStatThreshold)
            {
                conditions[1] = true;

                if (!secondThresholdSound.source.isPlaying)
                {
                    currentFlashingSpeed = flashingSpeed * 2;
                    secondThresholdSound.Play();   
                }
            }
        }

        return conditions;
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

    public void WaitEvolve(float seconds)
    {
        Invoke(nameof(Evolve), seconds);
    }

    void Evolve()
    {
        tama.Evolve();
        anim.SetTrigger("Evolve");
    }

    void SetUpdatingStats(bool isUpdating)
    {
        isUpdatingStats = isUpdating;

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

    public void StartRound(int round)
    {
        switch (round)
        {
            case 1:
                if ((int)tama.Age == 1)
                {
                    round1StartTime = Time.time;
                    pauseTime = 0;
                    
                    SetUpdatingStats(true);
                    hasRound1Started = true;
                    roundMusic.Play();
                }
                break;
            case 2:
                if ((int)tama.Age == 2)
                {
                    round2StartTime = Time.time;
                    pauseTime = 0;
                    
                    SetUpdatingStats(true);
                    hasRound2Started = true;
                    roundMusic.Play();
                }
                break;
            case 3:
                if ((int)tama.Age == 3)
                {
                    round3StartTime = Time.time;
                    pauseTime = 0;
                    
                    SetUpdatingStats(true);
                    hasRound3Started = true;
                    roundMusic.Play();
                }
                break;
        }
    }

    public bool IsRoundDone(int round)
    {
        bool temp =
            (int)tama.Age == round && (
                (round == 1 && hasRound1Started && (Time.time - round1StartTime - pauseTime >= round1Time)) ||
                (round == 2 && hasRound2Started && (Time.time - round2StartTime - pauseTime >= round2Time)) ||
                (round == 3 && hasRound3Started && (Time.time - round3StartTime - pauseTime >= round3Time))
            );

        if (temp)
        {
            SetUpdatingStats(false);

            pauseTime = 0;

            switch (round)
            {
                case 1:
                    hasRound1Started = false;
                    break;
                case 2:
                    hasRound2Started = false;
                    break;
                case 3:
                    hasRound3Started = false;
                    break;
            }

            if (cc != null)
                cc.roundDoneMessages = true;
            
            roundMusic.Stop();
        }
        else
        {
            if (cc != null)
                cc.roundDoneMessages = false;
        }

        return temp;
    }

    public bool IsGateTimeDone(int round)
    {
        return
            (int)tama.Age == round && (
                (round == 1 && hasRound1Started && (Time.time - round1StartTime - pauseTime >= 30)) ||
                (round == 2 && hasRound2Started && (Time.time - round2StartTime - pauseTime >= 30)) ||
                (round == 3 && hasRound3Started && (Time.time - round3StartTime - pauseTime >= 30))
            );
    }
}