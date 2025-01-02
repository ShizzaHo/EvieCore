[[Назад]](./main.md)

# SettingsManager <span style="font-size: 10px">[EvieCore/module]</span>

## Описание

Класс `SettingsManager` отвечает за управление настройками игры, включая аудио и графику. Он предоставляет возможности для сохранения и загрузки пользовательских предпочтений с помощью `PlayerPrefs`, а также для их применения в реальном времени. Реализован в виде синглтона, чтобы быть доступным из любой части приложения.

---

## Классы и интерфейсы 
**1. SettingsManagerAudioConfig**  
- **Описание** : Содержит конфигурацию для настройки аудио.
 
- **Поля** : 
  - `tag` (string): Тег аудио источника.
 
  - `playerPrefsFloatString` (string): Ключ для сохранения уровня громкости в `PlayerPrefs`.
 
  - `defaultValue` (float): Значение громкости по умолчанию (по умолчанию `1.0`).
**2. SettingsManager**  
- **Описание** : Главный класс для управления настройками.
 
- **Синглтон** : 
  - `Instance`: Статическое свойство для доступа к единственному экземпляру.
 
- **Поля** : 
  - **Аудио настройки** : 
    - `playerPrefsVolumeGeneral` (string): Ключ для общей громкости.
 
    - `defaultVolumeGeneral` (float): Общая громкость по умолчанию.
 
    - `audioSettings` (List<SettingsManagerAudioConfig>): Список настроек аудио.
 
    - `volumeSettings` (Dictionary<string, float>): Словарь громкостей для быстрых операций.
 
  - **Графические настройки** : 
    - `playerPrefsResolution` (string): Ключ для разрешения экрана.
 
    - `playerPrefsFullscreen` (string): Ключ для полноэкранного режима.
 
    - `playerPrefsQuality` (string): Ключ для качества графики.
 
    - `playerPrefsWindowMode` (string): Ключ для режима окна.
 
    - `defaultResolution` (Resolution): Разрешение экрана по умолчанию.
 
    - `defaultFullscreen` (bool): Полноэкранный режим по умолчанию.
 
    - `defaultQualityLevel` (int): Уровень качества по умолчанию.
 
    - `defaultWindowMode` (WindowMode): Режим окна по умолчанию.
 
- **Методы** : 
  - **Инициализация настроек** : 
    - `InitializeDefaultSettings()`: Устанавливает значения по умолчанию, если настройки отсутствуют.
 
  - **Аудио** : 
    - `AudioSettingsInitialization()`: Загружает и применяет аудио настройки.
 
    - `UpdateAudioVolumes()`: Обновляет громкость аудио источников.
 
    - `SetAudioVolume(string tag, float volume)`: Устанавливает громкость для источника с указанным тегом.
 
    - `ResetAudioToDefaults()`: Сбрасывает аудио настройки к значениям по умолчанию.
 
  - **Графика** : 
    - `GraphicsSettingsInitialization()`: Загружает и применяет графические настройки.
 
    - `ApplyGraphicsSettings()`: Применяет текущие настройки графики.
 
    - `ResetGraphicsToDefaults()`: Сбрасывает графические настройки к значениям по умолчанию.
**3. WindowMode (Enum)**  
- **Описание** : Определяет режим отображения окна.
 
- **Значения** : 
  - `Fullscreen`: Полноэкранный режим.
 
  - `Windowed`: Оконный режим.
 
  - `Borderless`: Безрамочный режим.


---

## Пример использования 

**1. Инициализация SettingsManager** 

```csharp
void Awake()
{
    SettingsManager.Instance.InitializeDefaultSettings();
}
```
**2. Установка громкости для аудио источника** 

```csharp
SettingsManager.Instance.SetAudioVolume("Music", 0.5f);
```
**3. Применение графических настроек** 

```csharp
SettingsManager.Instance.ApplyGraphicsSettings();
```
**4. Сброс настроек к значениям по умолчанию** 

```csharp
SettingsManager.Instance.ResetAudioToDefaults();
SettingsManager.Instance.ResetGraphicsToDefaults();
```


---

## Заключение 

Класс `SettingsManager` предоставляет удобный способ управления настройками игры с использованием `PlayerPrefs`. Его гибкость позволяет разработчику легко добавлять новые параметры, а также упрощает работу с сохранением и загрузкой пользовательских данных. Реализованный синглтон гарантирует, что настройки всегда будут доступны и централизованы.