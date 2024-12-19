using UnityEngine;

public class CameraPin : MonoBehaviour, EvieCoreUpdate
{
    [Header("Camera Pin Settings")]
    [Tooltip("The position to which the camera should be pinned.")]
    public Transform cameraPosition;

    void Start()
    {
        UpdateManager.Instance.Register(this);
    }

    public void OnUpdate()
    {
        transform.position = cameraPosition.position;
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.Unregister(this);
    }
}
