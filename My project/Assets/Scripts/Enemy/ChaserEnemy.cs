// Este script herda de 'Enemy', ganhando todos os seus campos e métodos.
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    // ... (variáveis de dano e knockback) ...
    [Header("Chaser Settings")]
    [SerializeField] private int touchDamage = 15;
    [SerializeField] private float knockbackForceOnPlayer = 8f;

    // A única responsabilidade desta classe é definir seu comportamento de perseguição.
    protected override void ExecuteChaseBehavior(float distanceToPlayer)
    {
        // Se não estivermos já pulando, pulamos em direção ao jogador.
        if (!isHopping)
        {
            StartCoroutine(HopToPosition(playerTarget.position));
        }
    }

    // A lógica de dano por toque é específica do Chaser.
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