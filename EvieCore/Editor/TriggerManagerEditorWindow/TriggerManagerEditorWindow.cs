using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TriggerManagerEditorWindow : EditorWindow
{
    private Vector2 scrollPosition;
    private string searchQuery = ""; // Строка поиска
    private List<string> filteredTriggers = new List<string>(); // Отфильтрованные триггеры

    [MenuItem("Window/EvieCore/Trigger Manager")]
    public static void ShowWindow()
    {
        GetWindow<TriggerManagerEditorWindow>("Trigger Manager");
    }

    private void OnGUI()
    {
        // Проверяем, запущена ли игра
        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Для работы этого окна игра должна быть запущена.", MessageType.Warning);
            return;
        }

        // Проверяем наличие экземпляра TriggerManager
        if (TriggerManager.Instance == null)
        {
            EditorGUILayout.HelpBox("TriggerManager не найден в сцене. Убедитесь, что объект с этим компонентом существует.", MessageType.Warning);
            return;
        }

        // Заголовок
        EditorGUILayout.LabelField("Trigger Manager", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Поле поиска
        EditorGUILayout.LabelField("Search Triggers:", EditorStyles.boldLabel);
        string newSearchQuery = EditorGUILayout.TextField(searchQuery);

        // Если строка поиска изменилась, обновляем список
        if (newSearchQuery != searchQuery)
        {
            searchQuery = newSearchQuery;
            UpdateFilteredTriggers();
        }

        EditorGUILayout.Space();

        // Кнопка обновления списка
        if (GUILayout.Button("Refresh Trigger List"))
        {
            UpdateFilteredTriggers();
        }

        EditorGUILayout.Space();

        // Начало области прокрутки
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        // Отображаем отфильтрованные триггеры
        foreach (var trigger in filteredTriggers)
        {
            EditorGUILayout.BeginHorizontal();

            // Отображаем имя триггера
            EditorGUILayout.LabelField(trigger, GUILayout.Width(200));

            // Переключатель состояния
            bool currentState = TriggerManager.Instance.GetTriggerState(trigger);
            bool newState = EditorGUILayout.Toggle(currentState);

            // Если состояние изменилось, обновляем его
            if (newState != currentState)
            {
                TriggerManager.Instance.SetTriggerState(trigger, newState);
            }

            EditorGUILayout.EndHorizontal();
        }

        // Конец области прокрутки
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// Обновить список триггеров на основе строки поиска.
    /// </summary>
    private void UpdateFilteredTriggers()
    {
        filteredTriggers.Clear();

        if (TriggerManager.Instance == null) return;

        foreach (var trigger in TriggerManager.Instance.GetAllTriggers())
        {
            // Фильтрация по строке поиска (регистр игнорируется)
            if (string.IsNullOrEmpty(searchQuery) || trigger.ToLower().Contains(searchQuery.ToLower()))
            {
                filteredTriggers.Add(trigger);
            }
        }

        Repaint();
    }

    private void OnEnable()
    {
        UpdateFilteredTriggers(); // Инициализация списка при открытии окна
    }
}
