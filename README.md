[[RUSSIAN README]](./README%20RU.md)



!!!

The project is closed and migrated to the new EvieEngine product. It has no detailed documentation, and was created purely for my beloved. If you would like to understand it, you are welcome to the new repository, there is an Einferia in the public domain.

Проект закрыт и мигрирован в новый продукт EvieEngine. Он без подробной документации, и создан чисто для меня любимого. Если вам охотно разобраться в нем, милости прошу в новый репозиторий, там есть Einferia в открытом доступе.

[Evie Engine]([./docs/EN/EvieCoreAdditional/main.md](https://github.com/ShizzaHo/EvieEngine))

!!!



# EvieCore

EvieCore is a set of tools and solutions for game development. The main goal of EvieCore is to simplify developers' work, streamline processes and organize the project.  

EvieCore provides powerful tools for game development, simplifying the process and allowing you to focus on creating a unique game experience.

## Main components of EvieCore

1. [EvieCore](./docs/EN/EvieCore/main.md) is the centerpiece that unifies all the tools. It can be used as a standalone tool, as modules and controllers allow you to work independently.

2. [EvieCoreAdditional](./docs/EN/EvieCoreAdditional/main.md) - additional libraries integrated into EvieCore logic. There are both free and paid solutions.

3. EvieCore/SubLibs — Additional libraries for EvieCore, deeply integrated with EvieCore

    3.1 EvieFS — Allows you to conveniently access the file system

    3.1.1 EvieConfigManager — Allows you to create and manage configs in the `.EvieConfig` format

    3.2 EvieSaveLoad — A universal save/upload system for your projects

4. EvieCore/Utils — Auxiliary scripts for EvieCore operation

    4.1. CameraPin — Attaches the camera to the object

    4.2. DictionaryJSON — Allows you to serialize and deserialize the Dictionary

    4.3. ObjectRoll — Creates smooth movement for an object when moving, it is recommended to use it in conjunction with `FPC_Camera`

    4.4. UnityActionEasyLogger <font color="red">[Usually]</font> — It is needed in order to bind the log to "UnityEvents`

## Installation
Follow the steps below to install EvieCore:  

* Download the .unitypackage from the release.

* Import the .unitypackage into Unity.

* Install the required dependencies via the package manager. (see below).

## Dependencies 

1. [NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes)

## Usage

* To get started with EvieCore:  

* Create an EvieCore component on the main stage (prefabs can be found in EvieCore\prefabs, or create an empty object with an EvieCore script)

* Add child objects with script modules (also available in EvieCore\prefabs)
