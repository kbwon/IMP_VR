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

    [Header("Monster Type Setting")]
    public MonsterType monsterType = MonsterType.BookheadMonster;

    [Header("Enable chase/attack per type")]
    public bool dollCanChaseAndAttack = false;
    public bool bookheadCanChaseAndAttack = true;

    [Header("Common Settings")]
    public Transform player;
    public float chaseDistance = 8f;       // Start chasing when within this distance
    public float attackDistance = 2f;      // Attack range
    public float wanderRadius = 10f;       // Patrol radius
    public float wanderTimer = 5f;         // Patrol interval
    public float attackDuration = 1.2f;    // Duration of attack animation

    [Header("Sound Settings")]
    public AudioClip attackSound;              // Sound to play when attacking
    public AudioClip periodicGrowlSound;       // Growl sound played periodically
    public float growlInterval = 5f;           // Growl interval in seconds

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;

    private float timer;
    private float growlTimer = 0f;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timer = wanderTimer;
    }

    void Update()
    {
        // 1) Check whether this monster is allowed to chase/attack
        bool isEnabled = (monsterType == MonsterType.Doll)
                            ? dollCanChaseAndAttack
                            : bookheadCanChaseAndAttack;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 2) Chase or attack only if enabled and within range
        if (isEnabled && distanceToPlayer <= attackDistance && !isAttacking)
        {
            // Attack state
            agent.SetDestination(transform.position);
            animator.speed = 1f;
            SetAnimation(false, true);
            StartCoroutine(EndAttackAfter(attackDuration));
        }
        else if (isEnabled && distanceToPlayer <= chaseDistance && !isAttacking)
        {
            // Chase state
            agent.SetDestination(player.position);
            animator.speed = 3f;
            SetAnimation(true, false);
        }
        else
        {
            // Patrol state
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

        // 3) Periodic growl sound
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

        // 🔊 Play attack sound
        if (attackSound && audioSource)
            audioSource.PlayOneShot(attackSound);

        yield return new WaitForSeconds(seconds);

        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    /// <summary>
    /// Enables or disables chase/attack based on the monster's type.
    /// </summary>
    public void SetChaseAndAttackEnabled(bool enabled)
    {
        if (monsterType == MonsterType.Doll)
            dollCanChaseAndAttack = enabled;
        else if (monsterType == MonsterType.BookheadMonster)
            bookheadCanChaseAndAttack = enabled;
    }

    /// <summary>
    /// Enables chase/attack for this monster.
    /// </summary>
    public void EnableChaseAndAttack()
    {
        SetChaseAndAttackEnabled(true);
    }

    /// <summary>
    /// Disables chase/attack for this monster.
    /// </summary>
    public void DisableChaseAndAttack()
    {
        SetChaseAndAttackEnabled(false);
    }
}
