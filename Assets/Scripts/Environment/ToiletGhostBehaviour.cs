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

    }

    void FixedUpdate()
    {
        ghostLook();
        ghostPlayerTracking();
    }

    //Need to fix so it detects proper collision point
    private void OnTriggerStay(Collider other)
    {
        Debug.Log($"bonk {other.gameObject.name} {other.gameObject.tag}");
        if (other.gameObject.CompareTag("FlashlightDetection"))
        {
            Debug.Log("ouch");
            playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (playercontroller.flashlightOn)
            {
                audioManagerMenu.ghostHitSFX.PlayOneShot(audioManagerMenu.ghostHitSFX.clip);
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("You Die");
        }
    }

    /// <summary>
    /// Keeps Ghost eyes Locked on player
    /// </summary>
    void ghostLook()
    {
        transform.LookAt(mainCam);
        transform.localEulerAngles = Vector3.up * transform.localEulerAngles.y;
    }

    /// <summary>
    /// Tracks player current location
    /// </summary>
    void ghostPlayerTracking()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, homingSpeed * speedVariation);
    }


}