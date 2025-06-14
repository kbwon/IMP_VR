using JetBrains.Annotations;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public bool isStared = false;

    [SerializeField]
    private float stareDistance = 5f;
    [SerializeField]
    private float maxIgnoreTime = 5f;
    [SerializeField]
    private float viewAngle = 75f;

    private float ignoreTimer = 0f;
    private GameObject eyes;
    private Transform cameraPos;

    void Start()
    {
        eyes = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        cameraPos = Camera.main.transform;

        Vector3 statueDir = transform.position - cameraPos.position;
        float angle = Vector3.Angle(Camera.main.transform.forward, statueDir);

        float playerDistance = Vector3.Distance(transform.position, cameraPos.position);
        bool isInRange = playerDistance <= stareDistance;

        if(isInRange)
        {
            if (angle <= viewAngle)
            {
                isStared = true;
                Vector3 lookDir = cameraPos.position - transform.position;

                Quaternion baseRotation = Quaternion.LookRotation(-lookDir);
                Quaternion yOffset = Quaternion.Euler(0, 60f, 0);
                transform.rotation = baseRotation * yOffset;

                if (eyes != null) eyes.SetActive(true);

                ignoreTimer = 0f;
            }
            else
            {
                isStared = false;

                if (eyes != null) eyes.SetActive(false);

                ignoreTimer += Time.deltaTime;
                if (ignoreTimer >= maxIgnoreTime)
                {
                    Debug.Log("You died");
                    PlayerInfo.Instance.isDead = true;
                    ignoreTimer = 0f;
                }
            }
        }
        else
        {
            isStared = false;
            if (eyes != null) eyes.SetActive(false);
        }
    }

    //이 함수로 특정 행동할 시 근처로 동상 이동 시키면 됨
    public void StatueTeleport(Vector3 pos)
    {
        transform.position = pos;   
    }
}