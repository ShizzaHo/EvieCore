using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ConsoleCommander
{
    public string command; // ��� �������
    public UnityAction<string> callback; // �������� �������
}

public class Console : MonoBehaviour, EvieCoreDebugTool
{
    private bool isActive = false; // ������ ���������� �������
    public TMP_InputField consoleField; // ���� �����
    public TMP_Text consoleOutput;
    public List<ConsoleCommander> commands = new List<ConsoleCommander>(); // ������ ��������� ������
    private const int maxLines = 15; // ������������ ���������� �����
    private List<string> logLines = new List<string>(); // ������ ��� �������� �����

    private void Start()
    {
        // ������������ ������� � ��������� �������
        DEBUGHUDManager.Instance.AddDebugTool(this, "Console");
    }

    private void Update()
    {
        if (!isActive) return;

        // �������� ������� ��� ������� Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string inputText = consoleField.text.Trim();
            ExecuteCommand(inputText);
            consoleField.text = ""; // ������� ���� �����
        }
    }

    public void OnActivated()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        isActive = true;

        consoleField.ActivateInputField(); // �������������� ����� �� ���� �����
    }

    public void OnDeactivated()
    {
        isActive = false;
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        consoleField.text = ""; // ���������� ����� � ���� �����
    }

    public bool IsActive()
    {
        return isActive;
    }

    /// <summary>
    /// ���������� �������.
    /// </summary>
    private void ExecuteCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return;

        string[] parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        string commandName = parts[0]; // ��� �������
        string argument = parts.Length > 1 ? parts[1] : ""; // �������� ������� (���� ����)

        // ���� ������� �� �����
        var command = commands.Find(c => c.command.Equals(commandName, StringComparison.OrdinalIgnoreCase));
        if (command != null)
        {
            command.callback?.Invoke(argument); // ��������� �������� ������� � ����������
            LogToConsole($"Command '{commandName}' executed with argument: '{argument}'");
        }
        else
        {
            LogToConsole($"<color=red>Unknown command: {commandName}<color=white>");
        }
    }

    /// <summary>
    /// ����� ��������� � ������� � ������������ �� ���������� �����.
    /// </summary>
    public void LogToConsole(string message)
    {
        if (consoleOutput != null)
        {
            // ��������� ��������� � ������ �����
            logLines.Add(message);

            // ���� ���������� ����� ������ maxLines, ������� ������
            if (logLines.Count > maxLines)
            {
                logLines.RemoveAt(0); // ������� ����� ������ ������
            }

            // ��������� ����� � �������
            consoleOutput.text = string.Join("\n", logLines);
        }
        Debug.Log("[EVIECOREADDITIONAL/DEBUGTOOLS/CONSOLE/LOG]" + message);
    }

    /// <summary>
    /// ����������� ����� �������.
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
            callback = callback // ������ ���������� UnityAction
        };

        commands.Add(newCommand);
    }
}