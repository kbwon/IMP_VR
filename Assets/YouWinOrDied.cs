using UnityEngine;

public class YouWinOrDied : MonoBehaviour
{
    public int winOrDie = 0;  //0=아무것도 아님, 1=이김, 2=죽음
    public static YouWinOrDied Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지하고 싶다면
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }

        if (PlayerInfo.Instance.gameObject != null) Destroy(PlayerInfo.Instance.gameObject);
    }
}
