[[Назад]](../../../README%20RU.md)

# DebugTools

Полноценный внутриигровой оверлей паредоставляющий инструменты для отладки игры

### Модули

1. SimpleTool - Шаблон для создания своих собственных инструментов отладки

2. FreeCamera - Позволяет выйти из "тела" игрока и начать управлять свободной камерой, аналог noclip из Half Life

3. Console - Консоль для игры с возможностью расширения команд. ``EvieCoreDefaultConsoleCommands`` добавляет стандартные комманды для консоли поставляющиеся вместе с ``EvieCoreAdditional``

    <details>
        <summary>Все команды EvieCoreDefaultConsoleCommands</summary>

        eviecore - Выводит сообщение "<color=#6da2ce>EVIECORE FOREVER!!!<color=white>" в консоль. (пасхалочка :D)

        destroy [имя_объекта] - Уничтожает объект с указанным именем.

        timescale [значение] - Изменяет значение Time.timeScale.

        clrum - Очищает хранилище UpdateManager.

        clrdm - Очищает все данные в DataManager.

        rmdm [ключ] - Удаляет конкретные данные из DataManager по указанному ключу.

        sendmessage [текст_сообщения] - Отправляет сообщение через MessageManager.

        setgamestate [состояние] - Устанавливает состояние игры через StateManager.

        edittrigger [ключ]-[true/false] - Изменяет состояние триггера в TriggerManager. Ключ и значение разделяются дефисом -. Значение должно быть либо true, либо false.
    </details>

### Специальные модули 

1. UI - Интерфейс ``DebugTools``, добавьте префаб ``DEBUG_HUD`` на сцену.