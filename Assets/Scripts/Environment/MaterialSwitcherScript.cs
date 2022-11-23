using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcherScript : MonoBehaviour
{
    [SerializeField] GameObject arcadeCab;
   
    Material[] materials;
    Material screenMaterial;
    PlayerObjectDetection playerObjectDetection;


    private void Start()
    {
        initialSceenSetup();
        playerObjectDetection = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerObjectDetection>();
    
    }

    void Update()
    {
       poweredOn();
    }

    void initialSceenSetup()
    {
        ///Keeps all screens black at start up until player collects coin
        materials = arcadeCab.GetComponent<MeshRenderer>().materials;
        screenMaterial = materials[1];
        screenMaterial.SetColor("_EmissionColor", Color.black);
    }

    void poweredOn()
    {
        screenMaterial.SetColor("_EmissionColor", Color.blue);
    }
}
