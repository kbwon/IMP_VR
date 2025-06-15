using UnityEngine;
using UnityEngine.Events;

public class FeedPizzaAtHere : MonoBehaviour
{
    [Header("피자가 불완전할 때 호출할 이벤트")]
    public UnityEvent onAngryEvent;

    [Header("피자가 완성되었을 때 호출할 이벤트")]
    public UnityEvent onSuccessEvent;

    public AudioClip eatingSound;
    public AudioClip failSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        FinalPizza finalPizza = other.GetComponent<FinalPizza>();
        if (finalPizza == null) return;

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
