using JetBrains.Annotations;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public bool isStared;

    [SerializeField]
    private float stareDistance = 5f;
    [SerializeField]
    private float maxIgnoreTime = 5f;
    [SerializeField]
    private float viewAngle = 75f;
    [SerializeField]
    private AudioClip stareSound;
    [SerializeField]
    private GameObject redLight;

    private float ignoreTimer = 0f;
    private GameObject eyes;
    private Transform cameraPos;
    private AudioSource audioSource;

    void Start()
    {
        eyes = transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
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
                if (redLight != null) redLight.SetActive(true);

                ignoreTimer = 0f;

                audioSource.Stop();
            }
            else
            {
                isStared = false;

                if (eyes != null) eyes.SetActive(false);
                if (redLight != null) redLight.SetActive(false);

                if (!audioSource.isPlaying)
                {
                    audioSource.clip = stareSound;
                    audioSource.Play();
                }

                ignoreTimer += Time.deltaTime;
                if (ignoreTimer >= maxIgnoreTime)
                {
                    Debug.Log("You died");
                    PlayerInfo.Instance.isDead = true;
                    PlayerInfo.Instance.whenPlayerDied();
                    ignoreTimer = 0f;
                }
            }
        }
        else
        {
            isStared = false;
            if (eyes != null) eyes.SetActive(false);
            if (redLight != null) redLight.SetActive(false);
            audioSource.Stop();
        }
    }

    //�� �Լ��� Ư�� �ൿ�� �� ��ó�� ���� �̵� ��Ű�� ��
    public void StatueTeleport(Vector3 pos)
    {
        transform.position = pos;   
    }
}