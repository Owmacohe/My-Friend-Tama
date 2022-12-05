using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRound2Script : MonoBehaviour
{
    [SerializeField] GameObject ArcadeTokenSpawner;

    PlayerObjectDetection playerObjectDetection;

    // Start is called before the first frame update
    void Start()
    {
        playerObjectDetection = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerObjectDetection>();
        ArcadeTokenSpawner.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                playerObjectDetection.hasCoin = true;
                ArcadeTokenSpawner.SetActive(true);
                Destroy(gameObject);

            }
        }
    }
}
