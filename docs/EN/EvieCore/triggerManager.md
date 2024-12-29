[[Back]](./main.md)

# TriggerManager <span style="font-size: 10px">[EvieCore/module]</span>

## Description 
`TriggerManager` is a class designed for managing the state of triggers in Unity. It allows adding, modifying, and tracking the states of various triggers (boolean variables), which is useful for creating event-driven systems in games or handling interaction logic.
### Key Features: 
 
1. **Adding a Trigger** : Add a new trigger with an initial state.
 
2. **Changing Trigger State** : Modify the state of an existing trigger.
 
3. **Getting Trigger State** : Retrieve the current state of a trigger.
 
4. **Retrieving All Triggers** : Get a list of all added triggers.
Additionally, the class uses the **Singleton**  pattern to ensure there is only one instance of `TriggerManager` throughout the game, making it easier to access globally.
## Classes and Interfaces 
`TriggerManager``TriggerManager` is the primary class that provides the functionality for managing triggers.
#### Properties: 
 
- **Instance (static)** : 
  - Type: `TriggerManager`
 
  - Description: A static property that gives access to the singleton instance of `TriggerManager`. If the instance already exists, it is returned; otherwise, a new instance is created.

#### Methods: 
 
- **`AddTrigger(string triggerName, bool initialState = false)`** :
  - Description: Adds a new trigger with the specified name and initial state. If a trigger with the same name already exists, a warning is logged.
 
  - Parameters: 
    - `triggerName` (string): The name of the trigger.
 
    - `initialState` (bool): The initial state of the trigger (default is `false`).
 
- **`SetTriggerState(string triggerName, bool state)`** :
  - Description: Sets the state of an existing trigger. If the trigger does not exist, an error is logged.
 
  - Parameters: 
    - `triggerName` (string): The name of the trigger.
 
    - `state` (bool): The new state to set for the trigger.
 
- **`GetTriggerState(string triggerName)`** : 
  - Description: Returns the current state of a trigger. If the trigger does not exist, an error is logged, and `false` is returned as the default state.
 
  - Parameters: 
    - `triggerName` (string): The name of the trigger.
 
  - Returns: 
    - Type: `bool`

    - Description: The state of the trigger.
 
- **`GetAllTriggers()`** :
  - Description: Returns a list of all trigger names that have been added to the system.
 
  - Returns: 
    - Type: `List<string>`

    - Description: A list of strings containing the names of all triggers.

## Example Usage 


```csharp
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        // Adding triggers
        TriggerManager.Instance.AddTrigger("PlayerEnterArea", true);
        TriggerManager.Instance.AddTrigger("EnemyDefeated", false);
        
        // Checking the state of a trigger
        bool playerInArea = TriggerManager.Instance.GetTriggerState("PlayerEnterArea");
        Debug.Log("Player in area: " + playerInArea);

        // Changing the state of a trigger
        TriggerManager.Instance.SetTriggerState("PlayerEnterArea", false);
        
        // Getting and displaying all triggers
        var triggers = TriggerManager.Instance.GetAllTriggers();
        foreach (var trigger in triggers)
        {
            Debug.Log("Trigger: " + trigger);
        }
    }
}
```

### Explanation: 
 
1. Two triggers are added at the start: `PlayerEnterArea` with an initial state of `true` and `EnemyDefeated` with an initial state of `false`.
 
2. The state of the `PlayerEnterArea` trigger is retrieved and logged to the console.
 
3. The state of the `PlayerEnterArea` trigger is updated to `false`.

4. All triggers are retrieved and their names are logged.

## Conclusion 
`TriggerManager` is a convenient and effective tool for managing trigger states in Unity. It allows developers to easily add, modify, and track various boolean conditions, making it ideal for event systems, interaction management, and other scenarios that require state tracking. By using the Singleton pattern, the manager ensures that there is only one instance of the trigger system throughout the game, simplifying architecture and preventing the creation of redundant instances of the class.