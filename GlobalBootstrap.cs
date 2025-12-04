using UnityEngine;

public class GlobalBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void CreateGlobalManager()
    {
        GameObject pause = new GameObject();
        pause.AddComponent<PauseMenu>();
        GameObject storage = new GameObject();
        storage.AddComponent<StorageManager>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateGlobalManagerBefore()
    {
        GameObject notifications = new GameObject();
        notifications.AddComponent<NotificationServer>();
    }
}