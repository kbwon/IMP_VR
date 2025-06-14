using UnityEngine;

public class MonsterWhereManager : MonoBehaviour
{
    public static MonsterWhereManager Instance { get; private set; }

    // 몬스터 위치 정보 (예: 방 번호 등)
    public int bookheadWhere;
    public int dollWhere;

    private void Awake()
    {
        // 중복 방지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  // 씬 넘어가도 유지하고 싶을 경우
    }
}
