using UnityEngine;

public class BreakingDish : MonoBehaviour
{
    public GameObject brokenPrefab;
    public GameObject colliderOfAny;
    public float breakForce = 3.0f; // 얼마나 세게 부딪혀야 깨질지

    void Start()
    {
        colliderOfAny.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > breakForce)
        {
            BreakDish();
        }
    }

    public void colliderOfAnyTurnOn()
    {
        colliderOfAny.SetActive(true);
    }
    void BreakDish()
    {
        Instantiate(brokenPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
