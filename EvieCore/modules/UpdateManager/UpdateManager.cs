using System.Collections.Generic;
using UnityEngine;

public interface EvieCoreUpdate
{
    void OnUpdate();
}

public class UpdateManager : MonoBehaviour
{
    public static UpdateManager Instance { get; private set; }

    private List<EvieCoreUpdate> updateStorage = new List<EvieCoreUpdate>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        foreach (var updatable in updateStorage)
        {
            updatable.OnUpdate();
        }
    }

    public void Register(EvieCoreUpdate updatable)
    {
        if (!updateStorage.Contains(updatable))
            updateStorage.Add(updatable);
    }

    public void Unregister(EvieCoreUpdate updatable)
    {
        updateStorage.Remove(updatable);
    }

    public void EnableDontDestroyOnLoad()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ClearStorage()
    {
        updateStorage.Clear();
    }
}
