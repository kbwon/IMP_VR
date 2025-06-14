using UnityEngine;

public class CanEscapeManager : MonoBehaviour
{
    public static CanEscapeManager Instance { get; private set; }

    private void Awake()
    {
        // 중복 인스턴스 방지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시 유지하려면
    }

    public bool canEscape = false;

    void Update()
    {
        if (canEscape == false) return;

        if (PlayerInfo.Instance.playerWhere != 0)
        {
            canEscape = false;
            if (PlayerInfo.Instance.chasedByBookhead == true) GameManager.Instance.ToggleBookheadBehavior(false);
            if (PlayerInfo.Instance.chasedByDoll == true) GameManager.Instance.ToggleDollBehavior(false);
        }
    }
}
