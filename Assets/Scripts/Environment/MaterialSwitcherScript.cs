using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MaterialSwitcherScript : MonoBehaviour
{
    [SerializeField] GameObject[] arcadeCabs;
    [SerializeField] float timeToSwitch = 10;

    [HideInInspector] public GameObject currentCab;
    GameObject lastCab;
    
    MeshRenderer[] renderers;
    float onStartTime, onEndTime;

    readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    void Start()
    {
        foreach (GameObject i in arcadeCabs)
        {
            PowerOff(i);
        }
        
        PowerRandomOn();
    }

    void FixedUpdate()
    {
        if (Time.time - onStartTime >= onEndTime)
        {
            PowerRandomOn();
        }
    }

    void PowerRandomOn()
    {
        // TODO: add checking so it doesn't pick the same one twice in a row
        PowerOn(arcadeCabs[Random.Range(0, arcadeCabs.Length)]);
    }

    void PowerOn(GameObject cab)
    {
        cab.GetComponent<MeshRenderer>().materials[1].SetColor(EmissionColor, Color.blue);
        
        onStartTime = Time.time;
        onEndTime = timeToSwitch + Random.Range(-(timeToSwitch/5), (timeToSwitch/5));
        
        if (currentCab != null)
        {
            PowerOff(lastCab);
        }
        
        currentCab = cab;
    }

    void PowerOff(GameObject cab)
    {
        cab.GetComponent<MeshRenderer>().materials[1].SetColor(EmissionColor, Color.black);
        
        lastCab = currentCab;
    }
}
