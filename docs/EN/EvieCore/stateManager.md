[[Назад]](./main.md)

# StateManager <span style="font-size: 10px">[EvieCore/module]</span>

## Description

The `StateManager` class provides a centralized way to manage states in a game or application. It implements the Singleton design pattern, ensuring a single global instance is accessible throughout the project. This class allows you to define a list of possible states, set the current state, and check whether the current state matches a specific value.

## Classes and Interfaces 
Class `StateManager`** `StateManager` is the core class responsible for state management.

#### Properties: 
 
- **`Instance`**  *(static)*: A static reference to the single instance of the `StateManager`, allowing global access.
 
- **`states`**  ***`states`**  (private List<string>)*: A list of possible states. Configurable via the Unity Inspector.
 
- **`currentState`**  *(private string)*: The currently active state.

#### Methods: 
 
- **`Awake()`** : Implements the Singleton pattern, preventing multiple instances of the class.
 
- **`Start()`** : Initializes the first state from the `states` list as the current state.
 
- **`SetState(string newState)`** : Sets the specified state as the current state if it exists in the `states` list.
  - Logs a warning if the state does not exist.
 
- **`GetCurrentState()`**  *(string)*: Returns the current state.
 
- **`IsCurrentState(string state)`**  *(bool)*: Checks if the specified state matches the current state.
 
- **`AddState(string newState)`** : Adds a new state to the list if it does not already exist.
  - Logs a warning if the state already exists.

#### Logging: 
All warnings are logged to the console with the prefix `[EVIECORE/STATEMANAGER/WARNING]` for easy identification.

---


#### Example Usage 


```csharp
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        // Access the StateManager instance
        StateManager stateManager = StateManager.Instance;

        // Add new states
        stateManager.AddState("Combat");
        stateManager.AddState("Exploration");

        // Set the initial state
        stateManager.SetState("Exploration");

        // Check the current state
        if (stateManager.IsCurrentState("Exploration"))
        {
            Debug.Log("The player is in exploration mode.");
        }

        // Change the state
        stateManager.SetState("Combat");

        // Retrieve the current state
        string currentState = stateManager.GetCurrentState();
        Debug.Log($"Current state: {currentState}");

        // Attempt to set an unknown state
        stateManager.SetState("Stealth"); // Logs a warning
    }
}
```


---


## Conclusion 
The `StateManager` class is a simple and flexible tool for managing states in Unity projects. It is particularly useful for systems that require tracking the current state, such as gameplay modes (combat, exploration, menu) or user interface behavior. The Singleton pattern ensures easy access to the class from anywhere in the codebase, reducing the need for complex object dependencies.