using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ToiletGhostBehaviour : MonoBehaviour
{
    [SerializeField] float homingSpeed = 0.01f;
    
    Transform mainCam;
    GameObject player;
    Collider flashlightCollider;
    PlayerController playerController;
    int speedVariation;
    AudioManagerMenu audioManagerMenu;

    void Start()
    {
        mainCam = Camera.main.transform;

        //Creates slight speed variation based on homingSpeed
        speedVariation = Random.Range(1, 6);

        audioManagerMenu = FindObjectOfType<AudioManagerMenu>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate()
    {
        GhostLook();

        if (!playerController.isPaused)
        {
            GhostPlayerTracking();   
        }
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
        if (playerController == null)
            playerController = FindObjectOfType<PlayerController>();
        
        if (playerController.flashlightOn)
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