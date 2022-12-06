using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] GameObject foodCourtSpawner;
    [SerializeField] GameObject arcadeSpawner;
    [SerializeField] GameObject washroomSpawner;
    [SerializeField] GameObject bathroomStartSwitch;
    [SerializeField] GameObject washroomSpawnPoints;
    [SerializeField] GameObject bathroomLights;
    [SerializeField] GameObject gateControl;
    [SerializeField] GameObject level1StartTrigger;
    [SerializeField] GameObject level2StartTrigger;

    LightSwitchBool lightSwitchBool;
    GateControlScript gateControlScript;

    private void Start()
    {
        lightSwitchBool = bathroomStartSwitch.GetComponent<LightSwitchBool>();
        gateControlScript = gateControl.GetComponent<GateControlScript>();

        checkPoint2();
    }

    /// <summary>
    /// Called at start of game
    /// </summary>
    public void checkPoint0()
    {
        gateControlScript.ToyStoreGateDown = false;
        gateControlScript.ArcadeGateADown = true;
        gateControlScript.ArcadeGateBDown = true;
        gateControlScript.BathroomGateDown = true;
        gateControlScript.FoodCourtGateDown = true;
        gateControlScript.FrontDoorGateDown = false;
        gateControlScript.TutorialGateDown = false;

        foodCourtSpawner.SetActive(false);
        arcadeSpawner.SetActive(false);
        washroomSpawner.SetActive(false);
        washroomSpawnPoints.SetActive(false);
        bathroomLights.SetActive(true);

        lightSwitchBool.lightOn = false;

        level1StartTrigger.SetActive(true);
        level2StartTrigger.SetActive(true);
    }

    /// <summary>
    /// In effect after player picks up coke can the first time
    /// </summary>
    public void checkPoint1()
    {
        gateControlScript.ToyStoreGateDown = false;
        gateControlScript.ArcadeGateADown = true;
        gateControlScript.ArcadeGateBDown = true;
        gateControlScript.BathroomGateDown = true;
        gateControlScript.FoodCourtGateDown = false;
        gateControlScript.FrontDoorGateDown = true;
        gateControlScript.TutorialGateDown = false;

        foodCourtSpawner.SetActive(false);
        arcadeSpawner.SetActive(false);
        washroomSpawner.SetActive(false);
        washroomSpawnPoints.SetActive(false);
        bathroomLights.SetActive(true);

        lightSwitchBool.lightOn = false;

        level1StartTrigger.SetActive(true);
        level2StartTrigger.SetActive(true);
    }

    /// <summary>
    /// In effect after first Tama altar evolution
    /// </summary>
    public void checkPoint2()
    {
        gateControlScript.ToyStoreGateDown = false;
        gateControlScript.ArcadeGateADown = true;
        gateControlScript.ArcadeGateBDown = false;
        gateControlScript.BathroomGateDown = true;
        gateControlScript.FoodCourtGateDown = false;
        gateControlScript.FrontDoorGateDown = true;
        gateControlScript.TutorialGateDown = false;

        foodCourtSpawner.SetActive(true);
        arcadeSpawner.SetActive(false);
        washroomSpawner.SetActive(false);
        washroomSpawnPoints.SetActive(false);
        bathroomLights.SetActive(true);

        lightSwitchBool.lightOn = false;

        level1StartTrigger.SetActive(false);
        level2StartTrigger.SetActive(true);
    }

    /// <summary>
    /// In effect after second Tama altar evolution
    /// </summary>
    public void checkPoint3()
    {
        gateControlScript.ToyStoreGateDown = false;
        gateControlScript.ArcadeGateADown = true;
        gateControlScript.ArcadeGateBDown = false;
        gateControlScript.BathroomGateDown = false;
        gateControlScript.FoodCourtGateDown = false;
        gateControlScript.FrontDoorGateDown = true;
        gateControlScript.TutorialGateDown = false;

        foodCourtSpawner.SetActive(true);
        arcadeSpawner.SetActive(true);
        washroomSpawner.SetActive(false);
        washroomSpawnPoints.SetActive(true);
        bathroomLights.SetActive(true);

        lightSwitchBool.lightOn = false;

        level1StartTrigger.SetActive(false);
        level2StartTrigger.SetActive(false);
    }
}
