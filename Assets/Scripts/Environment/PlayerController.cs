using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character Controls")]
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 10.0f;
    [SerializeField] float runSpeed = 15.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField, Range(0.0f, 0.5f)] float moveSmoothTime = 0.15f;
    [SerializeField, Range(0.0f, 0.3f)] float mouseSmoothTime = 0.01f;

    [Header("Other")]
    [SerializeField] GameObject playerFlashlight;
    [SerializeField] TamagotchiController tc;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathScreen;

    [HideInInspector] public bool flashlightOn = true;

    // Character values
    GameObject playerCamera;
    float cameraPitch;
    float velocityY;
    CharacterController controller;
    Light playerLight;
    bool isLookingAtTama;
    bool isCrouched;
    //bool isRunning;
    float maxPlayerHeight;
    float startWalkSpeed;

    // Used to create character smoothing movement
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    Vector2 currentMouseDelta = Vector2.zero;
    //Vector2 currentMouseDeltaVelocity = Vector2.zero;
    
    [HideInInspector] public bool isPaused;
    Transform respawnPoint;
    [HideInInspector] public bool isDead;

    void Start()
    {
        Resume();

        // Get child objects at start up
        playerCamera = Camera.main.gameObject;
        controller = GetComponent<CharacterController>();
        playerLight = playerFlashlight.GetComponent<Light>();
        maxPlayerHeight = controller.height;
        startWalkSpeed = walkSpeed;

        respawnPoint = GameObject.FindGameObjectWithTag("Respawn").gameObject.transform;
        deathScreen.SetActive(false);
        
        MouseSwitcher.SetInactive();
    }

    void Update()
    {
        if (!isPaused)
        {
            RunControls();
            //CrouchControl();
            UpdateMouseLook();
            UpdatePlayerMovement();
            FlashlightControls();

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                tc.SlideTama(!tc.slideUp);
            }
        }

        if (!isDead && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if (pauseMenu.activeSelf)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;

        MouseSwitcher.SetActive();
        
        tc.pauseStartTime = Time.time;
    }

    public void Resume()
    {
        if (isDead)
            tc.tama.ResetStats();

        tc.pauseTime += Time.time - tc.pauseStartTime;
        print(tc.pauseTime);
        
        pauseMenu.SetActive(false);
        deathScreen.SetActive(false);
        
        Invoke(nameof(UnPause), 0.1f);
        
        MouseSwitcher.SetInactive();

        isDead = false;
    }

    void UnPause()
    {
        isPaused = false;
    }

    /// <summary>
    /// Updates mouse controls
    /// </summary>
    void UpdateMouseLook()
    {
        // Gets new location every frame
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDelta, mouseSmoothTime);

        // Adjusts camera vertical with mouse sensitivity
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCamera.transform.localEulerAngles = Vector3.right * cameraPitch;

        // Adjusts camera horizontal with mouse sensitivity
        transform.Rotate(Vector3.up * (currentMouseDelta.x * mouseSensitivity));
    }

    /// <summary>
    /// Updates player movement
    /// </summary>
    void UpdatePlayerMovement()
    {
        // Gets new location every frame and normalizes values
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        // Checks to see if player grounded and stops them from falling further
        if (controller.isGrounded && velocityY < 0)
        {
            velocityY = 0.0f;
        }

        velocityY += gravity * Time.deltaTime;

        // Applies movement through character controller
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);

        // Jump Action
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocityY += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    /// <summary>
    /// Controls light component inside PlayerCharacter
    /// </summary>
    void FlashlightControls()
    {
        if (Input.GetKeyDown("f") && !isLookingAtTama)
        {
            flashlightOn = !flashlightOn;
            SetFlashlight(flashlightOn, false);
        }
    }

    ///Player Crouch controls
    void CrouchControl()
    {
        if (Input.GetKeyDown("left ctrl"))
        {
            if (!isCrouched)
            {
                controller.height = maxPlayerHeight /2;
                isCrouched = true;
            }
            else
            {
                controller.height = maxPlayerHeight;
                isCrouched = false;
            }
        }

    }

    void RunControls()
    {
        if (Input.GetKeyDown("left shift"))
        {
            walkSpeed = runSpeed;
            //isRunning = true;
        }
        else if (Input.GetKeyUp("left shift"))
        {
            walkSpeed = startWalkSpeed;
            //isRunning = false;
        }
    }

    public void SetFlashlight(bool isOn, bool fromTama)
    {
        var lightFX = playerFlashlight.GetComponent<VLB.VolumetricLightBeam>();

        lightFX.enabled = isOn;
        playerLight.enabled = isOn;

        isLookingAtTama = fromTama && !isOn;
    }

    public void KillPlayer()
    {
        Pause();
        
        deathScreen.SetActive(true);
        transform.position = respawnPoint.position;

        isDead = true;
    }
}
