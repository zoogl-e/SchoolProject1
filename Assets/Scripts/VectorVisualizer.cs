using TMPro;
using UnityEngine;

public class VectorVisualizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform target;

    [Header("Visualization")]
    [SerializeField] private LineRenderer lineRenderer;

    [Header("UI - Lab 3 Elements")]
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI directionText;
    [SerializeField] private TextMeshProUGUI normalizedText;
    [SerializeField] private TextMeshProUGUI angleText;

    [Header("UI - Lab 4 Elements")]
    [SerializeField] private TextMeshProUGUI deltaText;
    [SerializeField] private TextMeshProUGUI horizontalDistanceText;
    [SerializeField] private TextMeshProUGUI bearingText;
    [SerializeField] private TextMeshProUGUI elevationText;

    [Header("Optional Challenge - Compass")]
    [SerializeField] private RectTransform compassArrow;

    private void Update()
    {
        if (player == null || target == null)
            return;

        // world space: Direction vector between player and target positions
        Vector3 delta = target.position - player.position;

        // world space: Component differences along each world coordinate axis
        float deltaX = delta.x;
        float deltaY = delta.y;
        float deltaZ = delta.z;

        // world space: 3D Euclidean distance between player and target coordinates
        float distance = Vector3.Distance(player.position, target.position);

        // world space: Horizontal distance across the XZ ground plane (ignores Y height difference)
        float horizontalDistance = Mathf.Sqrt(deltaX * deltaX + deltaZ * deltaZ);

        // world space: Horizontal bearing angle in radians relative to World Forward (Z axis)
        float bearingRadians = Mathf.Atan2(deltaX, deltaZ);

        // world space: Converting horizontal bearing angle from radians to degrees
        float bearingDegrees = bearingRadians * Mathf.Rad2Deg;

        // world space: Elevation pitch angle in radians between horizontal distance and vertical height difference
        float elevationRadians = Mathf.Atan2(deltaY, horizontalDistance);

        // world space: Converting elevation angle from radians to degrees
        float elevationDegrees = elevationRadians * Mathf.Rad2Deg;

        // lab 3 calculations
        // world space: normalized direction vector (magnitude = 1)
        Vector3 normalizedDirection = delta.normalized;
        // world space: angle between player's forward vector and direction to target
        float angle = Vector3.Angle(player.forward, delta);

        // world space: update 3d line renderer connecting player and target
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, target.position);
        }

        // update lab 4 UI readouts
        if (deltaText != null)
        {
            deltaText.text = $"Δx:{deltaX:F2} Δy:{deltaY:F2} Δz:{deltaZ:F2}";
        }

        if (horizontalDistanceText != null)
        {
            horizontalDistanceText.text = $"Horizontal Distance: {horizontalDistance:F2}";
        }

        if (bearingText != null)
        {
            bearingText.text = $"Bearing: {bearingDegrees:F1}°";
        }

        if (elevationText != null)
        {
            elevationText.text = $"Elevation: {elevationDegrees:F1}°";
        }

        // compass indicator
        if (compassArrow != null)
        {
            // local space: signed angle between player heading and target direction on horizontal plane
            Vector3 playerForwardXZ = new Vector3(player.forward.x, 0f, player.forward.z);
            Vector3 deltaXZ = new Vector3(delta.x, 0f, delta.z);
            float compassAngle = Vector3.SignedAngle(playerForwardXZ, deltaXZ, Vector3.up);

            // local space: rotate UI arrow around Z so it points toward the target
            compassArrow.localRotation = Quaternion.Euler(0f, 0f, -compassAngle);
        }

        // lab 3 ui
        if (distanceText != null)
        {
            distanceText.text = $"Distance: {distance:F2}";
        }

        if (directionText != null)
        {
            directionText.text = $"Direction: {delta}";
        }

        if (normalizedText != null)
        {
            normalizedText.text = $"Normalized: {normalizedDirection}";
        }

        if (angleText != null)
        {
            angleText.text = $"Angle To Target: {angle:F1}°";
        }
    }

    private void OnDrawGizmos()
    {
        if (player == null || target == null)
            return;

        // world space: direct line connecting player to target (total 3d distance)
        Gizmos.color = Color.green;
        Gizmos.DrawLine(player.position, target.position);

        // world space: projection of target onto player's horizontal plane (same Y as player)
        Vector3 horizontalTarget = new Vector3(
            target.position.x,
            player.position.y,
            target.position.z
        );

        // world space: horizontal projection line (horizontal distance)
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(player.position, horizontalTarget);

        // world space: vertical component line (vertical difference) completing the right triangle
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(horizontalTarget, target.position);
    }
}