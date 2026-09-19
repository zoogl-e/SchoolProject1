using UnityEngine;

public class FloatingCube : MonoBehaviour
{
    [SerializeField] private float amplitude = 1.0f; // how high it floats
    [SerializeField] private float frequency = 2.0f; // how fast it floats
    
    private Vector3 startPosition;

    void Start()
    {
        // world space: records exact starting position
        startPosition = transform.position;
    }

    void Update()
    {
        // world space: modifies world position based on time
        Vector3 newPosition = startPosition;
        newPosition.y += Mathf.Sin(Time.time * frequency) * amplitude;
        
        transform.position = newPosition;
    }
}