using UnityEngine;

public class BookheadMover : MonoBehaviour
{
    [Header("움직일 오브젝트")]
    public GameObject targetObject; // 이동시킬 대상

    [Header("이동 경로")]
    public Transform startPoint;  // 처음위치
    public Transform endPoint;    // 나중위치

    public float moveSpeed = 2f;

    private bool isMoving = false;
    private Transform targetTransform;

    void Start()
    {
        // 타겟이 지정되어 있으면 비활성화
        if (targetObject != null)
        {
            targetTransform = targetObject.transform;
            targetObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("⚠️ targetObject가 비어 있습니다!");
        }
    }

    void Update()
    {
        // ✅ 스페이스바 입력 감지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateAndMove();
        }

        if (isMoving && targetTransform != null && endPoint != null)
        {
            // 이동 처리
            targetTransform.position = Vector3.MoveTowards(targetTransform.position, endPoint.position, moveSpeed * Time.deltaTime);

            // 도착하면 비활성화
            if (Vector3.Distance(targetTransform.position, endPoint.position) < 0.01f)
            {
                isMoving = false;
                targetObject.SetActive(false); // 도착 후 꺼주기
            }
        }
    }

    // 외부에서 호출: 시작 위치로 설정하고 이동 시작
    public void ActivateAndMove()
    {
        if (targetObject != null && startPoint != null)
        {
            targetTransform.position = startPoint.position;
            targetObject.SetActive(true); // 활성화
            isMoving = true;
        }
    }

    // 외부에서 강제로 끄고 싶을 때
    public void DeactivateImmediately()
    {
        if (targetObject != null)
        {
            isMoving = false;
            targetObject.SetActive(false);
        }
    }
}
