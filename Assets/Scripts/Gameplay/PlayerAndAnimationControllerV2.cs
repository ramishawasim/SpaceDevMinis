using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerAndAnimationControllerV2 : MonoBehaviour
{
    // declare reference variables
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    // variables to store optimized setter/getter parameter IDs
    int isWalkingHash;
    int isRunningHash;
    bool isWalking;
    bool isRunning;

    // variables to store player input values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;

    bool isMovementPressed;
    bool isRunPressed;

    // Jumping stuff
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 14.0f;
    float maxJumpTime = 4.0f;
    bool isJumping = false;
    int isJumpingHash;
    bool isJumpAnimating = false;
    float fallMultiplier = 2.0f;
    bool wasRunningWhenJumped = false;
    Vector3 movementWhenJumped;
    float movementWhenJumpedDamp = 5.0f;

    // Push Variables
    float runPushPower = 4.0f;
    float walkPushPower = 2.0f;


    float walkMultiplier = 4.25f;
    float runMultiplier = 8.5f;
    float gravity = -9.81f;
    float rotationSpeed = 7.5f;
    float runningRotationSpeed = 12.5f;

    // Death stuff
    public GameObject playerObject;
    public Vector3 respawnLocation;
    public bool isDead = false;
    int isDeadHash;

    float falloffThreshold = -3f;

    bool isSneaking = true;

    private Transform cameraMainTransform;

    public SkinnedMeshRenderer skinnedMesh;
    public VisualEffect snowVFX;

    // Platform Stuff

    bool onPlatform;
    Vector3 externalMovement;

    //flap

    public AudioSource JumpSound;

    private void Awake()
    {
        // initial setting of reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerObject = this.gameObject;
        respawnLocation = playerObject.transform.position;

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isDeadHash = Animator.StringToHash("isDead");

        // set player input callbacks
        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        // playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;

        cameraMainTransform = Camera.main.transform;

        setupJumpVariables();
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2.0f * maxJumpHeight) / Mathf.Pow(timeToApex, 2.0f);
        initialJumpVelocity = (2.0f * maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
            isJumpAnimating = true;
            isJumping = true;

            JumpSound.Play();

            currentMovement.y = initialJumpVelocity;
            appliedMovement.y = initialJumpVelocity;
            movementWhenJumped.x = currentMovement.x;
            movementWhenJumped.z = currentMovement.z;
            if (isRunPressed)
            {
                wasRunningWhenJumped = true;
            } else
            {
                wasRunningWhenJumped = false;
            }
        } else if (!isJumpPressed && isJumping && characterController.isGrounded) {
            isJumping = false;
        }
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        // Directional input
        currentMovementInput = context.ReadValue<Vector2>();

        // The camera vectors have y values, they must be removed
        Vector3 cameraMainTransformForward = cameraMainTransform.forward;
        Vector3 cameraMainTransformRight = cameraMainTransform.right;
        cameraMainTransformForward.y = 0f;
        cameraMainTransformRight.y = 0f;

        // Apply input movement to movement vector
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;

        // Modify currentMovement and currentRunMovement vector to match camera
        Vector3 movementModifiedByCamera = cameraMainTransformForward * currentMovement.z + cameraMainTransformRight * currentMovement.x;
        currentMovement.x = movementModifiedByCamera.x * walkMultiplier;
        currentMovement.z = movementModifiedByCamera.z * walkMultiplier;

        currentRunMovement.x = movementModifiedByCamera.x * runMultiplier;
        currentRunMovement.z = movementModifiedByCamera.z * runMultiplier;

        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void onRun(InputAction.CallbackContext context)
    {
        if (!isRunPressed)
        {
            isRunPressed = context.ReadValueAsButton();
        } else {
            isRunPressed = false;
        }
    }
    void handleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        
        if (characterController.isGrounded)
        {
            if (isJumpAnimating)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
            }
            // Stick to ground if walking
            currentMovement.y = -0.05f;
            appliedMovement.y = -0.05f;
        } else if (isFalling) {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * 2.0f * fallMultiplier * Time.deltaTime);
            appliedMovement.y = Mathf.Max((previousYVelocity + currentMovement.y) * 0.5f, -20.0f);
        } else {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * 2.0f * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * 0.5f;
        }
    }

    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    void handleRotation()
    {
        if (!isDead) {
            float currentRotationSpeed;
            if (!isRunPressed) {
                currentRotationSpeed = rotationSpeed;
            } else {
                currentRotationSpeed = runningRotationSpeed;
            }

            if (currentMovementInput != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(currentMovementInput.x, currentMovementInput.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * currentRotationSpeed);
            }
        }
    }

    void handleAnimation()
    {
        // get parameter values from animator
        isWalking = animator.GetBool(isWalkingHash);
        isRunning = animator.GetBool(isRunningHash);

        // start walking if movement is pressed and not already walking
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }

        // stop walking if movement is not pressed and not already walking
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        // run if movement and run are pressed and not currently running
        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        // stop running if movement or run are not pressed and currently running
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void Update()
    {
        handleRotation();
        handleAnimation();

        // Final movement
        if (isJumpAnimating && wasRunningWhenJumped)
        {
            appliedMovement.x = (movementWhenJumped.x / movementWhenJumpedDamp) + currentRunMovement.x;
            appliedMovement.z = (movementWhenJumped.z / movementWhenJumpedDamp) + currentRunMovement.z;
        } else if (isJumpAnimating) {
            appliedMovement.x = (movementWhenJumped.x / movementWhenJumpedDamp) + currentMovement.x;
            appliedMovement.z = (movementWhenJumped.z / movementWhenJumpedDamp) + currentMovement.z;
        } else if (isRunPressed) {
            appliedMovement.x = currentRunMovement.x;
            appliedMovement.z = currentRunMovement.z;
        } else {
            appliedMovement.x = currentMovement.x;
            appliedMovement.z = currentMovement.z;
        }

        characterController.Move((appliedMovement - externalMovement) * Time.deltaTime);

        handleGravity();
        handleJump();

        checkSneak();

        if (transform.position.y < falloffThreshold)
        {
            onDeath();
        }

        if (characterController.velocity.y > 0f)
        {
            Physics.IgnoreLayerCollision( 3, 7, true);
        } else
        {
            Physics.IgnoreLayerCollision(3, 7, false);
        }
    }

    void OnEnable()
    {
        // enable character controls action map
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        // disable character controls action map
        playerInput.CharacterControls.Disable();
    }

    void checkSneak()
    {
        if (isRunning || isJumpAnimating)
        {
            isSneaking = false;
        } else
        {
            isSneaking = true;
        }
    }

    // Push Logic and Platforms
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Platform")
        {
            onPlatform = true;
            externalMovement = hit.collider.gameObject.GetComponent<MovingPlatform>().publicPlatformVectorDirection;
            externalMovement.y = externalMovement.y * 1.75f;
        } else
        {
            Vector3 none = new Vector3();
            onPlatform = false;
            externalMovement = none;
        }


        Rigidbody body = hit.collider.attachedRigidbody;
        float pushPower;

        if (isRunPressed)
        {
            pushPower = runPushPower;
        } else {
            pushPower = walkPushPower;
        }

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }

    public void onDeath()
    {
        // Death needs delay
        if (!isDead) 
        {
            StartCoroutine(deathCoroutine());
        }
    }

    IEnumerator deathCoroutine()
    {
        // death logic
        animator.SetBool(isDeadHash, true);
        isDead = true;
        characterController.enabled = false;
        yield return new WaitForSeconds(2);
        animator.SetBool(isDeadHash, false);

        if (skinnedMesh.enabled == false)
        {
            skinnedMesh.enabled = true;
        }

        isDead = false;
        playerObject.transform.position = respawnLocation;

        if (transform.position.y < 25f)
        {
            snowVFX.Stop();
        }
        characterController.enabled = true;
    }

    public void setRespawnLocation(Vector3 fireLocation)
    {
        respawnLocation = fireLocation;
    }
}
