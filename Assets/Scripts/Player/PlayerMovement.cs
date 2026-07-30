using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.action.Disable();
        }
    }

    private void Update()
    {
        if (moveAction == null || moveAction.action == null)
        {
            return;
        }

        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = Vector3.zero;

        if (input.sqrMagnitude > 0.01f)
        {
            move = transform.right * input.x + transform.forward * input.y;
            move = Vector3.ClampMagnitude(move, 1f);
        }

        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 movement = move * walkSpeed * Time.deltaTime;
        movement += velocity * Time.deltaTime;

        controller.Move(movement);
    }
}
