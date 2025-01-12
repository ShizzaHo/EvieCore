using NaughtyAttributes;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Eviecore
{
    public class attackState : ExinAIState
    {
        private NavMeshAgent navMeshAgent;
        private NavMeshSurface navMeshSurface;
        private ExinAIController exinAI;

        [InfoBox("This is a universal AI State that comes with ExinAI, create your own conditions for more fine-tuning.", EInfoBoxType.Normal)]
        [Header("Messages")]
        public bool sendMessages = true;
        [ShowIf("sendMessages")]
        public string isPlayerAttackEndMessage = "playerAttackEnd";

        [Header("AI Navigation Settings")]
        public float detectionRadius = 3f; // Радиус обнаружения игрока
        public float stopDistance = 1.5f; // Минимальная дистанция, при которой зомби останавливается

        [Header("Animations")]
        public bool useAnimations = false;
        [ShowIf("useAnimations")]
        [InfoBox("Universal AI State only work with triggers", EInfoBoxType.Normal)]
        public Animator animator;
        [ShowIf("useAnimations")]
        [AnimatorParam("animator")]
        public string attackAnimation;

        private Transform player;

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

            // Поиск игрока
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        public override void Enter()
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = false;

                if (useAnimations)
                {
                    animator.SetTrigger(attackAnimation);
                }
            }
        }

        public override void StateUpdate()
        {
            if (player == null || navMeshAgent == null) return;

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= stopDistance)
            {
                // Игрок рядом, зомби стоит и атакует
                navMeshAgent.isStopped = true;
            }
            else if (distanceToPlayer <= detectionRadius)
            {
                // Игрок в пределах радиуса, зомби преследует
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                // Игрок слишком далеко, отправляем сообщение
                if (exinAI != null)
                {
                    if (sendMessages) exinAI.Message(isPlayerAttackEndMessage);
                }
                navMeshAgent.isStopped = true;
            }
        }

        public override void Exit()
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = true;
            }
        }
    }
}