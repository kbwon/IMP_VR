using UnityEngine;

public class DishOriginal : MonoBehaviour
{
    public GameObject brokenDish;
    public float breakForce = 3.0f; // 얼마나 세게 부딪혀야 깨질지
    public GameObject dishSound;
    public AudioClip audioclip;

    void Start()
    {
        brokenDish.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > breakForce)
        {
            BreakDish();
        }
    }
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