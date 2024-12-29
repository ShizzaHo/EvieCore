[[Back]](./main.md) 

# UpdateManager <span style="font-size: 10px">[EvieCore/module]</span>
### Description 
`UpdateManager` is a component for EvieCore that manages the updates of objects implementing the `EvieCoreUpdate` interface. It allows for registering, unregistering, and invoking the `OnUpdate` method for all registered objects. This update management pattern enables centralized control of processes that need to be executed every frame, without the need to add `Update()` calls in each object manually.
### Classes and Interfaces 
 
1. **`EvieCoreUpdate`**  (Interface)
The `EvieCoreUpdate` interface requires the implementation of the `OnUpdate()` method, which will be called every frame for objects registered with the `UpdateManager`.
**Methods:**
 
- `void OnUpdate();` — This method must be implemented in the class. It is called every frame for objects registered in `UpdateManager`.

Example of interface implementation:


```csharp
public class ExampleUpdate : MonoBehaviour, EvieCoreUpdate
{
    public void OnUpdate()
    {
        // Update logic
        Debug.Log("Updated");
    }
}
```
 
1. **`UpdateManager`**  — This is a singleton that manages the list of objects implementing the `EvieCoreUpdate` interface. It is responsible for invoking their `OnUpdate()` method each frame in Unity.
**Properties:**  
- `Instance`: A static property that returns the `UpdateManager` instance. It ensures a single instance of the class (singleton).
**Methods:**  
- `void Awake()`: Initializes the singleton. If an `UpdateManager` instance already exists, the current object is destroyed to avoid duplication.
 
- `void Update()`: Called every frame. For each object in `updateStorage`, the `OnUpdate()` method is called.
 
- `void Register(EvieCoreUpdate updatable)`: Registers an object implementing `EvieCoreUpdate` to invoke its `OnUpdate()` method every frame.
 
- `void Unregister(EvieCoreUpdate updatable)`: Removes an object from the list of updatable objects.
 
- `void EnableDontDestroyOnLoad()`: Marks the `UpdateManager` object as not to be destroyed when loading a new scene.
 
- `void ClearStorage()`: Clears the list of all registered objects.
Example of using `UpdateManager`:

```csharp
public class Example : MonoBehaviour, EvieCoreUpdate
{
    void Start()
    {
        UpdateManager.Instance.Register(this);
    }

    void OnDestroy()
    {
        UpdateManager.Instance.Unregister(this);
    }

    public void OnUpdate()
    {
        // Update logic
        Debug.Log("OnUpdate is called every frame.");
    }
}
```

### Example Usage 
 
1. Create an object that needs to be updated every frame and implement the `EvieCoreUpdate` interface.


```csharp
public class ExampleUpdate : MonoBehaviour, EvieCoreUpdate
{
    public void OnUpdate()
    {
        // Update logic
        Debug.Log("Updated");
    }
}
```
 
1. Register the object with the `UpdateManager` in the `Start()` method.


```csharp
public class Example : MonoBehaviour
{
    void Start()
    {
        UpdateManager.Instance.Register(this);
    }

    void OnDestroy()
    {
        UpdateManager.Instance.Unregister(this);
    }

    public void OnUpdate()
    {
        // Update logic
        Debug.Log("OnUpdate is called every frame.");
    }
}
```
 
1. The `UpdateManager` will automatically call the `OnUpdate()` method for all registered objects every frame.
 
2. If you need to persist the object across scenes, call the `EnableDontDestroyOnLoad()` method.


```csharp
void Start()
{
    UpdateManager.Instance.EnableDontDestroyOnLoad();
}
```

### Conclusion 
`UpdateManager` provides an efficient way to centrally manage object updates in Unity, reducing code complexity and increasing flexibility. It is ideal for situations where many objects need to be updated every frame, but you want to avoid spreading the `Update()` method across all classes. Using this pattern allows for better management of object lifecycles and minimizes the risk of errors related to object updates.