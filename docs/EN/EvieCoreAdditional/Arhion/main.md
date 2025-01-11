[[Back]](../main.md)

# Arhion 

## Namespaces 

### Arhion 

This namespace is responsible for handling localization logic. Key classes include:
 
- **Localization** :
Provides functionality for loading language files, retrieving localized strings, and managing the current language.
Main methods: 
  - `Initialize(string langName)`: Initializes localization by loading the corresponding file for the specified language.
 
  - `SetLanguage(string langName)`: Sets the current language and loads the translation file from the `Resources` folder.
 
  - `GetLocalizedText(string key)`: Returns the localized text for the given key. If the key is missing, it returns the key itself and logs a warning.

### Arhion.TMP 
This namespace contains tools for working with TextMeshPro (TMP), such as text preprocessors.
Key classes and interfaces: 
- **Ipreprocess** : An interface defining the `Format(string text)` method for text processing.
 
- **Preprocessor** : A text preprocessor class implementing the `ITextPreprocessor` interface from TMP. It processes text using all available implementations of the `Ipreprocess` interface.
Main features: 
  - `PreprocessText(string text)`: Processes the text through registered preprocessors.
 
  - Automatically detects and uses all implementations of `Ipreprocess` in the current assembly.
 
- **tmp_localizator** : An implementation of `Ipreprocess` that replaces localization keys in text formatted as `{key}` with their localized values.

#### Examples 

##### Localizing a String 


```csharp
using UnityEngine;
using Arhion;

public class LocalizationDemo : MonoBehaviour
{
    void Start()
    {
        // Initialize localization with the specified language
        Localization.Initialize("ru");

        // Retrieve localized text
        string welcomeMessage = Localization.GetLocalizedText("welcome_message");
        Debug.Log(welcomeMessage);
    }
}
```

##### Using the TMP Preprocessor for Localization 


```csharp
using UnityEngine;
using TMPro;
using Arhion.TMP;

public class TMPDemo : MonoBehaviour
{
    [SerializeField] private TMP_Text textElement;

    void Start()
    {
        // Initialize localization
        Localization.Initialize("en");

        // Set the preprocessor for TextMeshPro
        var preprocessor = new Preprocessor();
        textElement.textPreprocessor = preprocessor;

        // Set text with localization keys
        textElement.text = "Hello, {welcome_message}!";
    }
}
```

### Additional Information 
 
- Localization supports loading JSON files from the `Resources/EvieCore/Arhion/Langs/` folder.
Example JSON structure:


```json
{
    "entries": [
        { "code": "welcome_message", "text": "Welcome!" },
        { "code": "goodbye_message", "text": "Goodbye!" }
    ]
}
```
 
- If a key is missing in the current language, the `GetLocalizedText` method will return the key itself and log a warning to the Unity console.

### Example Localization File 
A localization file is a JSON structure that stores key-value pairs for texts in a specific language. Files are placed in the `Resources/EvieCore/Arhion/Langs/` folder.
Example `en.json` file:

```json
{
    "entries": [
        { "code": "welcome_message", "text": "Welcome!" },
        { "code": "goodbye_message", "text": "Goodbye!" },
        { "code": "error_not_found", "text": "Error: Not found." }
    ]
}
```
Example `ru.json` file:

```json
{
    "entries": [
        { "code": "welcome_message", "text": "Добро пожаловать!" },
        { "code": "goodbye_message", "text": "До свидания!" },
        { "code": "error_not_found", "text": "Ошибка: не найдено." }
    ]
}
```

### Processing Localized Strings with a Preprocessor 
The `tmp_localizator` preprocessor processes text formatted as `{key}` in TMP strings, replacing the key with localized text. This is convenient for avoiding manual text assignment for every UI element.
#### Example of Text Processing 

String in a TMP object:


```plaintext
"Hello, {welcome_message}!"
```

After preprocessor processing:


```plaintext
"Hello, Welcome!" // For language "en"
"Hello, Добро пожаловать!" // For language "ru"
```

### Extending Functionality 
You can create custom implementations of the `Ipreprocess` interface to add unique text processing, such as variable replacement or string formatting.
#### Example of a Custom Preprocessor 


```csharp
using Arhion.TMP;

public class VariableReplacer : Ipreprocess
{
    public string Format(string text)
    {
        return text.Replace("{username}", "Player123");
    }
}
```
Add this preprocessor to your assembly, and it will automatically be detected and used by `Preprocessor`.
### FAQs 

#### How to Add a New Language to the Localization System? 
 
1. Create a JSON file with translations (e.g., `fr.json` for French).
 
2. Save the file in the `Resources/EvieCore/Arhion/Langs/` folder.
 
3. Call `Localization.Initialize("fr")` or `Localization.SetLanguage("fr")` to switch to that language.

#### What Happens If a Key Is Missing in the Current Language? 

If a key is not found, the system logs a warning to the console and returns the key itself. This makes it easy to identify missing translations.

#### How to Debug Localization? 
 
1. Ensure JSON files are placed in the correct folder: `Resources/EvieCore/Arhion/Langs/`.

2. Verify that the keys in the file match those used in the code or TMP texts.

3. Enable logging and monitor warnings in the Unity console.

#### Can Localization Be Used Without TMP? 
Yes, localization can be used in any text element not related to TMP. Simply call `Localization.GetLocalizedText("key")` to retrieve localized text.