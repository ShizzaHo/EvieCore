using NaughtyAttributes;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class chaseState : ExinAIState
{
    private NavMeshAgent navMeshAgent;
    private NavMeshSurface navMeshSurface;
    private ExinAIController exinAI;

    [InfoBox("This is a universal AI State that comes with ExinAI, create your own conditions for more fine-tuning.", EInfoBoxType.Normal)]
    [Header("Messages")]
    public bool sendMessages = true;
    [ShowIf("sendMessages")]
    public string isPlayerAttackMessage = "playerAttack";
    [ShowIf("sendMessages")]
    public string isPlayerLoseMessage = "playerLose";

    // ѕараметры преследовани€, которые можно настроить в инспекторе
    [Header("Chase Settings")]
    public float maxChaseDistance = 20f; // ћаксимальна€ дистанци€ преследовани€
    public float losePlayerDistance = 25f; // ƒистанци€, при которой игрок считаетс€ потер€нным
    public float attackDistance = 1.5f; // ƒистанци€ дл€ атаки

    [Header("Animations")]
    public bool useAnimations = false;
    [ShowIf("useAnimations")]
    [InfoBox("Universal AI State only work with triggers", EInfoBoxType.Normal)]
    public Animator animator;
    [ShowIf("useAnimations")]
    [AnimatorParam("animator")]
    public string walkAnimation;

    [Header("More subtle settings")]
    public bool subtleSettingsShow = true;
    [ShowIf("subtleSettingsShow")]
    [Tag]
    public string playerTag = "Player";

    //---

    private GameObject player;

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

        player = GameObject.FindWithTag(playerTag);
    }

    public override void Enter()
    {
        // ѕреследование начинаетс€, ставим цель на игрока
        if (player != null)
        {
            navMeshAgent.SetDestination(player.transform.position);

            if (useAnimations)
            {
                animator.SetTrigger(walkAnimation);
            }
        }
    }

    public override void StateUpdate()
    {
        if (player == null)
            return;

        // ѕровер€ем рассто€ние до игрока
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // ≈сли игрок слишком близко, отправл€ем сообщение об атаке
        if (distanceToPlayer <= attackDistance)
        {
            if (sendMessages) exinAI.Message(isPlayerAttackMessage);
            return;
        }

        // ≈сли игрок слишком далеко, прекращаем преследование
        if (distanceToPlayer > maxChaseDistance)
        {
            if (sendMessages) exinAI.Message(isPlayerLoseMessage);
            return;
        }

        // ѕровер€ем, не заблокирован ли путь лучом
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - transform.position;

        // ≈сли между AI и игроком есть преп€тстви€
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, losePlayerDistance))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                if (sendMessages) exinAI.Message(isPlayerLoseMessage);
                return;
            }
        }

        // ≈сли мы не потер€ли игрока и он в пределах рассто€ни€, продолжаем преследование
        navMeshAgent.SetDestination(player.transform.position);
    }

    public override void Exit()
    {
        navMeshAgent.ResetPath();  // ѕрерываем движение при выходе из состо€ни€
    }
}
