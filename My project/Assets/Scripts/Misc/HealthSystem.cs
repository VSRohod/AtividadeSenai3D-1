// Este script pode ser anexado a qualquer GameObject que precise de vida (Jogador, Inimigo, etc.)
using UnityEngine;
using System.Collections; // Necessário para usar Coroutines

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("UI (Para Inimigos)")]
    [SerializeField] private GameObject healthBarPrefab; // Campo para o prefab da barra de vida.
    private EnemyHealthBar healthBar; // Referência para o script da barra de vida instanciada.

    [Header("Feedback de Dano")]
    // Referência ao Renderer do objeto para podermos mudar sua cor.
    [SerializeField] private Renderer objectRenderer;
    [SerializeField] private Color damageColor = Color.red; // A cor que o objeto piscará.
    [SerializeField] private float flashDuration = 0.1f; // Duração do piscar.
    private Color originalColor; // Para guardar a cor original do material.

    // Acessível publicamente para outros scripts saberem se o objeto está vivo.
    public bool IsDead { get; private set; }

    void Start()
    {
        // Inicializa a vida no máximo.
        currentHealth = maxHealth;
        IsDead = false;

        // Guarda a cor original do material do objeto.
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }

        if (gameObject.CompareTag("Player"))
        {
            UIManager.instance.UpdatePlayerHealth(currentHealth, maxHealth);
        }

        if (healthBarPrefab != null && !gameObject.CompareTag("Player"))
        {
            GameObject hb = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
            healthBar = hb.GetComponent<EnemyHealthBar>();
            // Associa a barra de vida a este inimigo específico.
             healthBar.transform.SetParent(this.transform);
        }
    }

    // Este é um método PÚBLICO que qualquer outro script pode chamar para causar dano.
    // Ex: um projétil ou a colisão de um inimigo chamará este método.
    public void TakeDamage(int damageAmount)
    {
        // Se o objeto já está morto, não faz mais nada.
        if (IsDead) return;

        // Subtrai o dano da vida atual.
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " tomou " + damageAmount + " de dano. Vida restante: " + currentHealth);

        // Inicia o feedback visual.
        StartCoroutine(FlashFeedback());

        if (gameObject.CompareTag("Player"))
        {
            UIManager.instance.UpdatePlayerHealth(currentHealth, maxHealth);
        }

        // Se tiver uma barra de vida, atualiza-a.
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        // Verifica se o objeto morreu.
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    // Uma Coroutine para o efeito de piscar.
    // Coroutines nos permitem criar lógicas que pausam e continuam ao longo de vários frames.
    private IEnumerator FlashFeedback()
    {
        if (objectRenderer == null) yield break; // Se não houver renderer, sai da coroutine.

        // Muda a cor do material para a cor de dano.
        objectRenderer.material.color = damageColor;

        // 'yield return new WaitForSeconds()' pausa a execução da coroutine pela duração especificada.
        yield return new WaitForSeconds(flashDuration);

        // Restaura a cor original.
        objectRenderer.material.color = originalColor;
    }

    private void Die()
    {
        IsDead = true;
        Debug.Log(gameObject.name + " morreu.");

        // --- NOVA LÓGICA DE COMUNICAÇÃO ---
        // Verificamos se o objeto que morreu tem a tag "Inimigo".
        if (gameObject.CompareTag("Enemy"))
        {
            // Se for, avisamos ao GameManager que um inimigo foi eliminado.
            GameManager.instance.OnEnemyKilled();
        }
        // Verificamos se o objeto que morreu tem a tag "Player".
        else if (gameObject.CompareTag("Player"))
        {
            // Se for, avisamos ao GameManager que o jogador morreu.
            GameManager.instance.OnPlayerDied();
        }
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }

        gameObject.SetActive(false);
    }
}