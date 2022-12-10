using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControlScript : MonoBehaviour
{
    [SerializeField] GameObject ToyStoreGate;
    [SerializeField] GameObject ArcadeGateA;
    [SerializeField] GameObject ArcadeGateB;
    [SerializeField] GameObject BathroomGate;
    [SerializeField] GameObject FoodCourtGate;
    [SerializeField] GameObject FrontDoorGate;
    [SerializeField] GameObject TutorialGate;

    //AudioManagerMenu audioManagerMenu;

    [HideInInspector] public bool toyStoreGateDown;
    [HideInInspector] public bool arcadeGateADown;
    [HideInInspector] public bool arcadeGateBDown;
    [HideInInspector] public bool bathroomGateDown;
    [HideInInspector] public bool foodCourtGateDown;
    [HideInInspector] public bool frontDoorGateDown;
    [HideInInspector] public bool tutorialGateDown;

    //bool gateSFXPlayed;
    
    void Update()
    {
        GateSwitcher();
    }

    void GateSwitcher()
    {
        ToyStoreGate.SetActive(toyStoreGateDown);
        ArcadeGateA.SetActive(arcadeGateADown);
        ArcadeGateB.SetActive(arcadeGateBDown);
        BathroomGate.SetActive(bathroomGateDown);
        FoodCourtGate.SetActive(foodCourtGateDown);
        FrontDoorGate.SetActive(frontDoorGateDown);
        TutorialGate.SetActive(tutorialGateDown);
    }
}
