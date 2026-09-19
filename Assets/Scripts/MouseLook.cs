using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private Transform playerBody;
    
    private float xRotation = 0f;

    void Start()
    {
        // locks cursor to screen center during gameplay
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // local space: tilts camera up/down without affecting whole body
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (playerBody != null)
        {
            // local space: rotates entire player body left/right
            playerBody.Rotate(Vector3.up * mouseX, Space.Self);
        }
    }
}