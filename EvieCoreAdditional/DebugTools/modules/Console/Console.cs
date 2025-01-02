using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ConsoleCommander
{
    public string command; // Имя команды
    public UnityAction<string> callback; // Действие команды
}

public class Console : MonoBehaviour, EvieCoreDebugTool
{
    private bool isActive = false; // Статус активности консоли
    public TMP_InputField consoleField; // Поле ввода
    public TMP_Text consoleOutput;
    public List<ConsoleCommander> commands = new List<ConsoleCommander>(); // Список доступных команд
    private const int maxLines = 15; // Максимальное количество строк
    private List<string> logLines = new List<string>(); // Список для хранения логов

    private void Start()
    {
        // Регистрируем консоль в менеджере отладки
        DEBUGHUDManager.Instance.AddDebugTool(this, "Console");
    }

    private void Update()
    {
        if (!isActive) return;

        // Отправка команды при нажатии Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string inputText = consoleField.text.Trim();
            ExecuteCommand(inputText);
            consoleField.text = ""; // Очищаем поле ввода
        }
    }

    public void OnActivated()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        isActive = true;

        consoleField.ActivateInputField(); // Автоматический фокус на поле ввода
    }

    public void OnDeactivated()
    {
        isActive = false;
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        consoleField.text = ""; // Сбрасываем текст в поле ввода
    }

    public bool IsActive()
    {
        return isActive;
    }

    /// <summary>
    /// Выполнение команды.
    /// </summary>
    private void ExecuteCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return;

        string[] parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        string commandName = parts[0]; // Имя команды
        string argument = parts.Length > 1 ? parts[1] : ""; // Аргумент команды (если есть)

        // Ищем команду по имени
        var command = commands.Find(c => c.command.Equals(commandName, StringComparison.OrdinalIgnoreCase));
        if (command != null)
        {
            command.callback?.Invoke(argument); // Выполняем действие команды с аргументом
            LogToConsole($"Command '{commandName}' executed with argument: '{argument}'");
        }
        else
        {
            LogToConsole($"<color=red>Unknown command: {commandName}<color=white>");
        }
    }

    /// <summary>
    /// Вывод сообщения в консоль с ограничением на количество строк.
    /// </summary>
    public void LogToConsole(string message)
    {
        if (consoleOutput != null)
        {
            // Добавляем сообщение в список логов
            logLines.Add(message);

            // Если количество строк больше maxLines, удаляем старые
            if (logLines.Count > maxLines)
            {
                logLines.RemoveAt(0); // Удаляем самую старую строку
            }

            // Обновляем вывод в консоли
            consoleOutput.text = string.Join("\n", logLines);
        }
        Debug.Log("[EVIECOREADDITIONAL/DEBUGTOOLS/CONSOLE/LOG]" + message);
    }

    /// <summary>
    /// Регистрация новой команды.
    /// </summary>
    public void RegisterCommand(string commandName, UnityAction<string> callback)
    {
        var existingCommand = commands.Find(c => c.command.Equals(commandName, StringComparison.OrdinalIgnoreCase));
        if (existingCommand != null)
        {
            Debug.LogError($"[EVIECOREADDITIONAL/DEBUGTOOLS/CONSOLE/ERROR] The command '{commandName}' has already been registered.");
            return;
        }

        var newCommand = new ConsoleCommander
        {
            command = commandName,
            callback = callback // Прямое назначение UnityAction
        };

        commands.Add(newCommand);
    }
}