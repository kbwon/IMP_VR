using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("✅ Mouse click - attack trigger activated");
            animator.SetTrigger("attackTrigger"); // Trigger activation
        }
    }
}
