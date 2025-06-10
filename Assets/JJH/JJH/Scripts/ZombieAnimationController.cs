using UnityEngine;

public class ZombieAnimationController : MonoBehaviour
{
    private Animator animator;

    // 예: 성공 조건 (예: 플레이어가 가까이 있음 등)
    public bool successCondition = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 예시용: 스페이스바 누르면 성공 조건 체크
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayReactionAnimation();
        }
    }

    void PlayReactionAnimation()
    {
        // 먼저 모든 트리거 초기화 (선택 사항)
        animator.ResetTrigger("SuccessTrigger");
        animator.ResetTrigger("FailTrigger");

        if (successCondition)
        {
            animator.SetTrigger("SuccessTrigger"); // Scream 1
            Debug.Log("✅ 성공 트리거 실행");
        }
        else
        {
            animator.SetTrigger("FailTrigger"); // Scream 0
            Debug.Log("❌ 실패 트리거 실행");
        }
    }
}
