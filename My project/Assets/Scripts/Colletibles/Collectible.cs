// Anexado a AMBOS os prefabs: Moeda e Caixa de Munição

using UnityEngine;

// Um 'enum' (enumeração) é um tipo de dado especial que nos permite criar uma lista de constantes nomeadas.
// É muito mais seguro e legível do que usar números (0 para Moeda, 1 para Munição) ou strings.
public enum CollectibleType
{
    Coin,
    Ammo
}

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]

    // Expomos o tipo do coletável no Inspector.
    // Assim, no prefab da Moeda, selecionamos 'Coin', e no da Munição, selecionamos 'Ammo'.
    [Tooltip("Define o tipo deste coletável.")]
    [SerializeField] public CollectibleType type;

    // A quantidade que este coletável concede.
    // Ex: 1 para uma moeda, 10 para uma caixa de munição.
    [Tooltip("A quantidade que este coletável concede ao jogador.")]
    [SerializeField] public int value = 1;
}