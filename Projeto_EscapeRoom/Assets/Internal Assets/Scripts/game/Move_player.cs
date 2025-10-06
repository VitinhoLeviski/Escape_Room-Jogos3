using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Move_player : MonoBehaviour
{

    public CharacterController player_actions;
    public float speed = 12f;
    public float gravity = -9.81f;

    public UnityEvent<Vector2> OnMoveInput;

    private float horizontalInput;
    private float verticalInput;
    private float verticalVelocity;
    private Vector3 velocity;

    void Start()
    {

    }

    void Update()
    {
        // Movimento no plano horizontal
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        player_actions.Move(move * speed * Time.deltaTime);

        // Aplica gravidade
        if (player_actions.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f; // pequena força para manter no chão
        else
            verticalVelocity += gravity * Time.deltaTime;

        velocity.y = verticalVelocity;
        player_actions.Move(velocity * Time.deltaTime);
    }

    public void OnMoveEvent(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }
}
