using UnityEngine;

public class EvieCoreDefaultConsoleCommands : MonoBehaviour
{
    public Console console;

    void Start()
    {
        console = GetComponent<Console>();

        console.RegisterCommand("destroy", destroy);
    }

    public void destroy(string arg)
    {
        Destroy(GameObject.Find(arg));
    }
}
