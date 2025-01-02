[[Back]](./main.md)

# SettingsManager <span style="font-size: 10px">[EvieCore/module]</span>

## Description

The `SettingsManager` class manages game settings, including audio and graphics configurations. It provides functionalities for saving and loading user preferences using `PlayerPrefs` and applies them in real-time. Implemented as a singleton, it ensures accessibility from anywhere within the application.

---

## Classes and Interfaces 
**1. SettingsManagerAudioConfig**  
- **Description** : Stores configuration for audio settings.
 
- **Fields** : 
  - `tag` (string): Tag of the audio source.
 
  - `playerPrefsFloatString` (string): Key for saving the volume level in `PlayerPrefs`.
 
  - `defaultValue` (float): Default volume value (defaults to `1.0`).
**2. SettingsManager**  
- **Description** : Main class for managing game settings.
 
- **Singleton** : 
  - `Instance`: Static property to access the single instance.
 
- **Fields** : 
  - **Audio Settings** : 
    - `playerPrefsVolumeGeneral` (string): Key for master volume.
 
    - `defaultVolumeGeneral` (float): Default master volume.
 
    - `audioSettings` (List<SettingsManagerAudioConfig>): List of audio configurations.
 
    - `volumeSettings` (Dictionary<string, float>): Dictionary of audio volumes for quick access.
 
  - **Graphics Settings** : 
    - `playerPrefsResolution` (string): Key for screen resolution.
 
    - `playerPrefsFullscreen` (string): Key for fullscreen mode.
 
    - `playerPrefsQuality` (string): Key for graphics quality.
 
    - `playerPrefsWindowMode` (string): Key for window mode.
 
    - `defaultResolution` (Resolution): Default screen resolution.
 
    - `defaultFullscreen` (bool): Default fullscreen mode.
 
    - `defaultQualityLevel` (int): Default graphics quality level.
 
    - `defaultWindowMode` (WindowMode): Default window mode.
 
- **Methods** : 
  - **Initialization** : 
    - `InitializeDefaultSettings()`: Sets default values if preferences are not found.
 
  - **Audio** : 
    - `AudioSettingsInitialization()`: Loads and applies audio settings.
 
    - `UpdateAudioVolumes()`: Updates the volume of audio sources.
 
    - `SetAudioVolume(string tag, float volume)`: Sets the volume for an audio source with a specified tag.
 
    - `ResetAudioToDefaults()`: Resets audio settings to default values.
 
  - **Graphics** : 
    - `GraphicsSettingsInitialization()`: Loads and applies graphics settings.
 
    - `ApplyGraphicsSettings()`: Applies current graphics settings.
 
    - `ResetGraphicsToDefaults()`: Resets graphics settings to default values.
**3. WindowMode (Enum)**  
- **Description** : Defines the window display mode.
 
- **Values** : 
  - `Fullscreen`: Fullscreen mode.
 
  - `Windowed`: Windowed mode.
 
  - `Borderless`: Borderless window mode.


---

## Usage Example 

**1. Initializing the SettingsManager** 

```csharp
void Awake()
{
    SettingsManager.Instance.InitializeDefaultSettings();
}
```
**2. Setting Volume for an Audio Source** 

```csharp
SettingsManager.Instance.SetAudioVolume("Music", 0.5f);
```
**3. Applying Graphics Settings** 

```csharp
SettingsManager.Instance.ApplyGraphicsSettings();
```
**4. Resetting Settings to Defaults** 

```csharp
SettingsManager.Instance.ResetAudioToDefaults();
SettingsManager.Instance.ResetGraphicsToDefaults();
```


---

## Conclusion
The `SettingsManager` class provides a convenient way to manage game settings using `PlayerPrefs`. Its flexibility allows developers to easily add new parameters and simplifies the process of saving and loading user preferences. The singleton implementation ensures that settings are centralized and always accessible.
