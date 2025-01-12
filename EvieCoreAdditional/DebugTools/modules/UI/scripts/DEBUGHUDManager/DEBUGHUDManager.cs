using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Eviecore
{
    public interface EvieCoreDebugTool
    {
        void OnActivated();
        void OnDeactivated();
        bool IsActive();
    }

    public class DEBUGHUDManager : MonoBehaviour
    {
        public static DEBUGHUDManager Instance { get; private set; } // Синглтон

        [SerializeField] private Transform parentUIElement; // UI-элемент, куда добавлять префабы
        [SerializeField] private GameObject prefab; // Префаб для отображения элемента

        private List<EvieCoreDebugTool> debugTools = new List<EvieCoreDebugTool>();
        private List<string> toolNames = new List<string>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        // Метод для добавления элемента в списки и отображения
        public void AddDebugTool(EvieCoreDebugTool tool, string name)
        {
            if (tool == null || string.IsNullOrEmpty(name)) return;

            // Добавляем в списки
            debugTools.Add(tool);
            toolNames.Add(name);

            // Создаем объект на UI
            GameObject uiElement = Instantiate(prefab, parentUIElement);
            TMP_Text textComponent = uiElement.GetComponentInChildren<TMP_Text>(); // Предполагается, что в префабе есть Text

            if (textComponent != null)
            {
                textComponent.text = name;
            }

            Button buttonComponent = uiElement.GetComponentInChildren<Button>(); // Предполагается, что в префабе есть Button
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() =>
                {
                    if (!tool.IsActive())
                    {
                        tool.OnActivated();
                    }
                    else
                    {
                        tool.OnDeactivated();
                    }
                });
            }
        }

        public void DeactivateAll()
        {
            foreach (var tool in debugTools)
            {
                tool.OnDeactivated();
            }
        }
    }
}