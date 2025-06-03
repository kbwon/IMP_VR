using UnityEngine;

public class BreakingDish : MonoBehaviour
{
    public GameObject intactDish;
    public GameObject brokenDish;
    public GameObject colliderOfAny;
    public float breakForce = 3.0f;

    void Start()
    {
        colliderOfAny.SetActive(false);
        intactDish.SetActive(true);
        brokenDish.SetActive(false);
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
        intactDish.SetActive(false);
        Destroy(intactDish);
        brokenDish.SetActive(true);
    }
}
