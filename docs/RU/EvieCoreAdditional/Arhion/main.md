[[Назад]](../main.md)

# Arhion 

## Пространства имен 

### Arhion 

Это пространство имен отвечает за логику работы с локализацией. Основные классы:
 
- **Localization** :
Обеспечивает функциональность для загрузки языковых файлов, получения локализованных строк и управления текущим языком.
Основные методы: 
  - `Initialize(string langName)`: Инициализирует локализацию, загружая соответствующий файл для указанного языка.
 
  - `SetLanguage(string langName)`: Устанавливает текущий язык и загружает файл переводов из папки Resources.
 
  - `GetLocalizedText(string key)`: Возвращает локализованный текст по указанному ключу. Если ключ отсутствует, возвращает сам ключ и выводит предупреждение.

### Arhion.TMP 
Это пространство имен содержит инструменты для работы с TextMeshPro (TMP), такие как препроцессоры текста.
Основные классы и интерфейсы: 
- **Ipreprocess** : Интерфейс, определяющий метод `Format(string text)` для обработки текста.
 
- **Preprocessor** : Класс-препроцессор, реализующий интерфейс `ITextPreprocessor` из TMP. Он обрабатывает текст, используя все доступные реализации интерфейса `Ipreprocess`.
Основные функции: 
  - `PreprocessText(string text)`: Обрабатывает текст через зарегистрированные препроцессоры.
 
  - Автоматически находит все реализации `Ipreprocess` в текущей сборке и использует их.
 
- **tmp_localizator** : Реализация `Ipreprocess`, заменяющая ключи локализации в тексте формата `{ключ}` на их локализованные значения.

#### Примеры 

##### Локализация строки 


```csharp
using UnityEngine;
using Arhion;

public class LocalizationDemo : MonoBehaviour
{
    void Start()
    {
        // Инициализация локализации с указанием языка
        Localization.Initialize("ru");

        // Получение локализованного текста
        string welcomeMessage = Localization.GetLocalizedText("welcome_message");
        Debug.Log(welcomeMessage);
    }
}
```

##### Использование препроцессора TMP для локализации 


```csharp
using UnityEngine;
using TMPro;
using Arhion.TMP;

public class TMPDemo : MonoBehaviour
{
    [SerializeField] private TMP_Text textElement;

    void Start()
    {
        // Инициализация локализации
        Localization.Initialize("en");

        // Установка препроцессора для TextMeshPro
        var preprocessor = new Preprocessor();
        textElement.textPreprocessor = preprocessor;

        // Установка текста с локализационными ключами
        textElement.text = "Hello, {welcome_message}!";
    }
}
```

### Дополнительная информация 
 
- Локализация поддерживает загрузку JSON-файлов из папки `Resources/EvieCore/Arhion/Langs/`.
Пример структуры JSON:

```json
{
    "entries": [
        { "code": "welcome_message", "text": "Добро пожаловать!" },
        { "code": "goodbye_message", "text": "До свидания!" }
    ]
}
```
 
- Если ключ не найден в текущем языке, метод `GetLocalizedText` возвращает сам ключ и выводит предупреждение в консоль.

### Пример файла локализации 
Файл локализации представляет собой JSON-структуру, которая хранит пары ключ-значение для текстов на определённом языке. Файлы размещаются в папке `Resources/EvieCore/Arhion/Langs/`.Пример файла `en.json`

```json
{
    "entries": [
        { "code": "welcome_message", "text": "Welcome!" },
        { "code": "goodbye_message", "text": "Goodbye!" },
        { "code": "error_not_found", "text": "Error: Not found." }
    ]
}
```
Пример файла `ru.json`

```json
{
    "entries": [
        { "code": "welcome_message", "text": "Добро пожаловать!" },
        { "code": "goodbye_message", "text": "До свидания!" },
        { "code": "error_not_found", "text": "Ошибка: не найдено." }
    ]
}
```

### Обработка локализованных строк с препроцессором 
Препроцессор `tmp_localizator` обрабатывает текст формата `{ключ}` в строках TMP, заменяя ключ на локализованный текст. Это удобно, чтобы не задавать текст вручную в каждом элементе интерфейса.
#### Пример обработки текста 

Строка в TMP-объекте:


```plaintext
"Hello, {welcome_message}!"
```

После обработки препроцессором:


```plaintext
"Hello, Welcome!" // Для языка "en"
"Hello, Добро пожаловать!" // Для языка "ru"
```

### Расширение функциональности 
Вы можете создавать собственные реализации интерфейса `Ipreprocess` для добавления уникальной обработки текста. Например, можно добавить замену переменных или форматирование строк.
#### Пример кастомного препроцессора 


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
Добавьте этот препроцессор в вашу сборку, и он автоматически будет найден и использован `Preprocessor`.
### Вопросы и ответы 

#### Как добавить новый язык в систему локализации? 
 
1. Создайте JSON-файл с переводами (например, `fr.json` для французского).
 
2. Сохраните файл в папке `Resources/EvieCore/Arhion/Langs/`.
 
3. Вызовите `Localization.Initialize("fr")` или `Localization.SetLanguage("fr")` для переключения на этот язык.

#### Что произойдёт, если ключ отсутствует в текущем языке? 

Если ключ не найден, система выведет предупреждение в консоль, а вместо текста вернёт сам ключ. Это позволяет легко обнаружить отсутствующие переводы.

#### Как отладить локализацию? 
 
1. Убедитесь, что JSON-файлы находятся в правильной папке: `Resources/EvieCore/Arhion/Langs/`.

2. Проверьте, что ключи в файле корректно совпадают с теми, которые используются в коде или текстах TMP.

3. Включите логирование и следите за предупреждениями в консоли Unity.

#### Можно ли использовать локализацию без TMP? 
Да, локализацию можно использовать в любом текстовом элементе, не связанном с TMP. Вызовите `Localization.GetLocalizedText("ключ")`, чтобы получить локализованный текст.