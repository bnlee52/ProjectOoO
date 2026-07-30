using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private InputActionReference mouseLookAction;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float pitchClamp = 90f;

    private float pitchRotation;
    private Vector2 mouseInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        if (mouseLookAction != null)
        {
            mouseLookAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (mouseLookAction != null)
        {
            mouseLookAction.action.Disable();
        }
    }

    private void Update()
    {
        if (mouseLookAction == null || mouseLookAction.action == null)
        {
            return;
        }

        mouseInput = mouseLookAction.action.ReadValue<Vector2>();

        float yaw = mouseInput.x * mouseSensitivity * Time.deltaTime;

        if (transform.parent != null)
        {
            transform.parent.Rotate(Vector3.up * yaw);
        }
    }

    private void LateUpdate()
    {
        if (mouseLookAction == null || mouseLookAction.action == null)
        {
            return;
        }

        float pitch = mouseInput.y * mouseSensitivity * Time.deltaTime;

        pitchRotation -= pitch;
        pitchRotation = Mathf.Clamp(pitchRotation, -pitchClamp, pitchClamp);

        transform.localRotation = Quaternion.Euler(pitchRotation, 0f, 0f);
    }
}
