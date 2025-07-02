// Este script será colocado em um GameObject vazio na cena.
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para recarregar a cena.

public class GameManager : MonoBehaviour
{
    // --- PADRÃO SINGLETON ---
    // 'static' significa que esta variável pertence à CLASSE, não a uma instância específica.
    // Só pode haver uma instância do GameManager em todo o jogo.
    public static GameManager instance;

    // A referência para o painel de Game Over, que vamos arrastar no Inspector.
    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;

    // Propriedade pública para que outros scripts possam ler, mas apenas este pode alterar.
    public int EnemiesKilled { get; private set; }

    // O método Awake() é chamado antes de qualquer Start(). É o lugar perfeito para configurar singletons.
    private void Awake()
    {
        // Se já existe uma instância e não sou eu, me destruo.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Senão, eu me torno a instância.
            instance = this;
        }
    }

    private void Start()
    {
        // Garante que o painel de game over esteja desligado no início.
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void OnEnemyKilled()
    {
        EnemiesKilled++;
        Debug.Log("Inimigos Eliminados: " + EnemiesKilled);

        // Avisa o UIManager para atualizar o placar.
        UIManager.instance.UpdateEnemiesKilledText(EnemiesKilled);
    }

    public void OnPlayerDied()
    {
        Debug.Log("GAME OVER");
        // Ativa o painel de game over.
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        // Poderíamos também pausar o jogo aqui: Time.timeScale = 0;
    }

    // Função pública para ser chamada pelo botão de "Tentar Novamente".
    public void RestartGame()
    {
        // Recarrega a cena atual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}