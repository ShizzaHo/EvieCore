using System;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EvieCoreDefaultConsoleCommands : MonoBehaviour
{
    private Console console;

    void Start()
    {
        console = GetComponent<Console>();

        console.RegisterCommand("eviecore", EvieCore);

        console.RegisterCommand("destroy", _Destroy);
        console.RegisterCommand("timescale", TimeScale);

        console.RegisterCommand("clrum", ClearUpdateManager);

        console.RegisterCommand("clrdm", ClearDataManager);
        console.RegisterCommand("rmdm", RemoveDataManager);

        console.RegisterCommand("sendmessage", SendMessageManager);

        console.RegisterCommand("setgamestate", SetGameState);

        console.RegisterCommand("edittrigger", EditTrigger);
    }
    public void EvieCore(string arg)
    {
        console.LogToConsole("<color=#6da2ce>EVIECORE FOREVER!!!<color=white>");
    }

    public void _Destroy(string arg)
    {
        Destroy(GameObject.Find(arg));
    }

    public void TimeScale(string arg)
    {
        Time.timeScale = float.Parse(arg);
    }

    public void ClearUpdateManager(string arg)
    {
        UpdateManager.Instance.ClearStorage();
    }

    public void ClearDataManager(string arg)
    {
        DataManager.Instance.ClearAllData();
    }

    public void RemoveDataManager(string arg)
    {
        DataManager.Instance.RemoveData(arg);
    }

    public void SendMessageManager(string arg)
    {
        MessageManager.Instance.SendMessage(arg);
    }

    public void SetGameState(string arg)
    {
        StateManager.Instance.SetState(arg);
    }

    public void EditTrigger(string arg)
    {
        if (string.IsNullOrWhiteSpace(arg))
        {
            console.LogToConsole("<color=red>Error: Argument cannot be null or empty.<color=white>");
            return;
        }

        if (!arg.Contains("-"))
        {
            console.LogToConsole("<color=yellow>The command argument is incorrect, use the entry format: <key>-<true/false><color=white>");
            return;
        }

        string[] argSplit = arg.Split('-');

        if (argSplit.Length != 2)
        {
            console.LogToConsole("<color=yellow>The command argument is incorrect, use the entry format: <key>-<true/false><color=white>");
            return;
        }

        string key = argSplit[0].Trim();
        string value = argSplit[1].Trim();

        if (string.IsNullOrWhiteSpace(key))
        {
            console.LogToConsole("<color=red>Error: Key cannot be null or empty.<color=white>");
            return;
        }

        if (!bool.TryParse(value, out bool state))
        {
            console.LogToConsole($"<color=yellow>Error: Value '{value}' is not a valid boolean. Use 'true' or 'false'.<color=white>");
            return;
        }

        try
        {
            TriggerManager.Instance.SetTriggerState(key, state);
            console.LogToConsole($"<color=green>Trigger '{key}' successfully set to {state}.<color=white>");
        }
        catch (Exception ex)
        {
            console.LogToConsole($"<color=red>Error setting trigger state: {ex.Message}<color=white>");
        }
    }
}
