[[Back]](../main.md)

# DebugTools

A full-fledged in-game overlay that provides debugging tools for the game

### Modules

1. SimpleTool is a template for creating your own debugging tools

2. FreeCamera - Allows you to exit the player's "body" and start controlling a free camera, an analog of noclip from Half Life

3. Console - A console for playing with the ability to expand commands. ``EvieCoreDefaultConsoleCommands`` adds the standard commands for the console that come with `EvieCoreAdditional`

    <details>
        <summary>All commands EvieCoreDefaultConsoleCommands</summary>

        eviecore - Displays the message "<color=#6da2ce>EVIECORE FOREVER!!!<color=white>" to the console. (Easter egg :D)

        destroy [object_name] - Destroys an object with the specified name.

        timescale [value] - Changes the value of Time.timeScale.

        clean - Cleans the UpdateManager storage.

        clrdm - Clears all data in the DataManager.

        rmdm [key] - Deletes specific data from the DataManager using the specified key.

        sendmessage [message text] - Sends a message via MessageManager.

        setgamestate [state] - Sets the state of the game via StateManager.

        edittrigger [key]-[true/false] - Changes the state of the trigger in the TriggerManager. The key and value are separated by a hyphen -. The value must be either true or false.
    </details>

### Special modules 

1. The ``DebugTools`` UI, add the ``DEBUG_HUD`` prefab to the stage.