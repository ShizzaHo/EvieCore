[[Назад]](./main.md)

# ExinAIController <span style="font-size: 10px">[ExinAI]</span>

## Описание

Данный скрипт представляет собой систему управления состояниями искусственного интеллекта (AI) в Unity, используя интерфейсы и абстрактные классы для обеспечения гибкости и расширяемости. Система включает состояния AI, которые могут меняться в зависимости от сообщений и условий, а также использует NavMesh для навигации.

## Классы и Интерфейсы

``IExinAIState`` - интерфейс, который определяет базовые методы для состояний AI:

``ExinAIState`` - Абстрактный класс, реализующий интерфейс IExinAIState. Предоставляет возможность создания конкретных состояний AI, наследуясь от этого класса.

* ``Enter()``: Метод, вызываемый при входе в состояние.

* ``StateUpdate()``: Метод, вызываемый каждый кадр для обновления состояния.

* ``Exit()``: Метод, вызываемый при выходе из состояния.

``AIState`` - Класс, представляющий состояние AI:

* ExinAIState state: Состояние AI.

* string name: Имя состояния.

``AIStateChanger`` - Класс, определяющий автоматическую смену состояний в зависимости от сообщения:

* ``string stateName``: Имя состояния, в которое нужно перейти.

* ``string message``: Сообщение, при получении которого происходит смена состояния.

``ExinAIController`` - Основной класс контроллера AI, реализующий интерфейс ``EvieCoreUpdate``:

* ``NavMeshAgent NavMeshAgent``: Компонент для навигации AI.

* ``NavMeshSurface NavMeshSurface``: Поверхность для навигации.

* ``List<AIState> StatesLibrary``: Список всех доступных состояний AI.

* ``List<AIStateChanger> AutoChanger``: Список автоматических смен состояний.

* ``ExinAIState currentState``: Текущее состояние AI.

Методы класса:

* ``void Start()``: Метод, вызываемый при старте объекта, регистрирует AI в UpdateManager и устанавливает начальное состояние.

* ``void OnDestroy()``: Метод, вызываемый при уничтожении объекта, удаляет AI из UpdateManager.

* ``void ChangeState(string name)``: Метод для смены состояния AI.

* ``void Message(string message)``: Метод для обработки сообщений и автоматической смены состояния.

* ``void OnUpdate()``: Метод для обновления текущего состояния AI.

## Пример использования

```csharp
public class PatrolState : ExinAIState
{
    public override void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public override void StateUpdate()
    {
        // Код для патрулирования
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
        // Код для атаки
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
        if (/* условие для атаки */)
        {
            AIController.ChangeState("Attack");
        }
    }
}
```

## Заключение

Данная система управления состояниями AI в Unity обеспечивает гибкость и удобство в создании сложного поведения искусственного интеллекта. Использование интерфейсов и абстрактных классов позволяет легко расширять функциональность и добавлять новые состояния, а также интегрировать систему с NavMesh для навигации. Это делает систему подходящей для разработки AI в различных игровых и симуляционных проектах.