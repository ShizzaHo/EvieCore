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
    private UnityEvent executeActionEnter;

    [Tooltip("Action to execute when exiting the trigger.")]
    [SerializeField]
    private UnityEvent executeActionExit;

    [Header("Message Settings")]
    [Tooltip("Should a message be sent when the trigger is activated?")]
    [SerializeField]
    private bool sendMessage = false;

    [Tooltip("Send a message when entering the trigger.")]
    [SerializeField]
    private string messageEnter;

    [Tooltip("Send a message when exiting the trigger.")]
    [SerializeField]
    private string messageExit;

    [Header("Required Trigger States")]
    [Tooltip("List of triggers and their expected states.")]
    [SerializeField]
    private List<TriggerState> needTriggersState = new List<TriggerState>();

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        // Проверяем флаг проверки на входе
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
        if (!isActive)
        {
            Debug.LogWarning($"TriggerZone is not active. Ignoring OnTriggerExit for {other.name}.");
            return;
        }

        // Проверяем флаг проверки на выходе
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
    /// Проверяет, соответствуют ли все триггеры в локальном списке состояниям в глобальном TriggerManager.
    /// </summary>
    /// <returns>True, если все триггеры совпадают, иначе False.</returns>
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
                Debug.LogWarning($"Trigger '{trigger.triggerName}' does not match: expected {trigger.isActive}, but found {globalState}");
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Выполняет логику триггера, включая отправку сообщений и выполнение действий.
    /// </summary>
    /// <param name="other">Объект, вошедший или вышедший из триггера.</param>
    /// <param name="isEnter">True, если объект вошел; False, если вышел.</param>
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
