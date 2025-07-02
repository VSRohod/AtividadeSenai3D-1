// Este script herda de 'Enemy', ganhando todos os seus campos e m�todos.
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    // ... (vari�veis de dano e knockback) ...
    [Header("Chaser Settings")]
    [SerializeField] private int touchDamage = 15;
    [SerializeField] private float knockbackForceOnPlayer = 8f;

    // A �nica responsabilidade desta classe � definir seu comportamento de persegui��o.
    protected override void ExecuteChaseBehavior(float distanceToPlayer)
    {
        // Se n�o estivermos j� pulando, pulamos em dire��o ao jogador.
        if (!isHopping)
        {
            StartCoroutine(HopToPosition(playerTarget.position));
        }
    }

    // A l�gica de dano por toque � espec�fica do Chaser.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(touchDamage);
            }

            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRigidbody.AddForce(knockbackDirection * knockbackForceOnPlayer, ForceMode.Impulse);
            }
        }
    }
}