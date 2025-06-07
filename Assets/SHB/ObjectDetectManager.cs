using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDetectManager : MonoBehaviour
{
    public static ObjectDetectManager Instance { get; private set; }
    public GameObject sitDoll;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class ObjectReactionPair
    {
        public GameObject targetObject;

        public GameObject[] objectToActivate;

        public UnityEvent onDisappearAction; // ✅ 여기 추가
        [HideInInspector] public bool hasDisappeared = false;
    }

    public ObjectReactionPair[] objectPairs;

    void Start()
    {
        foreach (var pair in objectPairs)
        {
            if (pair.objectToActivate != null)
            {
                foreach (var obj in pair.objectToActivate)
                {
                    if (obj != null)
                        obj.SetActive(false);
                }
            }
        }
    }

    void Update()
    {
        foreach (var pair in objectPairs)
        {
            if (pair.targetObject == null && !pair.hasDisappeared)
            {
                pair.hasDisappeared = true;

                // 기존 오브젝트 활성화
                if (pair.objectToActivate != null)
                {
                    foreach (var obj in pair.objectToActivate)
                    {
                        if (obj != null)
                            obj.SetActive(true);
                    }
                }

                // ✅ 새로운: 함수 실행
                pair.onDisappearAction?.Invoke();
            }
        }
    }


    public void whenIGotKeyNumber5()
    {
        PlayerInfo.Instance.isPlayerChased = true;
        GameManager.Instance.dollMonsterObject.transform.position = sitDoll.transform.position;
        GameManager.Instance.dollMonsterObject.transform.rotation = sitDoll.transform.rotation;
        sitDoll.SetActive(false);

        StartCoroutine(MoveZ(GameManager.Instance.dollMonsterObject.transform, 3.01f, 0.5f));
        
    }

    private IEnumerator MoveZ(Transform target, float deltaZ, float duration)
    {
        Vector3 startPos = target.position;
        Vector3 endPos = startPos + new Vector3(0f, 0f, deltaZ);
        float elapsed = 0f;
        GameManager.Instance.dollMonsterObject.SetActive(true);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            target.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        target.position = endPos; // 마지막 보정
        GameManager.Instance.dollMonsterObject.SetActive(false);
        GameManager.Instance.ToggleDollBehavior(true);
    }
}
