using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference runControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    private float playerSpeed = 6.5f;
    [SerializeField]
    private float playerRunSpeedMult = 2.0f;
    [SerializeField]
    private float jumpHeight = 0.75f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 5f;

    private CharacterController controller;
    private Animator animator;
    private Vector3 currentMovement;
    private Vector2 inputMovement;
    private bool isMovementPressed;
    private bool isRunPressed;
    private Transform cameraMainTransform;

    private float playerSpeedModifier = 1.0f;

    private void OnEnable()
    {
        movementControl.action.Enable();
        runControl.action.Enable();
        jumpControl.action.Enable();
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        runControl.action.Disable();
        jumpControl.action.Disable();
    }

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cameraMainTransform = Camera.main.transform;
        currentMovement.y = -0.01f;
    }

    void Update()
    {
        handleInputDirection();
        handleGravity();
        handleRotation();
        handleJump();

        // Final movement
        controller.Move(currentMovement * Time.deltaTime * playerSpeed * playerSpeedModifier);

    }

    void handleInputDirection()
    {
        // The camera vectors have y values, they must be removed
        Vector3 cameraMainTransformForward = cameraMainTransform.forward;
        Vector3 cameraMainTransformRight = cameraMainTransform.right;
        cameraMainTransformForward.y = 0f;
        cameraMainTransformRight.y = 0f;

        // Directional input
        inputMovement = movementControl.action.ReadValue<Vector2>();

        // Apply input movement to final movement vector
        currentMovement.x = inputMovement.x;
        currentMovement.z = inputMovement.y;

        // Modify currentMovement vector to match camera
        Vector3 movementModifiedByCamera = cameraMainTransformForward * currentMovement.z + cameraMainTransformRight * currentMovement.x;
        currentMovement.x = movementModifiedByCamera.x;
        currentMovement.z = movementModifiedByCamera.z;
    }

    void handleGravity()
    {
        if (controller.isGrounded)
        {
            // Stick to ground if walking
            currentMovement.y = -0.05f;
        } else
        {
            // else apply gravity
            currentMovement.y += gravityValue * Time.deltaTime;
        }
    }

    void handleRotation()
    {
        if (inputMovement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(inputMovement.x, inputMovement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    void handleJump()
    {
        if (jumpControl.action.triggered && controller.isGrounded)
        {
            currentMovement.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
    }
}
