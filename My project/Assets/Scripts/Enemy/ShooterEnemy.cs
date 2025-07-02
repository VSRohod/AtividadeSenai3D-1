// Este script também herda de 'Enemy'.
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [Header("Shooter Settings")]
    [SerializeField] private float attackRadius = 10f; // Raio em que ele para e atira.
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;

    private float nextFireTime = 0f;

    // Implementação do comportamento para o Shooter.
    protected override void ExecuteChaseBehavior(float distanceToPlayer)
    {
        // Se o jogador está longe, o Shooter persegue (pulando).
        if (distanceToPlayer > attackRadius)
        {
            if (!isHopping)
            {
                StartCoroutine(HopToPosition(playerTarget.position));
            }
        }
        // Se o jogador está perto, o Shooter para e atira.
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
        // base.OnDrawGizmosSelected() chama o método da classe pai, desenhando o raio amarelo.
        base.OnDrawGizmosSelected();

        // E então adicionamos nosso próprio gizmo vermelho.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}