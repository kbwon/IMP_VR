using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    [Header("Monster objects placed in the scene")]
    public GameObject dollMonsterObject;
    public GameObject bookheadMonsterObject;
    public bool dollActive = false;
    public bool bookheadActive = false;

    private MonsterAI dollMonster;
    private MonsterAI bookheadMonster;

    void Awake()
    {
        // Set singleton instance
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
        // Deactivate monster objects at the start of the game
        if (dollMonsterObject != null)
            dollMonsterObject.SetActive(false);
        if (bookheadMonsterObject != null)
            bookheadMonsterObject.SetActive(false);

        // Store component references
        if (dollMonsterObject != null)
            dollMonster = dollMonsterObject.GetComponent<MonsterAI>();
        if (bookheadMonsterObject != null)
            bookheadMonster = bookheadMonsterObject.GetComponent<MonsterAI>();

        // Also disable internal logic
        if (dollMonster != null)
            dollMonster.DisableChaseAndAttack();
        if (bookheadMonster != null)
            bookheadMonster.DisableChaseAndAttack();
    }

    // ✅ Keep original method names, also toggle object activation
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
