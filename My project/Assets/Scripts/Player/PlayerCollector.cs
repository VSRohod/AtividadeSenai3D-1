// Anexado ao GameObject do Jogador
using UnityEngine;

// Agora este script precisa da referência ao PlayerAttack para poder adicionar munição.
[RequireComponent(typeof(PlayerAttack))]
public class PlayerCollector : MonoBehaviour
{
    private int coinCount = 0;
    private PlayerAttack playerAttack; // Referência ao script de ataque.

    void Start()
    {
        // Pegamos a referência ao script de ataque no mesmo GameObject.
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Tentamos pegar o componente Collectible no objeto com o qual colidimos.
        Collectible collectible = other.gameObject.GetComponent<Collectible>();

        // Se o objeto realmente for um coletável (o componente existe)...
        if (collectible != null)
        {
            // ...usamos um switch-case para decidir o que fazer com base no seu tipo.
            // Switch-case é uma forma limpa de lidar com múltiplas condições baseadas em um enum.
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
                    // Se for munição...
                    // Chamamos o método público do nosso script de ataque para adicionar a munição.
                    playerAttack.AddAmmo(collectible.value);
                    break;
            }

            // Após processar o coletável, nós o destruímos.
            Destroy(other.gameObject);
        }
    }
}