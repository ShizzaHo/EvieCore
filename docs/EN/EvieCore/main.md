[[Back]](../../../README.md)

# EvieCore

## Modules

1. [Update Manager](./updateManager.md) - Centralized Update() to improve the efficiency of your game

2. [Data Manager](./dataManager.md) - Centralized data storage

3. [MessageManager](./messageManager.md) - A messaging system without a bundle of components (Similar to messages in Scratch)

4. [Trigger Manager](./triggerManager.md ) - Trigger state management system (useful for creating event systems in a game or controlling interaction logic)

    4.1. Trigger Manager Window - A window in which you can change triggers during the game, it is a tool for debugging the game (you can open it using the following path: Window/EvieCore/Trigger Manager)
    
    4.2. Trigger Zone (Prefab and script) - Allows you to create zones that will be triggered when an object (such as a player) hits and check triggers

## Controllers

1. [FirstPersonController](./fpc.md) - A full-fledged "constructor" of the player with a first-person view with extensive functionality

2. [SimpleHUD](./hud.md) - HUD's game constructor, a set of ready-made components

3. [TriggerZone](./triggerZone.md) - An advanced zone handler that tracks the penetration of an object with a specific tag into the zone