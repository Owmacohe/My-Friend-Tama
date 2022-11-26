using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostController : MonoBehaviour
{
    [SerializeField] float followSpeed = 1;
    [SerializeField] float shuffleSpeed = 2;
    
    Transform mainCam;
    Rigidbody rb;
    Transform player;
    
    void Start()
    {
        mainCam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
        
        GetComponent<BoxCollider>().center = Vector3.up * Random.Range(0.25f, 2f);
    }

    void FixedUpdate()
    {
        if (transform.position.y <= -100)
        {
            Destroy(gameObject);
        }
        
        transform.LookAt(mainCam);
        transform.localEulerAngles = Vector3.up * transform.localEulerAngles.y;

        if (Vector3.Distance(transform.position, player.position) <= 50)
        {
            rb.AddForce(new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * shuffleSpeed);

            if (Time.time % 4 == 0)
            {
                rb.AddForce(Vector3.up * (Random.Range(0f, 4f) * shuffleSpeed));
            }

            if (Mathf.Abs(player.position.y - transform.position.y) < 5)
            {
                Vector3 dir = (player.position - transform.position).normalized;
                Vector3 flattenedDir = new Vector3(dir.x, 0, dir.z);
                rb.velocity = flattenedDir * followSpeed;   
            }   
        }
    }
}