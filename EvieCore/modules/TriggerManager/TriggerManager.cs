using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class TriggerManager : MonoBehaviour
{
    // ��������
    public static TriggerManager Instance { get; private set; }

    // ������� ���������: ��� �������� � ��� ��������� (������� ��� ���)
    private Dictionary<string, bool> triggers = new Dictionary<string, bool>();

    private void Awake()
    {
        // ���������� ���������
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// �������� ������� � �������.
    /// </summary>
    /// <param name="triggerName">��� ��������.</param>
    /// <param name="initialState">��������� ��������� �������� (�� ��������� false).</param>
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
    /// ���������� ��������� ��������.
    /// </summary>
    /// <param name="triggerName">��� ��������.</param>
    /// <param name="state">����� ��������� ��������.</param>
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
    /// ��������� ��������� ��������.
    /// </summary>
    /// <param name="triggerName">��� ��������.</param>
    /// <returns>��������� �������� (true ��� false).</returns>
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
    /// �������� ������ ���� ���������.
    /// </summary>
    /// <returns>������ ���� ���� ���������.</returns>
    public List<string> GetAllTriggers()
    {
        return new List<string>(triggers.Keys);
    }
}
