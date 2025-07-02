// Anexado a AMBOS os prefabs: Moeda e Caixa de Muni��o

using UnityEngine;

// Um 'enum' (enumera��o) � um tipo de dado especial que nos permite criar uma lista de constantes nomeadas.
// � muito mais seguro e leg�vel do que usar n�meros (0 para Moeda, 1 para Muni��o) ou strings.
public enum CollectibleType
{
    Coin,
    Ammo
}

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]

    // Expomos o tipo do colet�vel no Inspector.
    // Assim, no prefab da Moeda, selecionamos 'Coin', e no da Muni��o, selecionamos 'Ammo'.
    [Tooltip("Define o tipo deste colet�vel.")]
    [SerializeField] public CollectibleType type;

    // A quantidade que este colet�vel concede.
    // Ex: 1 para uma moeda, 10 para uma caixa de muni��o.
    [Tooltip("A quantidade que este colet�vel concede ao jogador.")]
    [SerializeField] public int value = 1;
}