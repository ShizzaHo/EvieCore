using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TriggerState
{
    public string triggerName;
    public bool isActive;
}

public class TriggerZone : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Defines whether this trigger is active.")]
    [SerializeField]
    private bool isActive = true;

    [Header("Tag Settings")]
    [Tooltip("List of tags that will trigger this zone.")]
    [SerializeField]
    private List<string> validTags = new List<string>();

    [Header("Trigger Check Settings")]
    [Tooltip("Check trigger states when entering the trigger.")]
    [SerializeField]
    private bool CheckWhenEntering = false;

    [Tooltip("Check trigger states when exiting the trigger.")]
    [SerializeField]
    private bool CheckWhenExiting = false;

    [Header("Actions on Trigger Activation")]
    [Tooltip("Should an action be executed when the trigger is activated?")]
    [SerializeField]
    private bool executeAction = false;

    [Tooltip("Action to execute when entering the trigger.")]
    [SerializeField]
    [ShowIf("executeAction")]
    private UnityEvent executeActionEnter;

    [Tooltip("Action to execute when exiting the trigger.")]
    [SerializeField]
    [ShowIf("executeAction")]
    private UnityEvent executeActionExit;

    [Header("Message Settings")]
    [Tooltip("Should a message be sent when the trigger is activated?")]
    [SerializeField]
    private bool sendMessage = false;

    [Tooltip("Send a message when entering the trigger.")]
    [SerializeField]
    [ShowIf("sendMessage")]
    private string messageEnter;

    [Tooltip("Send a message when exiting the trigger.")]
    [SerializeField]
    [ShowIf("sendMessage")]
    private string messageExit;

    [Header("Required Trigger States")]
    [Tooltip("List of triggers and their expected states.")]
    [SerializeField]
    private List<TriggerState> needTriggersState = new List<TriggerState>();

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive || !IsValidTag(other.tag)) return;

        // Check flag for entering trigger
        if (CheckWhenEntering)
        {
            if (AreAllTriggersMatching())
            {
                ExecuteTriggerLogic(other, true);
            }
        }
        else
        {
            ExecuteTriggerLogic(other, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isActive || !IsValidTag(other.tag))
        {
            Debug.LogWarning($"[EVIECORE/CONTROLLER/TRIGGERZONE/WARNING] TriggerZone is not active or invalid tag. Ignoring OnTriggerExit for {other.name}.");
            return;
        }

        // Check flag for exiting trigger
        if (CheckWhenExiting)
        {
            if (AreAllTriggersMatching())
            {
                ExecuteTriggerLogic(other, false);
            }
        }
        else
        {
            ExecuteTriggerLogic(other, false);
        }
    }

    /// <summary>
    /// Checks if the tag is valid.
    /// </summary>
    /// <param name="tag">The tag to check.</param>
    /// <returns>True if the tag is valid, otherwise false.</returns>
    private bool IsValidTag(string tag)
    {
        if (validTags.Count == 0) return true; // If no tags are specified, allow all tags
        return validTags.Contains(tag);
    }

    /// <summary>
    /// Checks if all triggers in the local list match states in the global TriggerManager.
    /// </summary>
    /// <returns>True if all triggers match, otherwise false.</returns>
    private bool AreAllTriggersMatching()
    {
        foreach (var trigger in needTriggersState)
        {
            if (TriggerManager.Instance == null)
            {
                Debug.LogError($"[EVIECORE/ERROR] TriggerManager not found! Make sure it is added to the scene before using it {gameObject.name}.");
                return false;
            }

            bool globalState = TriggerManager.Instance.GetTriggerState(trigger.triggerName);
            if (globalState != trigger.isActive)
            {
                Debug.LogWarning($"[EVIECORE/CONTROLLER/TRIGGERZONE/WARNING] Trigger '{trigger.triggerName}' does not match: expected {trigger.isActive}, but found {globalState}");
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Executes the trigger logic, including sending messages and performing actions.
    /// </summary>
    /// <param name="other">The object entering or exiting the trigger.</param>
    /// <param name="isEnter">True if the object is entering; False if exiting.</param>
    private void ExecuteTriggerLogic(Collider other, bool isEnter)
    {
        if (sendMessage)
        {
            if (isEnter && !string.IsNullOrEmpty(messageEnter))
            {
                MessageManager.Instance.SendMessage(messageEnter);
            }
            else if (!isEnter && !string.IsNullOrEmpty(messageExit))
            {
                MessageManager.Instance.SendMessage(messageExit);
            }
        }

        if (executeAction)
        {
            if (isEnter)
            {
                executeActionEnter?.Invoke();
            }
            else
            {
                executeActionExit?.Invoke();
            }
        }
    }
}
