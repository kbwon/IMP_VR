using UnityEngine;

public class CanEscapeManager : MonoBehaviour
{
    // Singleton instance
    public static CanEscapeManager Instance { get; private set; }

    private void Awake()
    {
        // Prevent duplicate instances of this manager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep this object when switching scenes
    }

    public bool canEscape = false;

    void Update()
    {
        // Do nothing if escaping is not currently allowed
        if (canEscape == false) return;

        // If the player has moved into a room, disable chase behaviors and block further escape
        if (PlayerInfo.Instance.playerWhere != 0)
        {
            canEscape = false;

            if (PlayerInfo.Instance.chasedByBookhead == true)
                GameManager.Instance.ToggleBookheadBehavior(false);

            if (PlayerInfo.Instance.chasedByDoll == true)
                GameManager.Instance.ToggleDollBehavior(false);
        }
    }
}
