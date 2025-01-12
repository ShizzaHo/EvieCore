using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Eviecore
{

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
            if (!Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Для работы этого окна игра должна быть запущена.", MessageType.Warning);
                return;
            }

            if (TriggerManager.Instance == null)
            {
                EditorGUILayout.HelpBox("TriggerManager не найден в сцене. Убедитесь, что объект с этим компонентом существует.", MessageType.Warning);
                return;
            }

            EditorGUILayout.LabelField("Trigger Manager", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Search Triggers:", EditorStyles.boldLabel);
            string newSearchQuery = EditorGUILayout.TextField(searchQuery);

            if (newSearchQuery != searchQuery)
            {
                searchQuery = newSearchQuery;
                UpdateFilteredTriggers();
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Refresh Trigger List"))
            {
                UpdateFilteredTriggers();
            }

            EditorGUILayout.Space();

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            foreach (var trigger in filteredTriggers)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(trigger, GUILayout.Width(200));

                bool currentState = TriggerManager.Instance.GetTriggerState(trigger);
                bool newState = EditorGUILayout.Toggle(currentState);

                if (newState != currentState)
                {
                    TriggerManager.Instance.SetTriggerState(trigger, newState);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
        }

        private void UpdateFilteredTriggers()
        {
            filteredTriggers.Clear();

            if (TriggerManager.Instance == null) return;

            foreach (var trigger in TriggerManager.Instance.GetAllTriggers())
            {
                if (string.IsNullOrEmpty(searchQuery) || trigger.ToLower().Contains(searchQuery.ToLower()))
                {
                    filteredTriggers.Add(trigger);
                }
            }

            Repaint();
        }

        private void OnEnable()
        {
            UpdateFilteredTriggers();
        }
    }
}
