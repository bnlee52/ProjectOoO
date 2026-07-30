using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Light))]
public sealed class FlashlightController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference flashlightAction;

    private Light flashlight;

    /// <summary>
    /// True when the flashlight is emitting light.
    /// </summary>
    public bool IsOn => flashlight.enabled;

    private void Awake()
    {
        flashlight = GetComponent<Light>();

        if (flashlight == null)
        {
            Debug.LogError($"{nameof(FlashlightController)} requires a Light component.", this);
            enabled = false;
            return;
        }

        if (flashlightAction == null)
        {
            Debug.LogError("Flashlight Input Action is not assigned.", this);
            enabled = false;
            return;
        }

        SetFlashlightEnabled(false);
    }

    private void OnEnable()
    {
        flashlightAction.action.Enable();
        flashlightAction.action.performed += HandleFlashlightPressed;
    }

    private void OnDisable()
    {
        flashlightAction.action.performed -= HandleFlashlightPressed;
        flashlightAction.action.Disable();
    }

    private void HandleFlashlightPressed(InputAction.CallbackContext _)
    {
        ToggleFlashlight();
    }

    public void ToggleFlashlight()
    {
        SetFlashlightEnabled(!IsOn);
    }

    public void SetFlashlightEnabled(bool enabled)
    {
        if (flashlight.enabled == enabled)
            return;

        flashlight.enabled = enabled;

        // Future expansion:
        // audioSource.PlayOneShot(clickSound);
        // animator.SetBool("Enabled", enabled);
        // OnFlashlightChanged?.Invoke(enabled);
    }
}