using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Eviecore
{
    public class TemplateState : ExinAIState
    {
        private NavMeshAgent navMeshAgent;
        private NavMeshSurface navMeshSurface;
        private ExinAIController exinAI;

        private void Start()
        {
            exinAI = GetComponent<ExinAIController>();

            if (exinAI == null)
            {
                Debug.LogError($"[EVIECORE/EXINAI/ERROR] ExinAIController not found! Make sure it is added to the scene before using it {gameObject.name}.");
                return;
            }

            navMeshAgent = exinAI.NavMeshAgent;
            navMeshSurface = exinAI.NavMeshSurface;
        }

        public override void Enter()
        {

        }

        public override void StateUpdate()
        {

        }

        public override void Exit()
        {

        }
    }
}