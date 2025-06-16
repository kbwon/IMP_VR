using UnityEngine;

public class DoorM : MonoBehaviour
{
    public bool outDoor = false;
    public bool inDoor = false;

    public MonsterDoor monsterDoor;
    public Transform outdoorTeleport;
    public Transform indoorTeleport;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MonsterAI>() != null)  //3. 닿은 게 몬스터인가?
        {


            if (outDoor == true)  //내가 아웃도어인가?
            {
                other.gameObject.transform.position = outdoorTeleport.position;
                // 방 안으로 텔레포트시킴
                monsterDoor.outdoorOn();
            }

            else if (inDoor == true)  //내가 인도어인가?
            {
                other.gameObject.transform.position = indoorTeleport.position;
                //방 밖으로 텔레포트시킴
                monsterDoor.indoorOn();
            }
        }
    }
}
