// Anexado ao GameObject do Jogador
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 10f; // Qu�o r�pido o personagem se vira

    private Rigidbody rb;
    private Vector3 moveInput;
    private Transform cameraTransform; // Refer�ncia ao transform da c�mera principal

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Guardamos a refer�ncia do transform da c�mera para uso cont�nuo.
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("C�mera principal n�o encontrada! Certifique-se de que sua c�mera tem a tag 'MainCamera'.");
        }
    }

    void Update()
    {
        // A leitura do input permanece a mesma.
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Criamos um vetor de dire��o baseado no input.
        moveInput = new Vector3(horizontalInput, 0f, verticalInput).normalized;
    }

    void FixedUpdate()
    {
        // Se houver algum input de movimento...
        if (moveInput.magnitude >= 0.1f)
        {
            // --- C�LCULO DA DIRE��O RELATIVA � C�MERA ---
            // Pega a dire��o 'para frente' da c�mera e achata no plano Y=0.
            Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            // Pega a dire��o 'para a direita' da c�mera.
            Vector3 cameraRight = cameraTransform.right;

            // Calcula a dire��o final do movimento somando as dire��es da c�mera ponderadas pelo input.
            // Ex: Se apertar W, move na dire��o 'cameraForward'. Se apertar D, move na dire��o 'cameraRight'.
            Vector3 moveDirection = (cameraForward * moveInput.z + cameraRight * moveInput.x);

            // --- APLICA��O DO MOVIMENTO E ROTA��O ---
            // Move o Rigidbody na dire��o calculada.
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Cria uma rota��o que "olha" para a dire��o do movimento.
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Interpola suavemente da rota��o atual para a rota��o alvo.
            // Quaternion.Slerp � o 'Lerp' para rota��es, garantindo uma virada suave.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}