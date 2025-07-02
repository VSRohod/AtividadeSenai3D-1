// Anexado ao GameObject do Jogador
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;

    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 30; // Munição máxima que o jogador pode carregar.
    private int currentAmmo;                 // Munição atual.

    private float nextFireTime = 0f;

    void Start()
    {
        // Começa o jogo com a munição cheia.
        currentAmmo = maxAmmo;
        // Futuramente, aqui chamaríamos a UI para mostrar a munição inicial.
        // UIManager.instance.UpdateAmmoCount(currentAmmo, maxAmmo);
    }

    void Update()
    {
        // Adicionamos uma nova condição: só podemos atirar se tivermos munição.
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && currentAmmo > 0)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;

        currentAmmo--;
        Debug.Log("Munição: " + currentAmmo);

        // AQUI ESTÁ A CHAVE:
        // Posição: Usamos a posição exata do firePoint.
        // Rotação: Usamos a rotação exata do firePoint, que agora está perfeitamente alinhada com o jogador.
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // A lógica do projétil cuidará do resto.
    }

    // Este método PÚBLICO pode ser chamado por outros scripts (como o PlayerCollector)
    // para dar munição ao jogador.
    public void AddAmmo(int amount)
    {
        // Adicionamos a munição, mas garantimos que não ultrapasse o máximo.
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        Debug.Log("Munição adicionada! Total: " + currentAmmo); // Para depuração.

        // Futuramente, atualizar a UI.
        // UIManager.instance.UpdateAmmoCount(currentAmmo, maxAmmo);
    }
}