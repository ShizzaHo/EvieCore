[[Back]](./main.md)

# MessageManager <span style="font-size: 10px">[EvieCore/module]</span>

## Description 
**MessageManager**  is a class for managing a messaging and subscriber system in Unity. It allows subscribing and unsubscribing to messages, as well as sending them with various types of data (or without any). The messaging system is implemented using a dictionary where each message corresponds to a delegate that holds the list of subscribers.
Instead of direct calls between objects, messages provide a more flexible architecture, allowing objects to interact with each other without direct references. This helps reduce the coupling between components and facilitates testing.

### Key Features: 

- Messages are identified by a string key.

- Subscribers can be of different types (with or without parameters).
 
- Message handling is done using delegates (`Action` and `Action<T>`).

- Subscriptions and unsubscriptions can be made dynamically at any time.
 
- **Warning** : Use with caution, as overusing messaging can lead to tight coupling and make debugging difficult in large systems.

## Classes and Interfaces 

### Classes: 
 
1. **MessageManager** 
This is the core class managing subscriptions and message sending. It implements the Singleton pattern, allowing access to it through the global instance `MessageManager.Instance`.

### Methods: 
 
1. **`Awake`** 
Initializes the singleton instance of the class and assigns it to the global variable `Instance`. If an instance already exists, the current object is destroyed.
 
2. **`Subscribe(string message, Action listener)`** 
Subscribes to a message with no parameters. It adds the provided delegate (listener) to the message's subscriber list.
 
3. **`Unsubscribe(string message, Action listener)`** 
Unsubscribes from a message with no parameters. It removes the provided delegate (listener) from the subscriber list.
 
4. **`Subscribe<T>(string message, Action<T> listener)`** 
Subscribes to a message with a parameter of type `T`. It adds the provided delegate (listener) for messages with a parameter of type `T`.
 
5. **`Unsubscribe<T>(string message, Action<T> listener)`** 
Unsubscribes from a message with a parameter of type `T`. It removes the provided delegate (listener) from the subscriber list for messages with a parameter of type `T`.
 
6. **`SendMessage(string message)`** 
Sends a message with no parameters. All subscribers who are subscribed to this message will be invoked.
 
7. **`SendMessage<T>(string message, T arg)`** 
Sends a message with a parameter of type `T`. All subscribers who are subscribed to this message with the corresponding type will be invoked with the provided argument.

### Delegates: 
 
- **Action**  — Delegate for messages without parameters.
 
- **Action\<T>**  — Delegate for messages with parameters of type `T`.

## Example Usage 


```csharp
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        // Subscribing to messages
        MessageManager.Instance.Subscribe("OnGameStart", OnGameStart);
        MessageManager.Instance.Subscribe<int>("OnScoreChanged", OnScoreChanged);

        // Sending messages
        MessageManager.Instance.SendMessage("OnGameStart");
        MessageManager.Instance.SendMessage("OnScoreChanged", 100);
    }

    private void OnGameStart()
    {
        Debug.Log("Game has started!");
    }

    private void OnScoreChanged(int newScore)
    {
        Debug.Log($"New score: {newScore}");
    }

    private void OnDestroy()
    {
        // Unsubscribing from messages when the object is destroyed
        MessageManager.Instance.Unsubscribe("OnGameStart", OnGameStart);
        MessageManager.Instance.Unsubscribe<int>("OnScoreChanged", OnScoreChanged);
    }
}
```

### Explanation of Example: 
 
1. In the `Start` method, we subscribe to two messages: `"OnGameStart"` (no parameters) and `"OnScoreChanged"` (with a parameter of type `int`).
 
2. The messages are sent using the `SendMessage` methods: 
  - `"OnGameStart"` is sent without any parameters.
 
  - `"OnScoreChanged"` is sent with an integer parameter, which is logged to the console.
 
3. In the `OnDestroy` method, we unsubscribe from these messages to prevent memory leaks or unintended behavior when the object is destroyed.

## Conclusion 
**MessageManager**  provides a convenient mechanism for creating an event-driven system in Unity, allowing components to communicate with minimal dependencies. However, it’s important to be cautious about overusing this system, as excessive use of messages can lead to tighter coupling and make the code harder to debug in complex projects.
This system is best used for scenarios where components need to communicate with minimal direct references (e.g., for UI updates, event broadcasting, etc.), but it should be avoided in frequent interactions between core game objects, as it can reduce code readability and testability. Use it where flexibility is more important than the direct coupling between components.