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

    AudioManagerMenu audioManagerMenu;

    public bool ToyStoreGateDown;
    public bool ArcadeGateADown;
    public bool ArcadeGateBDown;
    public bool BathroomGateDown;
    public bool FoodCourtGateDown;
    public bool FrontDoorGateDown;
    public bool TutorialGateDown;

    bool gateSFXPlayed;

    void Start()
    {
        audioManagerMenu = FindObjectOfType<AudioManagerMenu>();
    }
    
    void Update()
    {
        GateSwitcher();
    }

    void GateSwitcher()
    {
        ToyStoreGate.SetActive(ToyStoreGateDown);
        ArcadeGateA.SetActive(ArcadeGateADown);
        ArcadeGateB.SetActive(ArcadeGateBDown);
        BathroomGate.SetActive(BathroomGateDown);
        FoodCourtGate.SetActive(FoodCourtGateDown);
        FrontDoorGate.SetActive(FrontDoorGateDown);
        TutorialGate.SetActive(TutorialGateDown);
    }
}
