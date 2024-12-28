using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCamera : MonoBehaviour, EvieCoreUpdate
{
    [Header("Camera Sensitivity Settings")]
    [Tooltip("Sensitivity of camera rotation on the X-axis (horizontal).")]
    [Range(0.1f, 1000f)]
    public float sensitivityX = 1f;

    [Tooltip("Sensitivity of camera rotation on the Y-axis (vertical).")]
    [Range(0.1f, 1000f)]
    public float sensitivityY = 1f;

    [Header("Vertical Rotation Limits")]
    [Tooltip("Minimum vertical angle the camera can rotate (looking up).")]
    [Range(-180f, 0f)]
    public float minVerticalAngle = -90f;

    [Tooltip("Maximum vertical angle the camera can rotate (looking down).")]
    [Range(0f, 180f)]
    public float maxVerticalAngle = 90f;

    [Header("Camera References")]
    [Tooltip("The orientation transform used to rotate the player horizontally.")]
    [Required]
    public Transform orientation;

    [Header("Roll Settings")]
    [Tooltip("Maximum angle for camera roll (in degrees).")]
    public float maxRollAngle = 10f;

    [Tooltip("Speed of roll adjustment.")]
    public float rollSpeed = 5f;

    private Transform camera;
    private float xRotation;
    private float yRotation;
    private float zRotation;
    private float targetZRotation;
    private float smoothingFactor = 0.1f; // Сглаживание
    private float lastMouseX = 0f;
    private Vector2 currentMouseLook;
    private Vector2 appliedMouseLook;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camera = GetComponentInChildren<Camera>().transform;

        if (UpdateManager.Instance != null)
        {
            UpdateManager.Instance.Register(this);
        }
        else
        {
            Debug.LogError($"[EVIECORE/ERROR] UpdateManager not found! Make sure it is added to the scene before using it {gameObject.name}.");
        }

        if (StateManager.Instance == null)
        {
            Debug.LogError($"[EVIECORE/ERROR] StateManager not found! Make sure it is added to the scene before using it {gameObject.name}.");
        }
    }

    public void OnUpdate()
    {
        if (!StateManager.Instance.IsCurrentState("playing")) return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        // Добавляем сглаживание движения мыши
        currentMouseLook = new Vector2(mouseX, mouseY);
        appliedMouseLook = Vector2.Lerp(appliedMouseLook, currentMouseLook, smoothingFactor);

        yRotation += appliedMouseLook.x;
        xRotation -= appliedMouseLook.y;

        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        // Рассчитываем целевой угол наклона по оси Z
        targetZRotation = -appliedMouseLook.x * maxRollAngle;
        targetZRotation = Mathf.Clamp(targetZRotation, -maxRollAngle, maxRollAngle);

        // Сглаживаем наклон по оси Z
        zRotation = Mathf.Lerp(zRotation, targetZRotation, smoothingFactor);

        // Применяем повороты
        transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.Unregister(this);
    }
}
