using UnityEngine;

public class Locker : MonoBehaviour
{
    // 몇 가지 알아야 하는 것
    // 1. 플레이어가 쫓기고 있는가?
    // 2. 플레이어와 몬스터가 같은 방에 있는가?
    // 3. 어떤 몬스터에게 쫓기고 있는가?

    public bool isPlayerChased = false;
    public bool isPlayerAndMonsterSameRoom = false;
    public LockerInDoor lockerInDoor;
    public LockerOutDoor lockerOutDoor;
    public int roomNumber;

    public bool isIn = false;

    [Header("아래는 락커에 숨었을 때 필요한 것들")]
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public AudioClip footstepSound;
    public AudioClip lockerOpenSound;
    public AudioSource audiosource;

    void Start()
    {
        lockerInDoor.gameObject.SetActive(false);
    }

    public void locker()
    {
        //락커 안에 있을 때 절대 못 움직이게 하기. update를 이용하여 계속 transform을 고정하면 되지 않을까?

        isPlayerChased = PlayerInfo.Instance.isPlayerChased;

        if (isPlayerChased == false) Debug.Log("아무 일도 안 일어남");

        else if (isPlayerChased == true)
        {
            //if(PlayerInfo.Instance.playerWhere == 몬스터 위치) isPlayerAndMonsterSameRoom = true;
            //else isPlayerAndMonsterSameRoom = false;

            if (isPlayerAndMonsterSameRoom == true)
            {
                Debug.Log("죽는 애니메이션 만들어서 그거 쓸 거임");
                Debug.Log("생각해보니 그럼 어떤 몬스터한테 쫓기고 있는지도 알아야 하겠네.");
            }

            else if (isPlayerAndMonsterSameRoom == false)
            {
                Debug.Log("몬스터가 방에 들어왔다 나가는 거 소리 애니메이션 만들어서 재생할 거임");
                Debug.Log("또한 이 경우에는 애니메이션 재생이 끝날 때까지 인터렉트 다 꺼버릴거임. 갑자기 중간에 나가버리면 안 되니까.");
            }
        }
    }

    public void updateDoor()
    {
        if (isIn == false)
        {
            lockerInDoor.gameObject.SetActive(false);
            lockerOutDoor.gameObject.SetActive(true);
        }

        else
        {
            lockerInDoor.gameObject.SetActive(true);
            lockerOutDoor.gameObject.SetActive(false);
        }
    }
}
