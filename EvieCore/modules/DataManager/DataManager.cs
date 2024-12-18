using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private Dictionary<string, object> dataStore = new Dictionary<string, object>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetData<T>(string key, T value)
    {
        if (dataStore.ContainsKey(key))
        {
            dataStore[key] = value;
        }
        else
        {
            dataStore.Add(key, value);
        }
    }

    public T GetData<T>(string key)
    {
        if (dataStore.TryGetValue(key, out var value))
        {
            if (value is T castedValue)
            {
                return castedValue;
            }
            else
            {
                Debug.LogWarning($"DataManager: �������� ��� ����� '{key}' �� ������������� ���� {typeof(T)}.");
            }
        }
        else
        {
            Debug.LogWarning($"DataManager: �������� ��� ����� '{key}' �� �������.");
        }
        return default;
    }

    public bool ContainsKey(string key)
    {
        return dataStore.ContainsKey(key);
    }

    public void RemoveData(string key)
    {
        if (dataStore.ContainsKey(key))
        {
            dataStore.Remove(key);
        }
    }

    public void ClearAllData()
    {
        dataStore.Clear();
    }
}