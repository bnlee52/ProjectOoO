using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Input")]
    [SerializeField] private InputActionReference lookAction;

    [Header("Settings")]
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float maxPitch = 89f;

    private Vector2 lookInput;
    private float pitch;

    private void Awake()
    {
        if (player == null)
            player = transform.parent;
    }

    private void OnEnable()
    {
        if (lookAction != null)
            lookAction.action.Enable();
    }

    private void OnDisable()
    {
        if (lookAction != null)
            lookAction.action.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        lookInput = lookAction.action.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        RotatePlayer();
        RotateCamera();
    }

    private void RotatePlayer()
    {
        float yaw = lookInput.x * sensitivity * Time.deltaTime;
        player.Rotate(Vector3.up * yaw);
    }

    private void RotateCamera()
    {
        pitch -= lookInput.y * sensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(
            pitch,
            -maxPitch,
            maxPitch);

        transform.localRotation = Quaternion.Euler(
            pitch,
            0f,
            0f);
    }
}