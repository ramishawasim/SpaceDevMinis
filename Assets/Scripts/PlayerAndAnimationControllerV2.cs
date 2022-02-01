using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAndAnimationControllerV2 : MonoBehaviour
{
    // declare reference variables
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    // variables to store optimized setter/getter parameter IDs
    int isWalkingHash;
    int isRunningHash;

    // variables to store player input values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 15.0f;
    float walkMultiplier = 2.0f;
    float runMultiplier = 8.0f;
    float gravity = -9.81f;
    float rotationSpeed = 5f;

    private Transform cameraMainTransform;

    private void Awake()
    {
        // initial setting of reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;

        cameraMainTransform = Camera.main.transform;
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
        isRunPressed = context.ReadValueAsButton();
    }
    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            // Stick to ground if walking
            currentMovement.y = -0.05f;
        }
        else
        {
            // else apply gravity
            currentMovement.y += gravity * 2.0f * Time.deltaTime;
        }
    }

    void handleRotation()
    {
        if (currentMovementInput != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(currentMovementInput.x, currentMovementInput.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    void handleAnimation()
    {
        // get parameter values from animator
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

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
        handleGravity();
        handleRotation();
        handleAnimation();

        // Final movement
        if (isRunPressed) {
            characterController.Move(currentRunMovement * Time.deltaTime);
        } else {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        characterController.Move(currentMovement * Time.deltaTime);
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
}
