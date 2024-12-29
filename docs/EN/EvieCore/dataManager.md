[[Back]](./main.md)

# DataManager <span style="font-size: 10px">[EvieCore/module]</span>

## Description 
`DataManager` is a class for managing and storing data in Unity. It uses a dictionary (`Dictionary<string, object>`) to store "key-value" pairs, where the key is a string and the value can be any object type. The class implements the **Singleton**  pattern, meaning there is only one instance of this class in the game, accessible via the static property `Instance`.
### Key Features: 

- Storing and retrieving data using keys.

- Checking for the existence of data by key.

- Removing data.

- Clearing all stored data.

- Handling data types with type checking and custom error logging when types don't match.

## Classes and Interfaces 
1. **DataManager** 
This is the main class responsible for managing data in the game. It provides the following methods:

#### Properties: 
 
- `public static DataManager Instance { get; private set; }` — A static property that returns the single instance of the `DataManager`. The Singleton pattern ensures that only one instance exists, and it is globally accessible.

#### Methods: 
 
- `void Awake()` — Initializes the `DataManager` instance and ensures that only one object of this class is created.
 
- `public void SetData<T>(string key, T value)` — Saves data with the specified key (string) and value (of any type `T`). If the key already exists, the value will be updated; otherwise, a new key-value pair will be added.
 
- `public T GetData<T>(string key)` — Retrieves the value associated with the specified key. It returns the value of type `T` if found, or the default value if the key does not exist.
 
- `public bool ContainsKey(string key)` — Checks if the specified key exists in the data store.
 
- `public void RemoveData(string key)` — Removes the data associated with the specified key.
 
- `public void ClearAllData()` — Clears all stored data.

#### Internal Fields: 
 
- `private Dictionary<string, object> dataStore = new Dictionary<string, object>();` — A dictionary used to store data, where the key is a string and the value is an object of any type.


---


## Example Usage 

### Storing Data: 


```csharp
// Storing an int value with the key "playerScore"
DataManager.Instance.SetData("playerScore", 100);

// Storing a string value with the key "playerName"
DataManager.Instance.SetData("playerName", "Alice");
```

### Retrieving Data: 


```csharp
// Retrieving an int value by the key "playerScore"
int score = DataManager.Instance.GetData<int>("playerScore");

// Retrieving a string value by the key "playerName"
string playerName = DataManager.Instance.GetData<string>("playerName");
```

### Checking for Data: 


```csharp
// Checking if the key "playerScore" exists in the data store
bool hasScore = DataManager.Instance.ContainsKey("playerScore");

// Checking if the key "playerName" exists in the data store
bool hasPlayerName = DataManager.Instance.ContainsKey("playerName");
```

### Removing Data: 


```csharp
// Removing the data associated with the key "playerScore"
DataManager.Instance.RemoveData("playerScore");
```

### Clearing All Data: 


```csharp
// Clearing all stored data
DataManager.Instance.ClearAllData();
```

### Example with Incorrect Type: 


```csharp
// Example of incorrect usage — trying to retrieve an int, but the data was stored as a string
string incorrect = DataManager.Instance.GetData<int>("playerName");
```

This call will output a warning in the console that the data type does not match the expected type.


---


## Conclusion 
The `DataManager` class provides a simple and efficient way to manage data in Unity games. It allows for the storage and retrieval of data using keys, and ensures type safety with type-checking features. The Singleton pattern ensures that there is a single instance of this manager that can be accessed globally throughout the game.

### Limitations: 
 
- **Use of `object` type** : May lead to runtime errors if there is a type mismatch when retrieving data.
 
- **Potential memory overhead** : The use of the `object` type may cause unnecessary boxing for value types, especially for large datasets.

This class is ideal for small to medium-sized projects where simple, flexible data management is needed without the complexity of a full-fledged database or custom data structures. It can be used to store game settings, player data, level states, and other temporary game information.