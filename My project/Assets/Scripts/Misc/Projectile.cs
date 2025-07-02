// Anexado ao Prefab do Proj�til
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 10; // Dano que este proj�til causa.
    [SerializeField] private float knockbackForce = 5f; // For�a do empurr�o.

    // ... o m�todo Start() permanece o mesmo ...
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Tenta pegar o componente HealthSystem no objeto com o qual colidimos.
        HealthSystem targetHealth = collision.gameObject.GetComponent<HealthSystem>();

        // Se o objeto tiver um sistema de vida...
        if (targetHealth != null)
        {
            // ...chama o m�todo p�blico para causar dano.
            targetHealth.TakeDamage(damage);
        }

        // Tenta pegar o Rigidbody do objeto atingido para aplicar o knockback.
        Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (targetRigidbody != null)
        {
            // Calcula a dire��o do knockback (do ponto de colis�o para longe do proj�til).
            Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
            // Adiciona uma for�a de impulso no alvo.
            targetRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }

        // Destroi o proj�til ap�s o impacto.
        Destroy(gameObject);
    }
}