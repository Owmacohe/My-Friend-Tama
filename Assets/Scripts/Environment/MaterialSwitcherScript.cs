using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcherScript : MonoBehaviour
{
    [SerializeField] GameObject arcadeCab;
    public bool poweredOn = false;
   
    Material[] materials;
    Material screenMaterial;
    float timePassed = 0f;

    private void Start()
    {
        ///Keeps all screens black at start up until player collects coin
        materials = arcadeCab.GetComponent<Renderer>().materials;
        screenMaterial = materials[1];
        screenMaterial.SetColor("_EmissionColor", Color.black);
    }

    void Update()
    {
        ///For some reason, the game freezes up if poweredOn is true
        ///Leaving off for now until I can troubleshoot later
        while (poweredOn)
        {
            timePassed += Time.deltaTime;

            if (timePassed < 3f)
            {
                screenMaterial.SetColor("_EmissionColor", Color.yellow);
            } 
            else if (timePassed > 3f && timePassed < 6f)
            {
                screenMaterial.SetColor("_EmissionColor", Color.red);
            }
            else if (timePassed > 6f && timePassed < 9f)
            {
                screenMaterial.SetColor("_EmissionColor", Color.blue);
            }
            else if (timePassed > 12f)
            {
                timePassed = 0f;
            }
        }
    }
}
