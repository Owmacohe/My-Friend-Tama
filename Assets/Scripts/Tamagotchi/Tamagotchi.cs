using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tamagotchi
{
    public float Food { get; private set; } // 0..1
    public float Happiness { get; private set; } // 0..1
    public float Discipline { get; private set; } // 0..1

    public enum TamagotchiAge { Egg, Baby, Kid, Adult }
    public TamagotchiAge Age { get; private set; }

    public Tamagotchi()
    {
        Food = 1;
        Happiness = 1;
        Discipline = 1;
        
        Age = TamagotchiAge.Egg;
    }

    /// <summary>
    /// Increases food level
    /// </summary>
    public void Feed(float amount)
    {
        if ((int)Age > 0)
        {
            Food = Food < 1 ? Food + amount : 1;
        }
    }
    
    // Increases happiness level
    public void Play(float amount)
    {
        if ((int)Age > 1)
        {
            Happiness = Happiness < 1 ? Happiness + amount : 1;
        }
    }
    
    /// <summary>
    /// Increases discipline level
    /// </summary>
    public void Scold(float amount)
    {
        if ((int)Age > 2)
        {
            Discipline = Discipline < 1 ? Discipline + amount : 1;
        }
    }

    /// <summary>
    /// Decreases all stats and evolves, if necessary
    /// </summary>
    /// <param name="amount">Amount that each stat has a chance to be decreased by</param>
    public void UpdateStats(float amount)
    {
        int currentAge = (int)Age;

        if (currentAge > 0)
        {
            Food = UpdateStat(Food, amount);

            if (currentAge > 1)
            {
                Happiness = UpdateStat(Happiness, amount * (2f/3f));

                if (currentAge > 2)
                {
                    Discipline = UpdateStat(Discipline, amount * (1f/3f));   
                }
            }
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
        else if (stat > 1)
        {
            return 1;
        }
        else
        {
            return 0;   
        }
    }

    /// <summary>
    /// Increases the age
    /// </summary>
    public void Evolve()
    {
        int enumSize = Enum.GetNames(typeof(TamagotchiAge)).Length;
        int currentAge = (int)Age;
        
        Age = currentAge < enumSize
            ? (TamagotchiAge)(currentAge + 1)
            : (TamagotchiAge)(enumSize - 1);
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
           "\nAge: " + Age +
           "\nFood: " + Round(Food) +
           "\nHappiness: " + Round(Happiness) +
           "\nDiscipline: " + Round(Discipline);
    }
}