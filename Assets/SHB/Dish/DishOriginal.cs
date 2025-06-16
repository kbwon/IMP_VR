using UnityEngine;

public class DishOriginal : MonoBehaviour
{
    public GameObject brokenDish; // The broken version of the dish to activate
    public float breakForce = 3.0f; // Required collision force to break the dish
    public GameObject dishSound; // GameObject used to play the breaking sound
    public AudioClip audioclip; // Sound clip to play when the dish breaks

    // Hides the broken dish object at the start
    void Start()
    {
        brokenDish.SetActive(false);
    }

    // Checks collision force and triggers dish break if threshold is exceeded
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > breakForce)
        {
            BreakDish();
        }
    }

    // Replaces the current dish with the broken version and plays a sound
    void BreakDish()
    {
        brokenDish.transform.position = this.transform.position;
        brokenDish.transform.rotation = this.transform.rotation;
        brokenDish.SetActive(true);

        dishSound.transform.position = this.transform.position;
        dishSound.GetComponent<AudioSource>().PlayOneShot(audioclip);
        Destroy(gameObject);
    }
}
