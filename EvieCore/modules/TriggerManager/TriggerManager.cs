using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    // Синглтон
    public static TriggerManager Instance { get; private set; }

    // Словарь триггеров: имя триггера и его состояние (активен или нет)
    private Dictionary<string, bool> triggers = new Dictionary<string, bool>();

    private void Awake()
    {
        // Реализация синглтона
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Добавить триггер в систему.
    /// </summary>
    /// <param name="triggerName">Имя триггера.</param>
    /// <param name="initialState">Начальное состояние триггера (по умолчанию false).</param>
    public void AddTrigger(string triggerName, bool initialState = false)
    {
        if (!triggers.ContainsKey(triggerName))
        {
            triggers[triggerName] = initialState;
            Debug.Log($"Trigger '{triggerName}' added with initial state: {initialState}");
        }
        else
        {
            Debug.LogWarning($"Trigger '{triggerName}' already exists.");
        }
    }

    /// <summary>
    /// Установить состояние триггера.
    /// </summary>
    /// <param name="triggerName">Имя триггера.</param>
    /// <param name="state">Новое состояние триггера.</param>
    public void SetTriggerState(string triggerName, bool state)
    {
        if (triggers.ContainsKey(triggerName))
        {
            triggers[triggerName] = state;
            Debug.Log($"Trigger '{triggerName}' state set to: {state}");
        }
        else
        {
            Debug.LogError($"Trigger '{triggerName}' does not exist.");
        }
    }

    /// <summary>
    /// Проверить состояние триггера.
    /// </summary>
    /// <param name="triggerName">Имя триггера.</param>
    /// <returns>Состояние триггера (true или false).</returns>
    public bool GetTriggerState(string triggerName)
    {
        if (triggers.ContainsKey(triggerName))
        {
            return triggers[triggerName];
        }
        else
        {
            Debug.LogError($"Trigger '{triggerName}' does not exist.");
            return false;
        }
    }

    /// <summary>
    /// Получить список всех триггеров.
    /// </summary>
    /// <returns>Список имен всех триггеров.</returns>
    public List<string> GetAllTriggers()
    {
        return new List<string>(triggers.Keys);
    }
}
