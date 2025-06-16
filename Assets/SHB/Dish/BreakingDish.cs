using UnityEngine;

public class BreakingDish : MonoBehaviour
{
    public GameObject intactDish; // The original, unbroken dish
    public GameObject brokenDish; // The broken version of the dish
    public GameObject colliderOfAny; // Optional collider to activate later
    public float breakForce = 3.0f; // Minimum collision force required to break the dish

    // Initializes the dish to be intact and disables others
    void Start()
    {
        colliderOfAny.SetActive(false);
        intactDish.SetActive(true);
        brokenDish.SetActive(false);
    }

    // Checks collision force and breaks the dish if threshold is exceeded
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > breakForce)
        {
            BreakDish();
        }
    }

    // Public method to activate an external collider (called externally)
    public void colliderOfAnyTurnOn()
    {
        colliderOfAny.SetActive(true);
    }

    // Handles the actual breaking: hides and destroys the intact dish, shows broken one
    void BreakDish()
    {
        intactDish.SetActive(false);
        Destroy(intactDish);
        brokenDish.SetActive(true);
    }
}
