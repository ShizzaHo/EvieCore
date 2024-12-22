using System;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    // Use this module with caution! Using messages is not a good practice.

    public static MessageManager Instance { get; private set; }

    // Словарь для хранения сообщений и их подписчиков
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

    // Подписка на сообщение без аргументов
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

    // Отписка от сообщения без аргументов
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

    // Подписка на сообщение с аргументом типа T
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
                Debug.LogWarning($"Сообщение \"{message}\" имеет неподходящий тип подписчика.");
            }
        }
        else
        {
            messageDictionary[message] = listener;
        }
    }

    // Отписка от сообщения с аргументом типа T
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
                Debug.LogWarning($"Сообщение \"{message}\" имеет неподходящий тип подписчика.");
            }
        }
    }

    // Отправка сообщения без аргументов
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
                Debug.LogWarning($"Сообщение \"{message}\" имеет неподходящий тип подписчика.");
            }
        }
        else
        {
            Debug.LogWarning($"Сообщение \"{message}\" не имеет подписчиков.");
        }
    }

    // Отправка сообщения с аргументом типа T
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
                Debug.LogWarning($"Сообщение \"{message}\" имеет неподходящий тип подписчика.");
            }
        }
        else
        {
            Debug.LogWarning($"Сообщение \"{message}\" не имеет подписчиков.");
        }
    }
}
