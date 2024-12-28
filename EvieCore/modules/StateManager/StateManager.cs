using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    [SerializeField]
    private List<string> states = new List<string>();

    private string currentState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetState(states[0]);
    }

    public void SetState(string newState)
    {
        if (states.Contains(newState))
        {
            currentState = newState;
            Debug.Log($"Текущее состояние изменено на: {currentState}");
        }
        else
        {
            Debug.LogWarning($"StateManager: Состояние '{newState}' отсутствует в списке возможных состояний.");
        }
    }

    public string GetCurrentState()
    {
        return currentState;
    }

    public bool IsCurrentState(string state)
    {
        return currentState == state;
    }

    public void AddState(string newState)
    {
        if (!states.Contains(newState))
        {
            states.Add(newState);
            Debug.Log($"Состояние '{newState}' добавлено в список.");
        }
        else
        {
            Debug.LogWarning($"StateManager: Состояние '{newState}' уже существует в списке.");
        }
    }
}
