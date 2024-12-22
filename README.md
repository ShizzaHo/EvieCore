
EvieCore is a set of solutions and tools for solving various tasks.
The main task of EvieCore is to simplify the developer's life, optimize and organize the project by shelves.
# Структура EvieCore

![[01.png]]

Create a global object EvieCore on the main stage, for example in the menu of your game, hang the script EvieCore.cs on it, and customize it as needed

![[02.png]]

The DontDestroyThisObject parameter will make EvieCore available throughout the project, when changing scenes the object will not be deleted and will always be available to you

As needed, add child objects to EvieCore that will work as modules, currently there are the following modules:

### UpdateManager

This module implements a manager for updates to objects that use the `EvieCoreUpdate` interface. It is designed to centrally manage component updates that need to be called every frame. 
#### How to use:

```csharp
public class ExampleObject : MonoBehaviour, EvieCoreUpdate
{
    private float speed = 5f; // Variable for travel speed

    // This method will be called every frame via UpdateManager
    public void OnUpdate()
    {
        // For example, moving an object forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // You can add additional logic for other updates
    }

    void Start()
    {
        // Register the object in UpdateManager to receive updates
        UpdateManager.Instance.Register(this);
    }

    void OnDestroy()
    {
        // Remove an object from the list of objects to be updated in UpdateManager
        UpdateManager.Instance.Unregister(this);
    }
}
```

Use the `EvieCoreUpdate` interface to create objects that should receive updates every frame. This can be useful for components such as motion control, input processing, animation, AI logic processing, or any other tasks that require regular state updates.
#### Benefits of Using:

- **Centralized update management**: All objects that need to be updated every frame can be centrally managed via `UpdateManager`.
- **Ease of adding new components**: When a new component with update logic needs to be added, all you need to do is implement the `EvieCoreUpdate` interface and register it.
- **Simplification of code**: Eliminates the need to manually add update calls to the `Update()` methods of multiple objects, reducing redundancy and increasing code readability.
### ### DataManager

This module implements a centralized data manager that allows you to store and manage data of various types in a dictionary. The `DataManager` provides methods for adding, retrieving, deleting and clearing key data.

#### How to use:

```csharp
public class Player : MonoBehaviour
{
    private void Start()
    {
        // Save data
        DataManager.Instance.SetData(“PlayerScore”, 100);
        
        // Get data
        int score = DataManager.Instance.GetData<int>(“PlayerScore”);
        Debug.Log($“Player's score: {score}”);

        // Check if the data is available
        if (DataManager.Instance.ContainsKey(“PlayerScore”))
        {
            Debug.Log(“Player score exists.”);
        }
        
        // Remove data
        DataManager.Instance.RemoveData(“PlayerScore”);
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

## Message Manager

This module implements a message system for interaction between objects. The logic resembles the mechanics of events in the work of scratch, which makes it intuitive for those who are familiar with this platform. **Important:** Using a messaging system is not always considered a good practice, as it can make it more difficult to track relationships between objects.
#### How to use:

```csharp
public class Player : MonoBehaviour
{
    private void Start()
    {
        // Subscribe to the message without arguments
        MessageManager.Instance.Subscribe(“GameStart”, OnGameStart);

        // Subscribe to a message with an argument
        MessageManager.Instance.Subscribe<int>(“PlayerScored”, OnPlayerScored);

        // Sending a message without arguments
        MessageManager.Instance.SendMessage(“GameStart”);

        // Sending a message with an argument
        MessageManager.Instance.SendMessage(“PlayerScored”, 10);
    }

    private void OnGameStart()
    {
        Debug.Log(“Game has started!”);
    }

    private void OnPlayerScored(int points)
    {
        Debug.Log($“Player scored {points} points!”);
    }

    private void OnDestroy()
    {
        // Unsubscribe from messages
        MessageManager.Instance.Unsubscribe(“GameStart”, OnGameStart);
        MessageManager.Instance.Unsubscribe<int>(“PlayerScored”, OnPlayerScored);
    }
}
```

#### Message management methods:

- **`Subscribe(message string, action listener)`**: Sends a method to receive messages.
- **`Subscribe<T>(message string, action<T> listener)`**: Subscribe by adding method with type argument `T`.
- **`Unsubscribe(message string, action listener)`**: Unsubscribes from any messages using this method.
- **`Unsubscribe<T>(message string, action<T> listener)`**: The method unsubscribes from the message with an argument of type "T`.
- **`SendMessage(string message)`**: Provides the ability to send messages without any prompting.
- **`SendMessage<T>(string message, T argument)`**: Cancels sending the message to all users.
#### Advantages of using:

- **Subscription flexibility**: Supports messages both without arguments and with arguments of any type.
- **Centralized event management**: Simplifies interaction between objects without requiring their direct connection.
- **Intuitive**: The operating technology is easily mastered by users from scratch.
- **Dynamic subscriber management**: Subscriptions can be added and deleted at any time.
#### Restrictions and recommendations:

- **Avoid overuse**: A large number of messages can make debugging difficult.
- **Check the data types**: Subscriber type errors can lead to warnings and unexpected situations.
- **Monitor the responses**: Unsubscribe from anything in the onDestroy method all the time to avoid new tasks.

### Trigger Manager

This module implements a trigger system for games that allows you to control certain states related to events or actions. For example, triggers can be used to track the completion of tasks, activate objects, or interact with the game environment.

#### How to use:

```csharp
public class GameController : MonoBehaviour
{
    private void Start()
    {
        // Add triggers
        TriggerManager.Instance.AddTrigger(“LevelComplete”);
        TriggerManager.Instance.AddTrigger(“HasKey”, false);

        // Set the trigger state
        TriggerManager.Instance.SetTriggerState(“HasKey”, true);

        // Check the trigger state
        if (TriggerManager.Instance.GetTriggerState(“HasKey”))
        {
            Debug.Log(“Player has the key.”);
        }

        // Get a list of all triggers
        var triggers = TriggerManager.Instance.GetAllTriggers();
        Debug.Log("All triggers: " + string.Join(", ", triggers));
    }
}
```

#### Trigger management Methods:

- **`AddTrigger(string triggerName, bool initialState = false)`**: Adds a new trigger to the system with the specified name and initial state. If the trigger already exists, a warning is displayed.
- **`SetTriggerState(string trigger name, logical state)`**: Sets a new trigger state. If the trigger does not exist, an error is displayed.
- **`GetTriggerState(string triggerName)`**: Returns the current state of the user trigger. If the trigger does not understand, it is eliminated from the game, then the resulting value is `false'.
- **`GetAllTriggers()`**: Calls all additional triggers.

#### Advantages of using:

- **Centralized State Management**: All triggers are managed through a single object, which makes it easier to monitor their current state.
- **Flexible to use**: Easily add new triggers or change their state at any time.
- **Easy Integration**: Triggers can be used for various mechanics such as quests, door logic, trap activation, and more.
- **Easy to check states**: The status of any trigger can be quickly checked in any part of the code.

#### Restrictions and recommendations:

- **Control the number of triggers**: Do not add too many triggers to avoid difficulty in managing.
- **Structure trigger names**: Use clear and unique names for triggers to avoid confusion.
- **Error Management**: Make sure that error handling is implemented correctly, especially if triggers can be used by other developers.

#### Additional settings for TriggerManager:

* Trigger Manager window - only if you can use triggers during the game to configure for other players (you can open it as follows: Window/EvieCore/Trigger Manager)
* TriggerZone (Team and script) - Allows you to create a zone where people will work on a project (for example, a game) and check for triggers

# Controllers
### FirstPersonController

### SimpleHUD

### Trigger zone