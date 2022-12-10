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
    TamagotchiController tc;

    [HideInInspector] public bool hasPassedCheckpoint1, hasPassedCheckpoint2, hasPassedCheckpoint3;

    void Start()
    {
        lightSwitchBool = bathroomStartSwitch.GetComponent<LightSwitchBool>();
        gateControlScript = gateControl.GetComponent<GateControlScript>();

        CheckPoint();
    }

    public void CheckPoint()
    {
        if (hasPassedCheckpoint3)
        {
            CheckPoint3();
        }
        else if (hasPassedCheckpoint2)
        {
            CheckPoint2();
        }
        else if (hasPassedCheckpoint1)
        {
            CheckPoint1();
        }
        else
        {
            CheckPoint0();
        }

        lightSwitchBool.lightOn = true;
    }

    /// <summary>
    /// Called at start of game
    /// </summary>
    void CheckPoint0()
    {
        gateControlScript.toyStoreGateDown = false;
        gateControlScript.arcadeGateADown = true;
        gateControlScript.arcadeGateBDown = true;
        gateControlScript.bathroomGateDown = true;
        gateControlScript.foodCourtGateDown = true;
        gateControlScript.frontDoorGateDown = false;
        gateControlScript.tutorialGateDown = false;

        foodCourtSpawner.SetActive(false);

        level1StartTrigger.SetActive(true);
        level2StartTrigger.SetActive(true);
    }

    /// <summary>
    /// In effect after player picks up coke can the first time
    /// </summary>
    void CheckPoint1()
    {
        gateControlScript.toyStoreGateDown = false;
        gateControlScript.arcadeGateADown = true;
        gateControlScript.arcadeGateBDown = true;
        gateControlScript.bathroomGateDown = true;
        gateControlScript.foodCourtGateDown = false;
        gateControlScript.frontDoorGateDown = true;
        gateControlScript.tutorialGateDown = false;

        foodCourtSpawner.SetActive(false);

        level1StartTrigger.SetActive(true);
        level2StartTrigger.SetActive(true);

        tc.roundMusic.Stop();
    }

    /// <summary>
    /// In effect after first Tama altar evolution
    /// </summary>
    void CheckPoint2()
    {
        gateControlScript.toyStoreGateDown = false;
        gateControlScript.arcadeGateADown = false;
        gateControlScript.arcadeGateBDown = false;
        gateControlScript.bathroomGateDown = true;
        gateControlScript.foodCourtGateDown = false;
        gateControlScript.frontDoorGateDown = true;
        gateControlScript.tutorialGateDown = false;

        foodCourtSpawner.SetActive(true);

        level1StartTrigger.SetActive(false);
        level2StartTrigger.SetActive(true);

        tc.roundMusic.Stop();
    }

    /// <summary>
    /// In effect after second Tama altar evolution
    /// </summary>
    void CheckPoint3()
    {
        gateControlScript.toyStoreGateDown = false;
        gateControlScript.arcadeGateADown = false;
        gateControlScript.arcadeGateBDown = false;
        gateControlScript.bathroomGateDown = false;
        gateControlScript.foodCourtGateDown = false;
        gateControlScript.frontDoorGateDown = true;
        gateControlScript.tutorialGateDown = false;

        foodCourtSpawner.SetActive(true);

        level1StartTrigger.SetActive(false);
        level2StartTrigger.SetActive(false);

        tc.roundMusic.Stop();
    }
}
