[[Back]](../../../README.md)

# EvieCore

## Modules

### UpdateManager

This module implements a manager for updates to objects that use the `EvieCoreUpdate` interface. It is designed to centrally manage component updates that need to be called every frame. 

#### How to use:

```csharp
public class ExampleObject : MonoBehaviour, EvieCoreUpdate
{
    private float speed = 5f; // Variable for movement speed

    // This method will be called every frame via UpdateManager
    public void OnUpdate()
    {
        // For example, move the object forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // You can add additional logic for other updates
    }

    void Start()
    {
        // Register an object in UpdateManager to receive updates
        UpdateManager.Instance.Register(this);
    }

    void OnDestroy()
    {
        // Remove an object from the list of objects to be updated in UpdateManager
        UpdateManager.Instance.Unregister(this);
    }
}
```

Use the `EvieCoreUpdate` interface to create objects that should receive updates every frame. This can be useful for components such as motion control, input processing, animation, AI logic processing, or any other task that requires regular state updates.
#### Benefits of use:

- **Centralized update management**: All objects that need updating every frame can be centrally managed via `UpdateManager`.
- **Ease of adding new components**: When a new component with update logic needs to be added, all you need to do is implement the `EvieCoreUpdate` interface and register it.
- **Simplification of code**: Eliminates the need to manually add update calls in `Update()` methods of multiple objects, reducing redundancy and increasing code readability.
### DataManager

This module implements a centralized data manager that allows you to store and manage data of various types in a dictionary. The `DataManager` provides methods for adding, retrieving, deleting and clearing keyed data.

#### How to use:

```csharp
public class Player : MonoBehaviour
{
    private void Start()
    {
        // Save data
        DataManager.Instance.SetData("PlayerScore", 100);
        
        // Get data
        int score = DataManager.Instance.GetData<int>("PlayerScore");
        Debug.Log($"Player's score: {score}");

        // Check if the data is available
        if (DataManager.Instance.ContainsKey("PlayerScore"))
        {
            Debug.Log("Player score exists.");
        }
        
        // Remove data
        DataManager.Instance.RemoveData("PlayerScore");
    }
}
```

#### DataManager methods:

- **`SetData<T>(string key, T value)`**: Adds or updates data to the `dataStore` by key. If the key already exists, the value is updated.
- **`GetData<T>(string key)`**: Gets data by key and casts it to type `T`. If no data is found or the data type does not match, the default value for type `T` is returned.
- **`ContainsKey(string key)`**: Checks if a record with the specified key exists.
- **`RemoveData(string key)`**: Removes a record with the specified key.
- **`ClearAllData()`**: Clears all data in the `dataStore`.

#### Usage Benefits:

- **Centralized data management**: All data can be centrally stored and retrieved through a single object, making it easy to manage.
- **Flexibility of types**: With generics, data of any type can be stored and retrieved, ensuring type safety at compile time.
- **Data Lifecycle Management**: Easily delete or purge data at any time, keeping stored information up-to-date.
- **Easy to use**: Keys make it easy to retrieve and modify data, making it intuitive to work with.

### MessageManager.

This module implements a message system for communication between objects. The logic of operation resembles the event mechanics in Scratch, making it intuitive for those familiar with that platform. **Important:** Using a message system is not always considered good practice, as it can make it difficult to keep track of relationships between objects.
#### How to use:

```csharp
public class Player : MonoBehaviour
{
    private void Start()
    {
        // Subscribe to message without arguments
        MessageManager.Instance.Subscribe("GameStart", OnGameStart);

        // Subscribe to a message with an argument
        MessageManager.Instance.Subscribe<int>("PlayerScored", OnPlayerScored);

        // Sending a message without arguments
        MessageManager.Instance.SendMessage("GameStart");

        // Send a message with an argument
        MessageManager.Instance.SendMessage("PlayerScored", 10);
    }

    private void OnGameStart()
    {
        Debug.Log("Game has started!");
    }

    private void OnPlayerScored(int points)
    {
        Debug.Log($"Player scored {points} points!");
    }

    private void OnDestroy()
    {
        // Unsubscribe from messages
        MessageManager.Instance.Unsubscribe("GameStart", OnGameStart);
        MessageManager.Instance.Unsubscribe<int>("PlayerScored", OnPlayerScored);
    }
}
```

