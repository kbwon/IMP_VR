using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    [Header("씬에 배치된 몬스터 오브젝트")]
    public GameObject dollMonsterObject;
    public GameObject bookheadMonsterObject;
    public bool dollActive = false;
    public bool bookheadActive = false;

    private MonsterAI dollMonster;
    private MonsterAI bookheadMonster;

    void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // 몬스터 오브젝트를 비활성화 (게임 시작 시)
        if (dollMonsterObject != null)
            dollMonsterObject.SetActive(false);
        if (bookheadMonsterObject != null)
            bookheadMonsterObject.SetActive(false);

        // 컴포넌트 참조 저장
        if (dollMonsterObject != null)
            dollMonster = dollMonsterObject.GetComponent<MonsterAI>();
        if (bookheadMonsterObject != null)
            bookheadMonster = bookheadMonsterObject.GetComponent<MonsterAI>();

        // 내부 로직도 비활성화 호출
        if (dollMonster != null)
            dollMonster.DisableChaseAndAttack();
        if (bookheadMonster != null)
            bookheadMonster.DisableChaseAndAttack();
    }

    // ✅ 기존 메서드 이름 유지하면서 몬스터 오브젝트 활성/비활성 포함
    public void ToggleDollBehavior(bool on)
    {
        if (dollMonsterObject != null)
        {
            dollMonsterObject.SetActive(on);
            if (dollMonster != null)
                dollMonster.SetChaseAndAttackEnabled(on);
        }
        dollActive = true;
    }

    public void ToggleBookheadBehavior(bool on)
    {
        if (bookheadMonsterObject != null)
        {
            bookheadMonsterObject.SetActive(on);
            if (bookheadMonster != null)
                bookheadMonster.SetChaseAndAttackEnabled(on);
        }
        bookheadActive = true;
    }
}
