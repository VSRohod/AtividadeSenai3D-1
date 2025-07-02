// Anexado ao GameObject do Jogador
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;

    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 30; // Muni��o m�xima que o jogador pode carregar.
    private int currentAmmo;                 // Muni��o atual.

    private float nextFireTime = 0f;

    void Start()
    {
        // Come�a o jogo com a muni��o cheia.
        currentAmmo = maxAmmo;
        // Futuramente, aqui chamar�amos a UI para mostrar a muni��o inicial.
        // UIManager.instance.UpdateAmmoCount(currentAmmo, maxAmmo);
    }

    void Update()
    {
        // Adicionamos uma nova condi��o: s� podemos atirar se tivermos muni��o.
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
        Debug.Log("Muni��o: " + currentAmmo);

        // AQUI EST� A CHAVE:
        // Posi��o: Usamos a posi��o exata do firePoint.
        // Rota��o: Usamos a rota��o exata do firePoint, que agora est� perfeitamente alinhada com o jogador.
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // A l�gica do proj�til cuidar� do resto.
    }

    // Este m�todo P�BLICO pode ser chamado por outros scripts (como o PlayerCollector)
    // para dar muni��o ao jogador.
    public void AddAmmo(int amount)
    {
        // Adicionamos a muni��o, mas garantimos que n�o ultrapasse o m�ximo.
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        Debug.Log("Muni��o adicionada! Total: " + currentAmmo); // Para depura��o.

        // Futuramente, atualizar a UI.
        // UIManager.instance.UpdateAmmoCount(currentAmmo, maxAmmo);
    }
}