using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    float verticalRotation = 0;
    private CharacterController controller; 

    void Awake()
    {
        controller = GetComponent<CharacterController>();

    }

    void Update()
    {

        PlayerInputMovement();
        PlayerInputMouseLook();
    }

    void PlayerInputMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D o Flechas Izq/Der
        float verticalInput = Input.GetAxis("Vertical");     // W/S o Flechas Arr/Abajo

        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;

        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void PlayerInputMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
