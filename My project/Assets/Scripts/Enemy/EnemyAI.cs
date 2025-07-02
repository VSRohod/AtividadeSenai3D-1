// Classe base abstrata para todos os inimigos.
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening; // Precisamos do DOTween aqui para o pulo da patrulha.
using System.Collections;

[RequireComponent(typeof(NavMeshAgent), typeof(HealthSystem), typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Base Enemy Settings")]
    [SerializeField] protected float detectionRadius = 15f;
    [SerializeField] protected float rotationSpeed = 5f;

    [Header("Patrol Settings")]
    [SerializeField] protected Transform[] patrolPoints; // Um array de pontos para patrulhar.
    [SerializeField] protected float patrolWaitTime = 3f; // Tempo que o inimigo espera em cada ponto.

    [Header("Hop Settings")]
    [SerializeField] protected float hopPower = 0.3f;
    [SerializeField] protected float hopDuration = 0.5f;

    // 'protected' permite que as classes filhas acessem estas variáveis.
    protected NavMeshAgent agent;
    protected Transform playerTarget;
    protected HealthSystem healthSystem;
    protected Animator animator;
    protected int currentPatrolIndex = 0;
    protected bool isWaitingAtPatrolPoint = false;
    protected bool isHopping = false;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        healthSystem = GetComponent<HealthSystem>();
        animator = GetComponent<Animator>();

        agent.updatePosition = false; // O DOTween controla a posição.
        agent.updateRotation = false; // Nossa função FaceTarget controla a rotação.

        // A lógica para encontrar o jogador permanece.
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) { playerTarget = playerObject.transform; }
        else { Debug.LogError("Inimigo não conseguiu encontrar o jogador!"); }
    }

    protected virtual void Update()
    {
        if (healthSystem.IsDead || playerTarget == null)
        {
            if (agent.enabled) agent.isStopped = true;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        // Se o jogador for detectado, o comportamento de perseguição/ataque é ativado.
        if (distanceToPlayer <= detectionRadius)
        {
            agent.isStopped = false; // Garante que o agente está ativo para calcular o caminho.
            FaceTarget();
            ExecuteChaseBehavior(distanceToPlayer);
        }
        // Senão, o inimigo executa sua rotina de patrulha.
        else
        {
            Patrol();
        }

        // Sincroniza a posição do agente (que não se move) com a do nosso objeto (movido pelo DOTween).
        if (!agent.isStopped)
            agent.nextPosition = transform.position;
    }

    protected void Patrol()
    {
        // Se não houver pontos de patrulha, não faz nada.
        if (patrolPoints.Length == 0) return;

        // Se o inimigo não estiver esperando em um ponto e não estiver pulando...
        if (!isWaitingAtPatrolPoint && !isHopping)
        {
            // ...e se ele chegou perto o suficiente do seu destino atual...
            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 1f)
            {
                // ...ele começa a esperar.
                StartCoroutine(WaitAtPatrolPoint());
            }
            // Senão, ele se move para o ponto de patrulha.
            else
            {
                StartCoroutine(HopToPosition(patrolPoints[currentPatrolIndex].position));
            }
        }
    }

    private IEnumerator WaitAtPatrolPoint()
    {
        isWaitingAtPatrolPoint = true;
        // Espera o tempo definido.
        yield return new WaitForSeconds(patrolWaitTime);
        // Escolhe o próximo ponto de patrulha (de forma circular).
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        isWaitingAtPatrolPoint = false;
    }

    // A lógica de pulo agora está na classe base para ser usada tanto na patrulha quanto na perseguição.
    protected IEnumerator HopToPosition(Vector3 targetPosition)
    {
        isHopping = true;

        // Antes de pular, vira para a direção do pulo.
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.DORotateQuaternion(lookRotation, 0.2f); // Rotação suave com DOTween.

        yield return new WaitForSeconds(0.2f); // Pequena pausa para a rotação terminar.

        animator.SetTrigger("doHop"); // Ativa a animação de pulo.

        transform.DOJump(targetPosition, hopPower, 1, hopDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => { isHopping = false; });
    }

    protected void FaceTarget()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    protected abstract void ExecuteChaseBehavior(float distanceToPlayer);
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}