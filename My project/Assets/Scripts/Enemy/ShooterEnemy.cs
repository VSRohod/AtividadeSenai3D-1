// Este script tamb�m herda de 'Enemy'.
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [Header("Shooter Settings")]
    [SerializeField] private float attackRadius = 10f; // Raio em que ele para e atira.
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;

    private float nextFireTime = 0f;

    // Implementa��o do comportamento para o Shooter.
    protected override void ExecuteChaseBehavior(float distanceToPlayer)
    {
        // Se o jogador est� longe, o Shooter persegue (pulando).
        if (distanceToPlayer > attackRadius)
        {
            if (!isHopping)
            {
                StartCoroutine(HopToPosition(playerTarget.position));
            }
        }
        // Se o jogador est� perto, o Shooter para e atira.
        else
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (projectilePrefab != null && firePoint != null)
            {
                Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            }
        }
    }

    // Sobrescrevemos o OnDrawGizmos da classe base para adicionar o raio de ataque.
    protected override void OnDrawGizmosSelected()
    {
        // base.OnDrawGizmosSelected() chama o m�todo da classe pai, desenhando o raio amarelo.
        base.OnDrawGizmosSelected();

        // E ent�o adicionamos nosso pr�prio gizmo vermelho.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}