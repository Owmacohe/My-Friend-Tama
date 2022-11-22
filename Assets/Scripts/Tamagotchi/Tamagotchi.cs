using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tamagotchi
{
    public float Food { get; private set; } // 0..1
    public float Happiness { get; private set; } // 0..1
    public float Discipline { get; private set; } // 0..1

    public enum TamagotchiAge { Egg = 0, Child = 1, Adult = 2 }
    public TamagotchiAge Age { get; private set; }

    float spawnTime;
    bool hasJustEvolved;
    int[] evolutionThresholds;

    public Tamagotchi(int[] thresholds)
    {
        Food = 1;
        Happiness = 1;
        Discipline = 1;
        
        Age = TamagotchiAge.Egg;
        spawnTime = Time.time;

        evolutionThresholds = thresholds;
    }

    /// <summary>
    /// Increases food level
    /// </summary>
    public void Feed()
    {
        Food = Food < 1 ? Food + 0.1f : 1;
    }
    
    // Increases happiness level
    public void Play()
    {
        Happiness = Happiness < 1 ? Happiness + 0.1f : 1;
    }
    
    /// <summary>
    /// Increases discipline level
    /// </summary>
    public void Scold()
    {
        Discipline = Discipline < 1 ? Discipline + 0.1f : 1;
    }

    /// <summary>
    /// Whether or not the tamagotchi is ready to evolve
    /// </summary>
    bool IsEvolutionTime()
    {
        if (evolutionThresholds != null)
            foreach (int i in evolutionThresholds)
                if (Round(Time.time - spawnTime, 0) == i)
                    return true;

        return false;
    }

    /// <summary>
    /// Decreases all stats and evolves, if necessary
    /// </summary>
    /// <param name="amount">Amount that each stat has a chance to be decreased by</param>
    public void UpdateStats(float amount)
    {
        Food = UpdateStat(Food, amount);
        Happiness = UpdateStat(Happiness, amount);
        Discipline = UpdateStat(Discipline, amount);

        if (IsEvolutionTime())
        {
            Evolve();
        }
        else
        {
            hasJustEvolved = false;
        }
    }
    
    /// <summary>
    /// Returns a decreased stat
    /// </summary>
    /// <param name="stat">The current value of the stat</param>
    /// <param name="amount">Amount that the stat has a chance to be decreased by</param>
    float UpdateStat(float stat, float amount)
    {
        if (stat is > 0 and <= 1)
        {
            return stat - (Random.Range(0, 4) == 0 ? amount : 0);
        }
        
        if (stat < 0)
        {
            return 0;
        }
            
        return 1;
    }

    /// <summary>
    /// Increases the age
    /// </summary>
    void Evolve()
    {
        if (!hasJustEvolved)
        {
            int enumSize = Enum.GetNames(typeof(TamagotchiAge)).Length;
            int currentAgeNum = (int)Age;
        
            Age = currentAgeNum < enumSize
                ? (TamagotchiAge)(currentAgeNum + 1)
                : (TamagotchiAge)(enumSize - 1);

            hasJustEvolved = true;   
        }
    }

    /// <summary>
    /// Rounds a float to a set number of decimal places
    /// </summary>
    /// <param name="amount">Float to be rounded</param>
    /// <param name="decimalPlaces">Number of decimal places to round to</param>
    float Round(float amount, int decimalPlaces = 2)
    {
        float temp = Mathf.Pow(10, decimalPlaces);
        
        return Mathf.Round(amount * temp) / temp;
    }

    /// <summary>
    /// Overridden ToString method
    /// </summary>
    public override string ToString()
    {
        return "<b>TAMAGODCHI</b>" +
           "\nAge: " + Age + " (" + Round(Time.time - spawnTime, 0) + "s)" +
           "\nFood: " + Round(Food) +
           "\nHappiness: " + Round(Happiness) +
           "\nDiscipline: " + Round(Discipline);
    }
}