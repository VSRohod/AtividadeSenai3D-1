// Anexado ao GameObject do Jogador
using UnityEngine;

// Agora este script precisa da refer�ncia ao PlayerAttack para poder adicionar muni��o.
[RequireComponent(typeof(PlayerAttack))]
public class PlayerCollector : MonoBehaviour
{
    private int coinCount = 0;
    private PlayerAttack playerAttack; // Refer�ncia ao script de ataque.

    void Start()
    {
        // Pegamos a refer�ncia ao script de ataque no mesmo GameObject.
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Tentamos pegar o componente Collectible no objeto com o qual colidimos.
        Collectible collectible = other.gameObject.GetComponent<Collectible>();

        // Se o objeto realmente for um colet�vel (o componente existe)...
        if (collectible != null)
        {
            // ...usamos um switch-case para decidir o que fazer com base no seu tipo.
            // Switch-case � uma forma limpa de lidar com m�ltiplas condi��es baseadas em um enum.
            switch (collectible.type)
            {
                case CollectibleType.Coin:
                    // Se for uma moeda...
                    coinCount += collectible.value;
                    Debug.Log("Moedas: " + coinCount);
                    // Futuramente, atualizar a UI de moedas.
                    // UIManager.instance.UpdateCoinCount(coinCount);
                    break;

                case CollectibleType.Ammo:
                    // Se for muni��o...
                    // Chamamos o m�todo p�blico do nosso script de ataque para adicionar a muni��o.
                    playerAttack.AddAmmo(collectible.value);
                    break;
            }

            // Ap�s processar o colet�vel, n�s o destru�mos.
            Destroy(other.gameObject);
        }
    }
}