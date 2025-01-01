using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvieCore : MonoBehaviour
{
    public static EvieCore Instance { get; private set; }

    [SerializeField]
    private bool DontDestroyThisObject = true;

    [Header("SybLibs initialize")]
    [SerializeField]
    private bool InitializeEvieFS = true;

    [SerializeField]
    private bool InitializeEvieSaveLoad = true;

    void Awake()
    {
        if (InitializeEvieFS) EvieFS.Initialize();

        if (InitializeEvieSaveLoad) EvieSaveLoad.Initialize();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (DontDestroyThisObject)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
