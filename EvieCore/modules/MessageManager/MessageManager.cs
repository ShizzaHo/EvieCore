using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    // Use this module with caution! Using messages is not a good practice.

    public static MessageManager Instance { get; private set; }

    // ������� ��� �������� ��������� � �� �����������
    private Dictionary<string, Delegate> messageDictionary = new Dictionary<string, Delegate>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �������� �� ��������� ��� ����������
    public void Subscribe(string message, Action listener)
    {
        if (messageDictionary.ContainsKey(message))
        {
            if (messageDictionary[message] is Action existingAction)
            {
                messageDictionary[message] = Delegate.Combine(existingAction, listener);
            }
            else
            {
                Debug.LogWarning($"Message \"{message}\" has a mismatched subscriber type.");
            }
        }
        else
        {
            messageDictionary[message] = listener;
        }
    }

    // ������� �� ��������� ��� ����������
    public void Unsubscribe(string message, Action listener)
    {
        if (messageDictionary.ContainsKey(message))
        {
            if (messageDictionary[message] is Action existingAction)
            {
                messageDictionary[message] = Delegate.Remove(existingAction, listener);

                if (messageDictionary[message] == null)
                {
                    messageDictionary.Remove(message);
                }
            }
            else
            {
                Debug.LogWarning($"Message \"{message}\" has a mismatched subscriber type.");
            }
        }
    }

    // �������� �� ��������� � ���������� ���� T
    public void Subscribe<T>(string message, Action<T> listener)
    {
        if (messageDictionary.ContainsKey(message))
        {
            if (messageDictionary[message] is Action<T> existingAction)
            {
                messageDictionary[message] = Delegate.Combine(existingAction, listener);
            }
            else
            {
                Debug.LogWarning($"��������� \"{message}\" ����� ������������ ��� ����������.");
            }
        }
        else
        {
            messageDictionary[message] = listener;
        }
    }

    // ������� �� ��������� � ���������� ���� T
    public void Unsubscribe<T>(string message, Action<T> listener)
    {
        if (messageDictionary.ContainsKey(message))
        {
            if (messageDictionary[message] is Action<T> existingAction)
            {
                messageDictionary[message] = Delegate.Remove(existingAction, listener);

                if (messageDictionary[message] == null)
                {
                    messageDictionary.Remove(message);
                }
            }
            else
            {
                Debug.LogWarning($"��������� \"{message}\" ����� ������������ ��� ����������.");
            }
        }
    }

    // �������� ��������� ��� ����������
    public void SendMessage(string message)
    {
        if (messageDictionary.ContainsKey(message))
        {
            if (messageDictionary[message] is Action action)
            {
                action.Invoke();
            }
            else
            {
                Debug.LogWarning($"��������� \"{message}\" ����� ������������ ��� ����������.");
            }
        }
        else
        {
            Debug.LogWarning($"��������� \"{message}\" �� ����� �����������.");
        }
    }

    // �������� ��������� � ���������� ���� T
    public void SendMessage<T>(string message, T arg)
    {
        if (messageDictionary.ContainsKey(message))
        {
            if (messageDictionary[message] is Action<T> action)
            {
                action.Invoke(arg);
            }
            else
            {
                Debug.LogWarning($"��������� \"{message}\" ����� ������������ ��� ����������.");
            }
        }
        else
        {
            Debug.LogWarning($"��������� \"{message}\" �� ����� �����������.");
        }
    }
}
