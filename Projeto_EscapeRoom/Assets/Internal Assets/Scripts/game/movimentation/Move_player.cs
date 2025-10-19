using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using FMODUnity;
using FMOD.Studio;

public class Move_player : MonoBehaviour
{
    public CharacterController player_actions;
    public float speed = 12f;
    public float gravity = -9.81f;
    public UnityEvent<Vector2> OnMoveInput;

    [Header("Som de Passos (FMOD)")]
    [Tooltip("Evento FMOD para o som de passos")]
    public EventReference footstepEvent;
    public float stepInterval = 0.5f; // Intervalo entre passos em segundos

    private float horizontalInput;
    private float verticalInput;
    private float verticalVelocity;
    private Vector3 velocity;

    private EventInstance footstepInstance;
    private float stepTimer = 0f;
    private bool isMoving = false;

    void Start()
    {
        // Cria a instância do som de passos
        if (!footstepEvent.IsNull)
        {
            footstepInstance = RuntimeManager.CreateInstance(footstepEvent);
        }
    }

    void Update()
    {
        // Movimento no plano horizontal
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        player_actions.Move(move * speed * Time.deltaTime);

        // Verifica se está se movendo no chão
        isMoving = (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f) && player_actions.isGrounded;

        // Sistema de som de passos
        if (isMoving)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }

        // Aplica gravidade
        if (player_actions.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        velocity.y = verticalVelocity;
        player_actions.Move(velocity * Time.deltaTime);
    }

    private void PlayFootstep()
    {
        if (!footstepEvent.IsNull)
        {
            // Toca o som de passo na posição do jogador
            RuntimeManager.PlayOneShot(footstepEvent, transform.position);
        }
    }

    public void OnMoveEvent(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }

    private void OnDestroy()
    {
 
        if (footstepInstance.isValid())
        {
            footstepInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            footstepInstance.release();
        }
    }
}