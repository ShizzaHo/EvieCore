using System.Collections.Generic;
using System;
using UnityEngine;

public interface IState
{
    void Enter();     // Вызывается при переходе в это состояние
    void Exit();      // Вызывается при выходе из этого состояния
    void Update();    // Вызывается каждый кадр
}

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    private IState currentState;
    private Dictionary<string, IState> states = new Dictionary<string, IState>();

    public event Action<IState> OnStateChanged;

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
        currentState?.Update();
    }

    public void RegisterState(string key, IState state)
    {
        if (!states.ContainsKey(key))
            states.Add(key, state);
    }

    public void ChangeState(string key)
    {
        if (states.TryGetValue(key, out IState newState))
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
            OnStateChanged?.Invoke(currentState);
        }
        else
        {
            Debug.LogWarning($"State with key '{key}' not found.");
        }
    }

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        OnStateChanged?.Invoke(currentState);
    }
}