using NaughtyAttributes;
using UnityEngine;

namespace Eviecore
{
    public class CameraPin : MonoBehaviour, EvieCoreUpdate
    {
        [Header("Camera Pin Settings")]
        [Tooltip("The position to which the camera should be pinned.")]
        [Required]
        public Transform cameraPosition;

        void Start()
        {
            if (UpdateManager.Instance != null)
            {
                UpdateManager.Instance.Register(this);
            }
            else
            {
                Debug.LogError($"[EVIECORE/ERROR] UpdateManager not found! Make sure it is added to the scene before using it {gameObject.name}.");
            }
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
}