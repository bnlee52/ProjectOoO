using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference sprintAction;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;

    [Header("Physics")]
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;

    private Vector2 moveInput;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if (moveAction != null)
            moveAction.action.Enable();
        if (sprintAction != null)
            sprintAction.action.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null)
            moveAction.action.Disable();
        if (sprintAction != null)
            sprintAction.action.Disable();
    }

    private void Update()
    {
        ReadInput();
        ApplyGravity();
        MovePlayer();
    }

    private void ReadInput()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
    }

    private void MovePlayer()
    {
        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        move = Vector3.ClampMagnitude(move, 1f);

        float speed =
            sprintAction.action.IsPressed()
            ? sprintSpeed
            : walkSpeed;

        Vector3 movement =
            move * speed +
            velocity;

        controller.Move(
            movement * Time.deltaTime);
    }
}