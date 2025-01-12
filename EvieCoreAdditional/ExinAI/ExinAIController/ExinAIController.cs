using NaughtyAttributes;
using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Eviecore;

namespace Eviecore
{
    public interface IExinAIState
    {
        void Enter();
        void StateUpdate();
        void Exit();
    }

    [Serializable]
    public abstract class ExinAIState : MonoBehaviour, IExinAIState
    {
        public virtual void Enter() { }
        public virtual void StateUpdate() { }
        public virtual void Exit() { }
    }

    [Serializable]
    public class AIState
    {
        public ExinAIState state;
        public string name;
    }

    [Serializable]
    public class AIStateChanger
    {
        public string stateName;
        public string message;
    }

    public class ExinAIController : MonoBehaviour, EvieCoreUpdate
    {
        [Header("NavMesh configuration")]
        public NavMeshAgent NavMeshAgent;
        public NavMeshSurface NavMeshSurface;

        [Header("States vonfiguration")]
        [Tooltip("Add here all the AI states that the object can use")]
        [SerializeField]
        private List<AIState> StatesLibrary = new List<AIState>();

        [Header("Automatic status switches")]
        [Tooltip("(Does not depend on EvieCore/Message Manager) add here the names of states and messages in which there will be a complete state switch, use this approach instead of changeState(string)")]
        [SerializeField]
        private List<AIStateChanger> AutoChanger = new List<AIStateChanger>();

        [ReadOnly]
        [SerializeField]
        private ExinAIState currentState;

        private void Start()
        {
            if (UpdateManager.Instance != null)
            {
                UpdateManager.Instance.Register(this);
            }
            else
            {
                Debug.LogError($"[EVIECORE/ERROR] UpdateManager not found! Make sure it is added to the scene before using it {gameObject.name}.");
            }

            NavMeshAgent = GetComponent<NavMeshAgent>();

            if (NavMeshAgent == null)
            {
                Debug.LogError($"[EVIECORE/EXINAI/ERROR] NavMeshAgent not found! Make sure it is added to the scene before using it {gameObject.name}.");
            }

            ChangeState(StatesLibrary[0].name);
        }

        private void OnDestroy()
        {
            UpdateManager.Instance.Unregister(this);
        }

        public void ChangeState(string name)
        {
            var newState = StatesLibrary.Find(s => s.name == name)?.state;
            if (newState != null && newState != currentState)
            {
                currentState?.Exit();
                currentState = newState;
                currentState.Enter();
            }
        }

        public void Message(string message)
        {
            AIStateChanger AIStateChanger = AutoChanger.Find(s => s.message == message);

            if (AIStateChanger != null)
            {
                ChangeState(AIStateChanger.stateName);
            }
            else
            {
                Debug.LogWarning($"[EVIECORE/EXINAI/WARNING] State with name '{message}' not found in AutoChanger list.");
            }
        }

        public void OnUpdate()
        {
            currentState.StateUpdate();
        }
    }
}