using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Example: Press Space to play success, F key to play failure
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySuccessReaction();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayFailReaction();
        }
    }

    // ✅ Play success animation
    public void PlaySuccessReaction()
    {
        ResetAllTriggers();
        animator.SetTrigger("SuccessTrigger");
        Debug.Log("✅ Success animation triggered");
    }

    // ❌ Play failure animation
    public void PlayFailReaction()
    {
        ResetAllTriggers();
        animator.SetTrigger("FailTrigger");
        Debug.Log("❌ Failure animation triggered");
    }

    // 🔄 Reset triggers (optional)
    private void ResetAllTriggers()
    {
        animator.ResetTrigger("SuccessTrigger");
        animator.ResetTrigger("FailTrigger");
    }
}
