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
        // 예시: 스페이스바로 성공 실행, F 키로 실패 실행
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaySuccessReaction();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayFailReaction();
        }
    }

    // ✅ 성공 애니메이션 실행
    public void PlaySuccessReaction()
    {
        ResetAllTriggers();
        animator.SetTrigger("SuccessTrigger");
        Debug.Log("✅ 성공 애니메이션 실행");
    }

    // ❌ 실패 애니메이션 실행
    public void PlayFailReaction()
    {
        ResetAllTriggers();
        animator.SetTrigger("FailTrigger");
        Debug.Log("❌ 실패 애니메이션 실행");
    }

    // 🔄 트리거 초기화 (선택사항)
    private void ResetAllTriggers()
    {
        animator.ResetTrigger("SuccessTrigger");
        animator.ResetTrigger("FailTrigger");
    }
}
