using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRound2Script : MonoBehaviour
{
    [SerializeField] GameObject ArcadeTokenSpawner;

    PlayerObjectDetection playerObjectDetection;
    TamagotchiController tc;

    // Start is called before the first frame update
    void Start()
    {
        playerObjectDetection = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerObjectDetection>();
        ArcadeTokenSpawner.SetActive(false);

        tc = FindObjectOfType<TamagotchiController>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                playerObjectDetection.hasCoin = true;
                ArcadeTokenSpawner.SetActive(true);
                Destroy(gameObject);
                
                tc.SetUpdatingStats(true);
            }
        }
    }
}
