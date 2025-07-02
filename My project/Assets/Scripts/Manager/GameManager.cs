// Este script ser� colocado em um GameObject vazio na cena.
using UnityEngine;
using UnityEngine.SceneManagement; // Necess�rio para recarregar a cena.

public class GameManager : MonoBehaviour
{
    // --- PADR�O SINGLETON ---
    // 'static' significa que esta vari�vel pertence � CLASSE, n�o a uma inst�ncia espec�fica.
    // S� pode haver uma inst�ncia do GameManager em todo o jogo.
    public static GameManager instance;

    // A refer�ncia para o painel de Game Over, que vamos arrastar no Inspector.
    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;

    // Propriedade p�blica para que outros scripts possam ler, mas apenas este pode alterar.
    public int EnemiesKilled { get; private set; }

    // O m�todo Awake() � chamado antes de qualquer Start(). � o lugar perfeito para configurar singletons.
    private void Awake()
    {
        // Se j� existe uma inst�ncia e n�o sou eu, me destruo.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Sen�o, eu me torno a inst�ncia.
            instance = this;
        }
    }

    private void Start()
    {
        // Garante que o painel de game over esteja desligado no in�cio.
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
        // Poder�amos tamb�m pausar o jogo aqui: Time.timeScale = 0;
    }

    // Fun��o p�blica para ser chamada pelo bot�o de "Tentar Novamente".
    public void RestartGame()
    {
        // Recarrega a cena atual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}