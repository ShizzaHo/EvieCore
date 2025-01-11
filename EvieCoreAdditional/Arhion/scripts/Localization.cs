using UnityEngine;
using System.Collections.Generic;

namespace Arhion
{
    public static class Localization
    {
        private static Dictionary<string, string> localizedTexts = new Dictionary<string, string>();
        private static string currentLanguage;

        /// <summary>
        /// Инициализация локализации с выбором языка.
        /// </summary>
        public static void Initialize(string langName)
        {
            SetLanguage(langName);
        }

        /// <summary>
        /// Устанавливает текущий язык и загружает соответствующий JSON-файл.
        /// </summary>
        public static void SetLanguage(string langName)
        {
            currentLanguage = langName;
            localizedTexts.Clear();

            // Загружаем файл из папки Resources
            TextAsset jsonFile = Resources.Load<TextAsset>($"EvieCore/Arhion/Langs/{langName}");

            if (jsonFile != null)
            {
                localizedTexts = JsonUtility.FromJson<LocalizationData>(jsonFile.text).ToDictionary();
            }
            else
            {
                Debug.LogError($"Localization file not found in Resources: Einferia/Langs/{langName}");
            }
        }

        /// <summary>
        /// Возвращает локализованную строку по ключу.
        /// </summary>
        public static string GetLocalizedText(string key)
        {
            if (localizedTexts.TryGetValue(key, out string localizedText))
            {
                return localizedText;
            }
            else
            {
                Debug.LogWarning($"Key '{key}' not found in localization for language '{currentLanguage}'.");
                return key; // Возвращаем ключ, если перевод не найден.
            }
        }

        [System.Serializable]
        private class LocalizationData
        {
            public List<LocalizationEntry> entries;

            public Dictionary<string, string> ToDictionary()
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (var entry in entries)
                {
                    dict[entry.code] = entry.text;
                }
                return dict;
            }
        }

        [System.Serializable]
        private class LocalizationEntry
        {
            public string code;
            public string text;
        }
    }
}
