[[Back]](./main.md)

# FirstPersonController <span style="font-size: 10px">[EvieCore/controller]</span>

## Description 

FirstPersonController is a сontroller that allows you to flexibly configure a player with a first—person view to perform various tasks, including movement, interaction with the environment, and camera control.

## What is included in the structure

### Prefabs

1. ``FPC_Player`` - The basic prefab of the player, responsible for movement, interaction with the environment and integration with the control system.

2. ``FPC_Camera`` - The camera provides a first-person view.

### Scripts

1. `BasicMovement" - Processes keystrokes to move the player. Supports flexible configuration via the inspector. It is necessary for the basic movement functionality, it is assigned to the player's object.

2. ``CameraRaycast`` - Releases a beam from the player's camera to interact with the environment, there is a flexible setting for sending messages via `MessageManager` and `UnityEvent`, the camera can be configured in the inspector, but it is recommended to hang on the camera from which the beam will be released

3. `MovementCameraAnimation" <font color="red">[deprecated, notWorking]</font> - Designed to animate the camera when moving. It is not recommended to use the constructor in the current version.

4. `PlayerCamera" - Controls the camera from the first person. Processes mouse movements to control turns. Flexibly configured in the inspector. Assigned to the camera object.

### Borrowings from "EvieCore/utils`

1. `CameraPin" - Fixes the camera at a given coordinate. It is convenient for scenes where the camera must remain at one point relative to the subject.