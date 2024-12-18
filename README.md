
EvieCore is a set of solutions and tools for solving various tasks.
The main task of EvieCore is to simplify the developer's life, optimize and organize the project by shelves.
# Структура EvieCore

![](docs/01.png)

Create a global object EvieCore on the main stage, for example in the menu of your game, hang the script EvieCore.cs on it, and customize it as needed

![](docs/02.png)

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






