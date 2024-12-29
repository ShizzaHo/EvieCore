[[Назад]](./main.md)

# UpdateManager <span style="font-size: 10px">[EvieCore/module]</span>

### Описание 
`UpdateManager` — это компонент для EvieCore, который управляет обновлениями объектов, реализующих интерфейс `EvieCoreUpdate`. Он позволяет регистрировать, удалять и вызывать метод обновления (`OnUpdate`) для всех зарегистрированных объектов. Этот паттерн управления обновлениями позволяет централизованно контролировать процессы, которые должны выполняться каждый кадр, без необходимости добавлять вызовы `Update()` в каждый объект вручную.
### Классы и Интерфейсы 
1. `EvieCoreUpdate` (Интерфейс)Интерфейс `EvieCoreUpdate` требует реализации метода `OnUpdate()`, который будет вызван каждый кадр для объектов, зарегистрированных в `UpdateManager`.**Методы:**  
- `void OnUpdate();` — метод, который должен быть реализован в классе. Он вызывается каждый кадр для объектов, зарегистрированных в `UpdateManager`.

Пример реализации интерфейса:


```csharp
public class ExampleUpdate : MonoBehaviour, EvieCoreUpdate
{
    public void OnUpdate()
    {
        // Логика обновления
        Debug.Log("Updated");
    }
}
```
2. `UpdateManager` — это синглтон, который управляет списком объектов, реализующих интерфейс `EvieCoreUpdate`. Он отвечает за выполнение их метода `OnUpdate()` каждое обновление (кадр) в Unity.

**Свойства:**  
- `Instance`: Статическое свойство, возвращающее экземпляр `UpdateManager`. Оно используется для обеспечения единственного экземпляра класса (синглтон).
**Методы:**  
- `void Awake()`: Инициализация синглтона. Если уже существует экземпляр `UpdateManager`, то текущий объект уничтожается, чтобы не было дублирования.
 
- `void Update()`: Вызывается каждый кадр. Для каждого объекта в `updateStorage` вызывается метод `OnUpdate()`.
 
- `void Register(EvieCoreUpdate updatable)`: Регистрирует объект, реализующий `EvieCoreUpdate`, для вызова его метода `OnUpdate()` каждый кадр.
 
- `void Unregister(EvieCoreUpdate updatable)`: Убирает объект из списка обновляемых объектов.
 
- `void EnableDontDestroyOnLoad()`: Устанавливает объект `UpdateManager` как не уничтожаемый при переходе на другой сцены.
 
- `void ClearStorage()`: Очищает список всех зарегистрированных объектов.
Пример использования `UpdateManager`:

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
        // Логика обновления
        Debug.Log("OnUpdate вызывается каждый кадр.");
    }
}
```

### Пример использования 
 
1. Создайте объект, который будет обновляться каждый кадр, и реализуйте интерфейс `EvieCoreUpdate`.


```csharp
public class ExampleUpdate : MonoBehaviour, EvieCoreUpdate
{
    public void OnUpdate()
    {
        // Логика обновления
        Debug.Log("Updated");
    }
}
```
 
1. Зарегистрируйте объект в `UpdateManager` в методе `Start()`.


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
        // Логика обновления
        Debug.Log("OnUpdate вызывается каждый кадр.");
    }
}
```
 
1. При необходимости вызова метода `OnUpdate()` для всех зарегистрированных объектов, `UpdateManager` будет это делать автоматически каждый кадр.
 
2. Если нужно сохранить объект между сценами, вызовите метод `EnableDontDestroyOnLoad()`.


```csharp
void Start()
{
    UpdateManager.Instance.EnableDontDestroyOnLoad();
}
```

### Заключение 

`UpdateManager` представляет собой эффективный способ централизованного управления обновлениями объектов в Unity, который снижает количество кода и повышает гибкость приложения. Он идеально подходит для ситуаций, когда необходимо обновлять множество объектов каждый кадр, но при этом избежать распространения метода `Update()` по всем классам. Использование этого паттерна позволяет более эффективно управлять жизненным циклом объектов и минимизировать вероятность ошибок, связанных с обновлением объектов.