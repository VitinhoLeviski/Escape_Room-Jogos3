using UnityEngine;
using UnityEngine.InputSystem;

public class Look_around : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform cameraTransform; // Arraste o GameObject da câmera aqui no inspetor

    private float xRotation = 0f;
    private Vector2 mouseInput;

    void Update()
    {
        float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotaciona a câmera para cima/baixo
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotaciona o corpo do player para os lados
        transform.Rotate(Vector3.up * mouseX);
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Chamado via PlayerInput (Unity Events)
    public void OnLookEvent(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
    }
}
