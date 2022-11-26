using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MaterialSwitcherScript : MonoBehaviour
{
    [SerializeField] GameObject[] arcadeCabs;
    [SerializeField] float timeToSwitch = 10;

    [HideInInspector] public GameObject currentCab; // Current cab turned on
    MeshRenderer[] cabRenderers; // Array of MeshRenderers of the cabinets
    MeshRenderer lastRend; // The last MeshRenderer turned on
    
    float onStartTime, onEndTime;
    readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    void Start()
    {
        cabRenderers = new MeshRenderer[arcadeCabs.Length];
        
        for (int i = 0; i < arcadeCabs.Length; i++)
        {
            // The first MeshRenderer it finds in the children
            cabRenderers[i] = arcadeCabs[i].GetComponentInChildren<MeshRenderer>();
            
            PowerOff(cabRenderers[i]);
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
        PowerOn(cabRenderers[Random.Range(0, cabRenderers.Length)]);
    }

    void PowerOn(MeshRenderer rend)
    {
        rend.materials[1].SetColor(EmissionColor, Color.blue);
        
        onStartTime = Time.time;
        onEndTime = timeToSwitch + Random.Range(-(timeToSwitch/5), (timeToSwitch/5));

        if (currentCab != null)
        {
            PowerOff(lastRend);
        }
        
        currentCab = rend.transform.parent.gameObject;
    }

    void PowerOff(MeshRenderer rend)
    {
        rend.materials[1].SetColor(EmissionColor, Color.black);
        
        lastRend = currentCab.GetComponentInChildren<MeshRenderer>();
    }
}
