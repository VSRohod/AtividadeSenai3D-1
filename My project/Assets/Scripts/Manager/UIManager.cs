// Este script será colocado no GameObject do Canvas.
using UnityEngine;
using TMPro; // Namespace para usar TextMeshPro.
using UnityEngine.UI; // Namespace para usar componentes de UI como Slider e Image.

public class UIManager : MonoBehaviour
{
    // --- PADRÃO SINGLETON ---
    public static UIManager instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private Slider playerHealthSlider; // Usaremos um Slider para a vida do jogador.

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); }
        else { instance = this; }
    }

    public void UpdateEnemiesKilledText(int count)
    {
        if (enemiesKilledText != null)
        {
            enemiesKilledText.text = "ELIMINADOS: " + count;
        }
    }

    public void UpdatePlayerHealth(int currentHealth, int maxHealth)
    {
        if (playerHealthSlider != null)
        {
            // O valor de um slider vai de 0 a 1.
            // Para calcular a porcentagem de vida, dividimos a vida atual pela máxima.
            // Precisamos converter um dos valores para float para garantir uma divisão com casas decimais.
            playerHealthSlider.value = (float)currentHealth / maxHealth;
        }
    }
}