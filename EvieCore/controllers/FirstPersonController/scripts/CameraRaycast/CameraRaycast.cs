using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    [Serializable]
    public class RaycastAction
    {
        [Tooltip("The tag of the object to react to.")]
        public string tag;

        [Tooltip("Action to perform on detection: true - hover, false - leave.")]
        public UnityEvent<bool> action;
    }

    private GameObject objectHit;

    [Header("Messaging")]
    [Tooltip("Enable sending messages through the MessageManager.")]
    public bool sendMessage = false;

    [Tooltip("Default message sent on hover or leave: true - hover, false - leave.")]
    [ShowIf("sendMessage")]
    public string defaultAddtiveMessage;

    [Header("Action Settings")]
    [Tooltip("List of tags and actions to perform upon detection.")]
    public List<RaycastAction> actions;

    [Header("Ray Settings")]
    [Tooltip("Maximum distance of the ray.")]
    [Range(1f, 500f)] // Limit the range of maxDistance in the Inspector
    public float maxDistance = 100f;

    [Tooltip("Layer mask to filter objects the ray can interact with.")]
    public LayerMask raycastMask;

    [Header("Debugging")]
    [Tooltip("Enable ray visualization in Gizmos.")]
    public bool drawDebugRay = false;

    [Tooltip("Offset for the ray's starting position from the camera.")]
    [Range(0f, 5f)] // Define a range for the ray origin offset
    [ShowIf("drawDebugRay")]
    public float rayOriginOffset = 0.1f;

    [Header("References")]
    [Tooltip("Reference to the player's camera. Defaults to the main camera if left empty.")]
    [Required]
    public Camera _camera;

    private string _lastHitTag = null; // The tag of the last object the player looked at

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
    }

    private void Update()
    {
        Vector3 rayOrigin = _camera.transform.position + _camera.transform.forward * rayOriginOffset;
        Ray ray = new Ray(rayOrigin, _camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, raycastMask))
        {
            string currentTag = hit.collider.tag;

            if (_lastHitTag != currentTag) // Объект сменился
            {
                // Если был объект, отправляем "false" на прошлый
                if (_lastHitTag != null)
                {
                    TriggerAction(_lastHitTag, false);
                }

                // Отправляем "true" на новый объект
                TriggerAction(currentTag, true);
                _lastHitTag = currentTag;
                objectHit = hit.collider.gameObject;
            }
        }
        else if (_lastHitTag != null) // Если больше ни на что не смотрим
        {
            TriggerAction(_lastHitTag, false);
            _lastHitTag = null;
        }
    }

    private void TriggerAction(string tag, bool state)
    {
        foreach (var action in actions)
        {
            if (action.tag == tag)
            {
                if (sendMessage)
                    MessageManager.Instance.SendMessage<bool>(defaultAddtiveMessage, state);
                action.action?.Invoke(state);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawDebugRay && _camera != null)
        {
            Vector3 rayOrigin = _camera.transform.position + _camera.transform.forward * rayOriginOffset;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayOrigin, _camera.transform.forward * maxDistance);
        }
    }
}
