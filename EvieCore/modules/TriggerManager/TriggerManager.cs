using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log($"Trigger '{triggerName}' added with initial state: {initialState}");
        }
        else
        {
            Debug.LogWarning($"Trigger '{triggerName}' already exists.");
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
            Debug.Log($"Trigger '{triggerName}' state set to: {state}");
        }
        else
        {
            Debug.LogError($"Trigger '{triggerName}' does not exist.");
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
            Debug.LogError($"Trigger '{triggerName}' does not exist.");
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
