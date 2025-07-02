// Anexado ao GameObject do Jogador
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 10f; // Quão rápido o personagem se vira

    private Rigidbody rb;
    private Vector3 moveInput;
    private Transform cameraTransform; // Referência ao transform da câmera principal

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Guardamos a referência do transform da câmera para uso contínuo.
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("Câmera principal não encontrada! Certifique-se de que sua câmera tem a tag 'MainCamera'.");
        }
    }

    void Update()
    {
        // A leitura do input permanece a mesma.
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Criamos um vetor de direção baseado no input.
        moveInput = new Vector3(horizontalInput, 0f, verticalInput).normalized;
    }

    void FixedUpdate()
    {
        // Se houver algum input de movimento...
        if (moveInput.magnitude >= 0.1f)
        {
            // --- CÁLCULO DA DIREÇÃO RELATIVA À CÂMERA ---
            // Pega a direção 'para frente' da câmera e achata no plano Y=0.
            Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            // Pega a direção 'para a direita' da câmera.
            Vector3 cameraRight = cameraTransform.right;

            // Calcula a direção final do movimento somando as direções da câmera ponderadas pelo input.
            // Ex: Se apertar W, move na direção 'cameraForward'. Se apertar D, move na direção 'cameraRight'.
            Vector3 moveDirection = (cameraForward * moveInput.z + cameraRight * moveInput.x);

            // --- APLICAÇÃO DO MOVIMENTO E ROTAÇÃO ---
            // Move o Rigidbody na direção calculada.
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            // Cria uma rotação que "olha" para a direção do movimento.
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Interpola suavemente da rotação atual para a rotação alvo.
            // Quaternion.Slerp é o 'Lerp' para rotações, garantindo uma virada suave.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}