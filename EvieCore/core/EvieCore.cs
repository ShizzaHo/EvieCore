using UnityEngine;
using UnityEngine.SceneManagement;

public class EvieCore : MonoBehaviour
{
    public static EvieCore Instance { get; private set; }

    [SerializeField]
    private bool DontDestroyThisObject = true;

    void Awake()
    {
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
