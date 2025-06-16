using UnityEngine;
using UnityEngine.Events;

public class FeedPizzaAtHere : MonoBehaviour
{
    // (Event triggered when the pizza is incomplete)
    [Header("피자가 불완전할 때 호출할 이벤트")]
    public UnityEvent onAngryEvent;

    // (Event triggered when the pizza is complete)
    [Header("피자가 완성되었을 때 호출할 이벤트")]
    public UnityEvent onSuccessEvent;

    public AudioClip eatingSound; // Sound played when pizza is accepted
    public AudioClip failSound; // Sound played when pizza is rejected

    private AudioSource audioSource; // Audio source component reference

    // Gets the AudioSource component on start
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Triggered when a pizza enters the collider
    private void OnTriggerEnter(Collider other)
    {
        FinalPizza finalPizza = other.GetComponent<FinalPizza>();
        if (finalPizza == null) return;

        // If the pizza is missing required parts (dough or hands), it's a failure
        if (!finalPizza.dough || !finalPizza.hands)
        {
            audioSource.clip = failSound;
            audioSource.Play();
            Debug.Log("피자에 햄을 넣으면 화냄");
            PlayerInfo.Instance.isDead = true;
            onAngryEvent?.Invoke();
            PlayerInfo.Instance.whenPlayerDied();
        }
        else
        {
            audioSource.clip = eatingSound;
            audioSource.Play();
            onSuccessEvent?.Invoke();
        }

        Destroy(other.gameObject);
    }
}
