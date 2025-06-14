using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonsterAI : MonoBehaviour
{
    public enum MonsterType
    {
        Doll,
        BookheadMonster
    }

    [Header("몬스터 타입 설정")]
    public MonsterType monsterType = MonsterType.BookheadMonster;

    [Header("각 타입별 추적/공격 허용 여부")]
    public bool dollCanChaseAndAttack = false;
    public bool bookheadCanChaseAndAttack = true;

    [Header("공통 설정")]
    public Transform player;
    public float chaseDistance = 8f;
    public float attackDistance = 2f;
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    public float attackDuration = 1.2f;

    [Header("점프 스케어 설정")]
    public float jumpScareDistance = 6f;
    public float jumpScareSpeed = 10f;
    public AudioClip jumpScareSound;

    [Header("주기적 사운드 설정")]
    public AudioClip periodicGrowlSound;
    public float growlInterval = 5f;

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;

    private float timer;
    private float growlTimer = 0f;
    private bool isAttacking = false;
    private bool jumpScareTriggered = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timer = wanderTimer;
    }

    void Update()
    {
        bool isEnabled = (monsterType == MonsterType.Doll)
                            ? dollCanChaseAndAttack
                            : bookheadCanChaseAndAttack;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 1️⃣ 점프 스케어 트리거
        if (isEnabled && !jumpScareTriggered && distanceToPlayer <= jumpScareDistance)
        {
            StartCoroutine(TriggerJumpScare());
            return;
        }

        // 2️⃣ 일반 공격
        if (isEnabled && distanceToPlayer <= attackDistance && !isAttacking)
        {
            agent.SetDestination(transform.position);
            animator.speed = 1f;
            SetAnimation(false, true);
            StartCoroutine(EndAttackAfter(attackDuration));
        }
        else if (isEnabled && distanceToPlayer <= chaseDistance && !isAttacking)
        {
            agent.SetDestination(player.position);
            animator.speed = 3f;
            SetAnimation(true, false);
        }
        else
        {
            if (!isAttacking)
            {
                timer += Time.deltaTime;
                if (timer >= wanderTimer)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0f;
                }

                bool isMoving = agent.velocity.magnitude > 0.1f;
                animator.speed = 1f;
                SetAnimation(isMoving, false);
            }
        }

        // 3️⃣ 주기적 사운드 재생
        growlTimer += Time.deltaTime;
        if (growlTimer >= growlInterval)
        {
            if (periodicGrowlSound && audioSource)
                audioSource.PlayOneShot(periodicGrowlSound);

            growlTimer = 0f;
        }
    }

    void SetAnimation(bool isWalking, bool isAttacking)
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isAttacking", isAttacking);
    }

    IEnumerator EndAttackAfter(float seconds)
    {
        isAttacking = true;
        yield return new WaitForSeconds(seconds);
        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    IEnumerator TriggerJumpScare()
    {
        jumpScareTriggered = true;
        isAttacking = true;

        agent.speed = jumpScareSpeed;
        agent.SetDestination(player.position);

        SetAnimation(false, true); // 기존 공격 애니메이션 사용

        if (jumpScareSound && audioSource)
            audioSource.PlayOneShot(jumpScareSound);

        yield return new WaitForSeconds(attackDuration);

        animator.SetBool("isAttacking", false);
        agent.speed = 3f; // 기본 추적 속도로 복귀
        isAttacking = false;
        jumpScareTriggered = false;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    public void SetChaseAndAttackEnabled(bool enabled)
    {
        if (monsterType == MonsterType.Doll)
            dollCanChaseAndAttack = enabled;
        else if (monsterType == MonsterType.BookheadMonster)
            bookheadCanChaseAndAttack = enabled;
    }

    public void EnableChaseAndAttack()
    {
        SetChaseAndAttackEnabled(true);
    }

    public void DisableChaseAndAttack()
    {
        SetChaseAndAttackEnabled(false);
    }
}
