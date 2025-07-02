using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserSight : MonoBehaviour
{
    [Header("Laser Settings")]
    [SerializeField] private float maxLaserDistance = 50f;
    [SerializeField] private Color hitEnemyColor = Color.red;
    [SerializeField] private Color defaultColor = Color.green;

    private LineRenderer lineRenderer;
    private Color currentColor; // Vari�vel para guardar a cor atual

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Define a cor inicial
        currentColor = defaultColor;
        SetLaserColor(currentColor);
    }

    void Update()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position); // Ponto inicial � sempre o firePoint

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxLaserDistance))
        {
            // Se atingiu algo, o ponto final � o ponto de colis�o
            lineRenderer.SetPosition(1, hit.point);

            // Verifica se a cor precisa mudar
            if (hit.collider.CompareTag("Enemy"))
            {
                // Se a mira est� no inimigo, mas a cor atual N�O � a de acerto
                if (currentColor != hitEnemyColor)
                {
                    // Muda para a cor de acerto
                    SetLaserColor(hitEnemyColor);
                }
            }
            else
            {
                // Se a mira N�O est� no inimigo, mas a cor atual N�O � a padr�o
                if (currentColor != defaultColor)
                {
                    // Muda para a cor padr�o
                    SetLaserColor(defaultColor);
                }
            }
        }
        else
        {
            // Se n�o atingiu nada, o ponto final � o alcance m�ximo
            lineRenderer.SetPosition(1, transform.position + transform.forward * maxLaserDistance);

            // Garante que a cor seja a padr�o
            if (currentColor != defaultColor)
            {
                SetLaserColor(defaultColor);
            }
        }
    }

    // Fun��o auxiliar para definir a cor e guardar o estado
    void SetLaserColor(Color color)
    {
        currentColor = color;
        lineRenderer.startColor = currentColor;
        lineRenderer.endColor = currentColor;
    }

    public void DisableLaser()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }
}