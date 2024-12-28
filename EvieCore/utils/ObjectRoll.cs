using UnityEngine;

public class ObjectRoll : MonoBehaviour, EvieCoreUpdate
{
    [Header("Weapon Roll Settings")]
    [Tooltip("Maximum roll angle for the weapon (in degrees).")]
    public float maxRollAngle = 5f;

    [Tooltip("Speed of weapon roll adjustment.")]
    public float rollSpeed = 10f;

    [Header("Weapon Sway Settings")]
    [Tooltip("Amount of sway based on mouse movement.")]
    public float swayAmount = 0.02f;

    [Tooltip("Speed of sway adjustment.")]
    public float swaySpeed = 5f;

    [Tooltip("Maximum sway distance.")]
    public float maxSwayDistance = 0.1f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float targetZRotation;
    private float currentZRotation;
    private Vector3 targetPosition;
    private Vector3 currentPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;

        if (UpdateManager.Instance != null)
        {
            UpdateManager.Instance.Register(this);
        }
        else
        {
            Debug.LogError("[EVIECORE/ERROR] UpdateManager not found! Make sure it is added to the scene before using it.");
        }

        if (StateManager.Instance == null)
        {
            Debug.LogError("[EVIECORE/ERROR] StateManager not found! Make sure it is added to the scene before using it.");
        }
    }

    public void OnUpdate()
    {
        if (!StateManager.Instance.IsCurrentState("playing")) return;

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;

        // Calculate target rotation based on mouse movement
        targetZRotation = -mouseX * maxRollAngle;
        targetZRotation = Mathf.Clamp(targetZRotation, -maxRollAngle, maxRollAngle);

        // Smoothly interpolate to the target rotation
        currentZRotation = Mathf.Lerp(currentZRotation, targetZRotation, rollSpeed * Time.deltaTime);

        // Apply roll rotation
        transform.localRotation = initialRotation * Quaternion.Euler(0, 0, currentZRotation);

        // Calculate weapon sway based on mouse movement
        Vector3 swayOffset = new Vector3(-mouseX * swayAmount, -mouseY * swayAmount, 0);
        swayOffset = Vector3.ClampMagnitude(swayOffset, maxSwayDistance);

        // Smoothly interpolate to the target position
        targetPosition = initialPosition + swayOffset;
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, swaySpeed * Time.deltaTime);

        // Apply sway position
        transform.localPosition = currentPosition;
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.Unregister(this);
    }
}