#### MessageManager methods:

- **`Subscribe(string message, Action listener)`**: Subscribes method to a message with no arguments.
- **`Subscribe<T>(string message, Action<T> listener)`**: Subscribes a method to a message with an argument of type `T`.
- **`Unsubscribe(string message, Action listener)`**: Unsubscribes a method to a message with no arguments.
- **`Unsubscribe<T>(string message, Action<T> listener)`**: Unsubscribes a method from a message with an argument of type `T`.
- **`SendMessage(string message)`**: Sends a message with no arguments to all subscribers.
- **`SendMessage<T>(string message, T arg)`**: Sends a message with an argument to all subscribers.
#### Usage advantages:

- **Subscription Flexibility**: Supports messages with no arguments as well as with arguments of arbitrary type.
- **Centralized event management**: Simplifies communication between objects without requiring them to be directly connected.
- **Intuitiveness**: The operating logic is easy to learn due to its similarity to Scratch.
- **Dynamic Subscriber Management**: Subscriptions can be added and removed at any time.
#### Limitations and Recommendations:

- **Avoid overuse**: A large number of messages can make debugging difficult.
- **Check data types**: Subscriber type errors can lead to warnings and unexpected situations.
- **Control unsubscribes**: Always unsubscribe from messages in methods like `OnDestroy` to avoid calls to remote objects.

### TriggerManager

This module implements a system of triggers for games to control certain states associated with events or actions. For example, triggers can be used to track task completion, object activation, or interaction with the game environment.

#### How to use:

```csharp
public class GameController : MonoBehaviour
{
    private void Start()
    {
        // Adding triggers
        TriggerManager.Instance.AddTrigger("LevelComplete");
        TriggerManager.Instance.AddTrigger("HasKey", false);

        // 
        TriggerManager.Instance.SetTriggerState("HasKey", true);

        // Set the trigger state
        if (TriggerManager.Instance.GetTriggerState("HasKey"))
        {
            Debug.Log("Player has the key.");
        }

        // Get a list of all triggers
        var triggers = TriggerManager.Instance.GetAllTriggers();
        Debug.Log("All triggers: " + string.Join(", ", triggers));
    }
}
```


#### TriggerManager methods:

- **`AddTrigger(string triggerName, bool initialState = false)`**: Adds a new trigger to the system with the specified name and initialState. If the trigger already exists, a warning is output.
- **`SetTriggerState(string triggerName, bool state)`**: Sets the new trigger state. If the trigger does not exist, an error is printed.
- **`GetTriggerState(string triggerName)`**: Returns the current state of the specified trigger. If the trigger does not exist, an error is printed and the return value is `false`.
- **`GetAllTriggers()`**: Returns a list of all added triggers.

#### Usage Benefits:

- **Centralized state management**: All triggers are managed through a single object, making it easy to monitor their current state.
- **Flexibility in use**: Easily add new triggers or change their state at any time.
- **Ease of Integration**: Triggers can be used for a variety of mechanics such as quests, door logic, trap activation, and more.
- **Easy to check states**: The state of any trigger can be quickly checked in any part of the code.

#### Limitations and Recommendations:

- **Control the number of triggers**: Do not add too many triggers to avoid complexity in management.
- **Structure trigger names**: Use clear and unique names for triggers to avoid confusion.
- **Manage errors**: Make sure error handling is implemented correctly, especially if triggers may be used by other developers.

#### Additional Tools for TriggerManager:

* Trigger Manager Window - A window where you can change triggers during the game, this is a tool for debugging the game (open at the following path: Window/EvieCore/Trigger Manager)

* TriggerZone (Prefab and script) - Allows you to create zones that will trigger when an object (e.g. player) is hit and check triggers

## Controllers

### FirstPersonController

### SimpleHUD

### TriggerZone
