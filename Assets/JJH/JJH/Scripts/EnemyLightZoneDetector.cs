using UnityEngine;

public class EnemyLightZoneDetector : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chasing
    }

    [Header("Detection Range Settings")]
    public float detectionRadius = 7f;

    [Header("Player Reference")]
    public Transform playerTransform;
    public PlayerFlashlight flashlight;

    public EnemyState currentState = EnemyState.Idle;
    private float loseSightTimer = 0f;
    private float loseSightDelay = 5f;

    private void Update()
    {
        if (flashlight == null || playerTransform == null) return;

        bool flashlightOn = flashlight.IsEnabled();
        bool inRange = Vector3.Distance(transform.position, playerTransform.position) <= detectionRadius;

        if (flashlightOn && inRange)
        {
            loseSightTimer = 0f;
            if (currentState != EnemyState.Chasing)
            {
                Debug.Log("🔵 Enemy B: Start chasing");
                currentState = EnemyState.Chasing;
                GameManager.Instance.ToggleBookheadBehavior(true);
            }

            loseSightTimer = 0f;
        }
        else
        {
            if (currentState == EnemyState.Chasing)
            {
                loseSightTimer += Time.deltaTime;

                if (loseSightTimer >= loseSightDelay)
                {
                    Debug.Log("🛑 Enemy B: Stop chasing");
                    GameManager.Instance.ToggleBookheadBehavior(false);
                    currentState = EnemyState.Idle;
                    loseSightTimer = 0f;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
