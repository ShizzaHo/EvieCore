using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsManagerAudioConfig
{
    public string tag; // Тег аудио источника
    public string playerPrefsFloatString; // Ключ для хранения громкости в PlayerPrefs
    public float defaultValue = 1.0f; // Значение по умолчанию
}

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; } // Синглтон

    public bool isActive = true;

    [Header("Audio Settings")]
    public string playerPrefsVolumeGeneral = "volume_general"; // Общий ключ громкости
    public float defaultVolumeGeneral = 1.0f; // Громкость по умолчанию
    public List<SettingsManagerAudioConfig> audioSettings; // Конфигурации аудио
    private Dictionary<string, float> volumeSettings; // Словарь для быстрого доступа к настройкам

    [Header("Graphics Settings")]
    public string playerPrefsResolution = "graphics_resolution";
    public string playerPrefsFullscreen = "graphics_fullscreen";
    public string playerPrefsQuality = "graphics_quality";
    public string playerPrefsWindowMode = "graphics_window_mode";

    public Resolution defaultResolution = new Resolution { width = 1920, height = 1080 }; // Разрешение по умолчанию
    public bool defaultFullscreen = true; // Полноэкранный режим по умолчанию
    public int defaultQualityLevel = 1; // Уровень качества по умолчанию
    public WindowMode defaultWindowMode = WindowMode.Fullscreen; // Режим окна по умолчанию

    public enum WindowMode
    {
        Fullscreen,
        Windowed,
        Borderless
    }

    private Resolution currentResolution;
    private bool isFullscreen;
    private int qualityLevel;
    private WindowMode currentWindowMode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeDefaultSettings();
        AudioSettingsInitialization();
        GraphicsSettingsInitialization();
    }

    #region defaults

    private void InitializeDefaultSettings()
    {
        // Устанавливаем значения по умолчанию, если PlayerPrefs пуст
        if (!PlayerPrefs.HasKey(playerPrefsVolumeGeneral))
        {
            PlayerPrefs.SetFloat(playerPrefsVolumeGeneral, defaultVolumeGeneral);
        }

        foreach (var config in audioSettings)
        {
            if (!PlayerPrefs.HasKey(config.playerPrefsFloatString))
            {
                PlayerPrefs.SetFloat(config.playerPrefsFloatString, config.defaultValue);
            }
        }

        if (!PlayerPrefs.HasKey(playerPrefsResolution))
        {
            PlayerPrefs.SetString(playerPrefsResolution, $"{defaultResolution.width}x{defaultResolution.height}");
        }

        if (!PlayerPrefs.HasKey(playerPrefsFullscreen))
        {
            PlayerPrefs.SetInt(playerPrefsFullscreen, defaultFullscreen ? 1 : 0);
        }

        if (!PlayerPrefs.HasKey(playerPrefsQuality))
        {
            PlayerPrefs.SetInt(playerPrefsQuality, defaultQualityLevel);
        }

        if (!PlayerPrefs.HasKey(playerPrefsWindowMode))
        {
            PlayerPrefs.SetInt(playerPrefsWindowMode, (int)defaultWindowMode);
        }
    }

    #endregion

    #region audio

    private void AudioSettingsInitialization()
    {
        volumeSettings = new Dictionary<string, float>();
        foreach (var config in audioSettings)
        {
            float volume = PlayerPrefs.GetFloat(config.playerPrefsFloatString, config.defaultValue);
            volumeSettings[config.tag] = volume;
        }

        UpdateAudioVolumes();
    }

    public void UpdateAudioVolumes()
    {
        if (!isActive) return;

        float volumeGeneral = PlayerPrefs.GetFloat(playerPrefsVolumeGeneral, defaultVolumeGeneral);

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource == null) continue;

            string tag = audioSource.tag;
            if (volumeSettings.ContainsKey(tag))
            {
                audioSource.volume = volumeGeneral * volumeSettings[tag];
            }
        }
    }

    public void SetAudioVolume(string tag, float volume)
    {
        if (!volumeSettings.ContainsKey(tag)) return;

        volumeSettings[tag] = volume;
        PlayerPrefs.SetFloat(tag, volume);
        UpdateAudioVolumes();
    }

    #endregion

    #region graphics

    private void GraphicsSettingsInitialization()
    {
        string resolutionStr = PlayerPrefs.GetString(playerPrefsResolution, $"{defaultResolution.width}x{defaultResolution.height}");
        isFullscreen = PlayerPrefs.GetInt(playerPrefsFullscreen, defaultFullscreen ? 1 : 0) == 1;
        qualityLevel = PlayerPrefs.GetInt(playerPrefsQuality, defaultQualityLevel);
        currentWindowMode = (WindowMode)PlayerPrefs.GetInt(playerPrefsWindowMode, (int)defaultWindowMode);

        string[] resParts = resolutionStr.Split('x');
        if (resParts.Length == 2 && int.TryParse(resParts[0], out int width) && int.TryParse(resParts[1], out int height))
        {
            currentResolution = new Resolution { width = width, height = height };
        }
        else
        {
            currentResolution = defaultResolution;
        }

        ApplyGraphicsSettings();
    }

    public void ApplyGraphicsSettings()
    {
        switch (currentWindowMode)
        {
            case WindowMode.Fullscreen:
                Screen.SetResolution(currentResolution.width, currentResolution.height, FullScreenMode.ExclusiveFullScreen);
                break;
            case WindowMode.Windowed:
                Screen.SetResolution(currentResolution.width, currentResolution.height, FullScreenMode.Windowed);
                break;
            case WindowMode.Borderless:
                Screen.SetResolution(currentResolution.width, currentResolution.height, FullScreenMode.FullScreenWindow);
                break;
        }

        QualitySettings.SetQualityLevel(qualityLevel);
    }

    public void ResetGraphicsToDefaults()
    {
        PlayerPrefs.SetString(playerPrefsResolution, $"{defaultResolution.width}x{defaultResolution.height}");
        PlayerPrefs.SetInt(playerPrefsFullscreen, defaultFullscreen ? 1 : 0);
        PlayerPrefs.SetInt(playerPrefsQuality, defaultQualityLevel);
        PlayerPrefs.SetInt(playerPrefsWindowMode, (int)defaultWindowMode);
        GraphicsSettingsInitialization();
    }

    public void ResetAudioToDefaults()
    {
        PlayerPrefs.SetFloat(playerPrefsVolumeGeneral, defaultVolumeGeneral);
        foreach (var config in audioSettings)
        {
            PlayerPrefs.SetFloat(config.playerPrefsFloatString, config.defaultValue);
        }
        AudioSettingsInitialization();
    }

    #endregion
}
