using UnityEngine;

public class MonsterWhereManager : MonoBehaviour
{
    public static MonsterWhereManager Instance { get; private set; }

    // Monster location information (e.g., room number, etc.)
    public int bookheadWhere;
    public int dollWhere;

    private void Awake()
    {
        // Prevent duplicate instances
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  // Keep this object when changing scenes
    }
}
