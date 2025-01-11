[[Back]](./main.md)

# ExinAIController <span style="font-size: 10px">[ExinAI]</span>

## Description

This script implements a state management system for Artificial Intelligence (AI) in Unity, using interfaces and abstract classes to ensure flexibility and extensibility. The system includes AI states that can change based on messages and conditions, and it utilizes NavMesh for navigation.

## Classes and Interfaces

``IExinAIState`` - Interface defining the basic methods for AI states:

``ExinAIState`` - Abstract class implementing the ``IExinAIState`` interface. Provides a base for creating specific AI states by inheritance.

* ``Enter()``: Method called when entering a state.

* ``StateUpdate()``: Method called every frame to update the state.

* ``Exit()``: Method called when exiting a state.

``AIState`` - Class representing an AI state:

* ExinAIState state: The AI state.

* string name: The name of the state.

``AIStateChanger`` - Class defining automatic state transitions based on messages:

* ``string stateName``: The name of the state to transition to.

* ``string message``: The message that triggers the state transition.

``ExinAIController`` - Main AI controller class, implementing the ``EvieCoreUpdate`` interface:

* ``NavMeshAgent NavMeshAgent``: Component for AI navigation.

* ``NavMeshSurface NavMeshSurface``: Surface for navigation.

* ``List<AIState> StatesLibrary``: List of all available AI states.

* ``List<AIStateChanger> AutoChanger``: List of automatic state transitions.

* ``ExinAIState currentState``: The current AI state.

Class methods:

* ``void Start()``: Method called when the object starts, registers the AI in the UpdateManager and sets the initial state.

* ``void OnDestroy()``: Method called when the object is destroyed, removes the AI from the UpdateManager.

* ``void ChangeState(string name)``: Method for changing the AI state.

* ``void Message(string message)``: Method for processing messages and triggering automatic state transitions.

* ``void OnUpdate()``: Method for updating the current AI state.

## Usage Example

```csharp
public class PatrolState : ExinAIState
{
    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public override void StateUpdate()
    {
        // Patrol logic
    }

    public override void Exit()
    {
        Debug.Log("Exiting Patrol State");
    }
}

public class AttackState : ExinAIState
{
    public override void Enter()
    {
        Debug.Log("Entering Attack State");
    }

    public override void StateUpdate()
    {
        // Attack logic
    }

    public override void Exit()
    {
        Debug.Log("Exiting Attack State");
    }
}

public class ExampleUsage : MonoBehaviour
{
    public ExinAIController AIController;

    private void Start()
    {
        AIController.ChangeState("Patrol");
    }

    private void Update()
    {
        if (/* condition for attack */)
        {
            AIController.ChangeState("Attack");
        }
    }
}
```

## Conclusion

This AI state management system in Unity provides flexibility and convenience for creating complex AI behaviors. Using interfaces and abstract classes allows for easy functionality extension and the addition of new states, while integrating the system with NavMesh ensures navigation capabilities. This makes the system suitable for developing AI in various gaming and simulation projects.