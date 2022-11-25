using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostController : MonoBehaviour
{
    [SerializeField] float shuffleSpeed = 2;
    
    Transform mainCam;
    Rigidbody rb;

    void Start()
    {
        mainCam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        
        GetComponent<BoxCollider>().center = Vector3.up * Random.Range(0.25f, 1.5f);
    }

    void FixedUpdate()
    {
        transform.LookAt(mainCam);
        transform.localEulerAngles = Vector3.up * transform.localEulerAngles.y;
        
        rb.AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * shuffleSpeed);

        if (Time.time % 4 == 0)
        {
            rb.AddForce(Vector3.up * (Random.Range(0f, 4f) * shuffleSpeed));
        }
    }
}