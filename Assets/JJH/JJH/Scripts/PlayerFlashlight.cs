using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public enum FlashlightState { Off, On }
    public FlashlightState currentState = FlashlightState.On;
    private Light spotlight;

    void Start()
    {
        spotlight = GetComponent<Light>();
        if (spotlight == null)
            Debug.LogWarning("Could not find Spotlight component.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleFlashlight();
        }
    }

    public void ToggleFlashlight()
    {
        if (currentState == FlashlightState.Off)
        {
            currentState = FlashlightState.On;
            spotlight.enabled = true;
        }
        else
        {
            currentState = FlashlightState.Off;
            spotlight.enabled = false;
        }

        Debug.Log($"🔦 Flashlight state: {currentState}");
    }

    public bool IsEnabled()
    {
        return currentState == FlashlightState.On;
    }

    public float spotAngle = 20f;
    public float range = 15f;
    public Color coneColor = Color.yellow; // Debug cone color

    public Vector3 GetConeOrigin() => transform.position;
    public Vector3 GetConeDirection() => transform.forward.normalized;
    public float GetConeAngle() => spotAngle * 0.5f;
    public float GetConeRange() => range;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = coneColor;

        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;
        float halfAngle = GetConeAngle();

        Vector3 rightDir = Quaternion.Euler(0, halfAngle, 0) * forward;
        Vector3 leftDir = Quaternion.Euler(0, -halfAngle, 0) * forward;

        // Light cone lines
        Gizmos.DrawLine(origin, origin + rightDir * range);
        Gizmos.DrawLine(origin, origin + leftDir * range);

        // Center line
        Gizmos.DrawLine(origin, origin + forward * range);

        // Draw circular arc
        DrawConeArc(origin, forward, halfAngle, range, 20);
    }

    // Function to draw arc of the cone
    void DrawConeArc(Vector3 origin, Vector3 forward, float halfAngle, float radius, int segments)
    {
        float angleStep = (halfAngle * 2) / segments;
        Vector3 prevPoint = origin + Quaternion.Euler(0, -halfAngle, 0) * forward * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = -halfAngle + angleStep * i;
            Vector3 nextPoint = origin + Quaternion.Euler(0, angle, 0) * forward * radius;
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
}
