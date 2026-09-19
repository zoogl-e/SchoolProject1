using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // local space: movement is calculated relative to which way the player is facing
        Vector3 move = new Vector3(x, 0, z) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.Self);

        float rotation = 0f;
        if (Input.GetKey(KeyCode.Q)) rotation = -1f;
        if (Input.GetKey(KeyCode.E)) rotation = 1f;

        // local space: rotation is applied to the object's local Y axis
        transform.Rotate(Vector3.up * rotation * rotationSpeed * Time.deltaTime, Space.Self);
    }
}