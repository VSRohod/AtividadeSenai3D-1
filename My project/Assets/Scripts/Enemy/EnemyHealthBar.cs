// Este script será colocado no Canvas da barra de vida do inimigo.
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    private Transform cameraTransform;

    void Start()
    {
    }


    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
    }
}