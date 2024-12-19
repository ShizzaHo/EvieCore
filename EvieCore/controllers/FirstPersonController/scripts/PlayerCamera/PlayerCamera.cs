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
    public Transform orientation;

    private Transform camera;

    private float xRotation;
    private float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camera = GetComponentInChildren<Camera>().transform;

        xRotation = transform.localEulerAngles.x;
        yRotation = transform.localEulerAngles.y;

        UpdateManager.Instance.Register(this);
    }

    public void OnUpdate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.Unregister(this);
    }
}