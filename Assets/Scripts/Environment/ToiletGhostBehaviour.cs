using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ToiletGhostBehaviour : MonoBehaviour
{
    [SerializeField] float homingSpeed = 0.01f;
    
    Transform mainCam;
    GameObject player;
    Collider flashlightCollider;
    PlayerController playercontroller;
    int speedVariation;
    AudioManagerMenu audioManagerMenu;

    void Start()
    {
        mainCam = Camera.main.transform;

        //Creates slight speed varation based on homingSpeed
        speedVariation = Random.Range(1, 6);

        audioManagerMenu = GameObject.FindGameObjectWithTag("audioManager").gameObject.GetComponent<AudioManagerMenu>();
        playercontroller = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate()
    {
        GhostLook();
        GhostPlayerTracking();
    }

    //Need to fix so it detects proper collision point
    void OnTriggerStay(Collider other)
    {
        Debug.Log($"bonk {other.gameObject.name} {other.gameObject.tag}");
        if (other.gameObject.CompareTag("FlashlightDetection"))
        {
            GhostDestroy();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().KillPlayer();
            FindObjectOfType<CheckpointScript>().CheckPoint();
            FindObjectOfType<TamagotchiController>().tama.ResetStats();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FlashlightDetection"))
        {
            GhostDestroy();
        }
    }

    void GhostDestroy()
    {
        if (playercontroller == null)
            playercontroller = FindObjectOfType<PlayerController>();
        
        if (playercontroller.flashlightOn)
        {
            audioManagerMenu.ghostHitSFX.PlayOneShot(audioManagerMenu.ghostHitSFX.clip);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Keeps Ghost eyes Locked on player
    /// </summary>
    void GhostLook()
    {
        transform.LookAt(mainCam);
        transform.localEulerAngles = Vector3.up * transform.localEulerAngles.y;
    }

    /// <summary>
    /// Tracks player current location
    /// </summary>
    void GhostPlayerTracking()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, homingSpeed * speedVariation);
    }
}