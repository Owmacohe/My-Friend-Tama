using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitcherScript : MonoBehaviour
{
    [SerializeField] GameObject arcadeCab;
   
    Material[] materials;
    Material screenMaterial;
    PlayerObjectDetection playerObjectDetection;
    
    void Start()
    {
        InitialSceenSetup();
        playerObjectDetection = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerObjectDetection>();
    }

    void Update()
    {
       PoweredOn();
    }

    void InitialSceenSetup()
    {
        ///Keeps all screens black at start up until player collects coin
        materials = arcadeCab.GetComponent<MeshRenderer>().materials;
        screenMaterial = materials[1];
        screenMaterial.SetColor("_EmissionColor", Color.black);
    }

    void PoweredOn()
    {
        screenMaterial.SetColor("_EmissionColor", Color.blue);
    }
}
