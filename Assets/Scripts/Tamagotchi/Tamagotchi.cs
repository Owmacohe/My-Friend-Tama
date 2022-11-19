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

    public Tamagotchi()
    {
        Food = 1;
        Happiness = 1;
        Discipline = 1;
        
        Age = TamagotchiAge.Egg;
        spawnTime = Time.time;
    }

    public void Feed()
    {
        Food = Food < 1 ? Food + 0.1f : 1;
    }
    
    public void Play()
    {
        Happiness = Happiness < 1 ? Happiness + 0.1f : 1;
    }
    
    public void Scold()
    {
        Discipline = Discipline < 1 ? Discipline + 0.1f : 1;
    }

    float UpdateStat(float stat, float amount)
    {
        return stat > 0 ? stat - (Random.Range(0, 4) == 0 ? amount : 0) : 0;
    }

    public void UpdateStats(float amount)
    {
        Food = UpdateStat(Food, amount);
        Happiness = UpdateStat(Happiness, amount);
        Discipline = UpdateStat(Discipline, amount);

        if (Round(Time.time - spawnTime, 0) == 5 ||
            Round(Time.time - spawnTime, 0) == 10)
        {
            Evolve();
        }
        else
        {
            hasJustEvolved = false;
        }
    }

    public void Evolve()
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

    float Round(float amount, int decimalPlaces = 2)
    {
        float temp = Mathf.Pow(10, decimalPlaces);
        
        return Mathf.Round(amount * temp) / temp;
    }

    public string ToString()
    {
        return "<b>TAMAGODCHI</b>" +
           "\nAge: " + Age + " (" + Round(Time.time - spawnTime, 0) + "s)" +
           "\nFood: " + Round(Food) +
           "\nHappiness: " + Round(Happiness) +
           "\nDiscipline: " + Round(Discipline);
    }
}