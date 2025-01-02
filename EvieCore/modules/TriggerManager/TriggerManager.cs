using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

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
        }
        else
        {
            Debug.LogWarning($"[EVIECORE/TRIGGERMANAGER/WARNING] trigger '{triggerName}' already exists.");
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
        }
        else
        {
            Debug.LogWarning($"[EVIECORE/TRIGGERMANAGER/WARNING] trigger '{triggerName}' does not exist.");
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
            Debug.LogError($"[EVIECORE/TRIGGERMANAGER/ERROR] trigger '{triggerName}' does not exist.");
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
