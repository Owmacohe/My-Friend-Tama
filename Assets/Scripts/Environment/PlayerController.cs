using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float mouseSensitivity = 3.5f;
    public float walkSpeed = 10f;
    public float gravity = -13.0f;
    [Range(0.0f, 0.5f)] public float moveSmoothTime = 0.15f;
    [Range(0.0f, 0.3f)] public float mouseSmoothTime = 0.01f;

    private GameObject playerCamera;
    private float cameraPitch = 0.0f;
    private float velocityY = 0.0f;
    private CharacterController controller = null;
    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;


    private void Start()
    {
        //Locks cursor position at startup 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Get child objects at start up
        playerCamera = gameObject.transform.Find("MainCamera").gameObject;
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateMouseLook();
        UpdatePlayerMovement();
    }

    private void UpdateMouseLook()
    {
        //Gets new location every frame
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDelta, mouseSmoothTime);

        //Adjusts camera vertical with mouse sensitivity
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCamera.transform.localEulerAngles = Vector3.right * cameraPitch;

        //Adjusts camera horizontal with mouse sensitivity
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    private void UpdatePlayerMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
    }
}
