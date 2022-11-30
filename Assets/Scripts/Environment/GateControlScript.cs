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

    AudioManagerMenu audioManagerMenu;

    public bool ToyStoreGateDown = false;
    public bool ArcadeGateADown = false;
    public bool ArcadeGateBDown = false;
    public bool BathroomGateDown = false;
    public bool FoodCourtGateDown = false;
    public bool FrontDoorGateDown = false;

    bool gateSFXPlayed = false;

    void Start()
    {
        audioManagerMenu = FindObjectOfType<AudioManagerMenu>();
        GateSwitcher();
    }
    // Update is called once per frame
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
    }

   
}
