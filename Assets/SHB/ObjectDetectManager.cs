using UnityEngine;

public class ObjectDetectManager : MonoBehaviour
{
    public static ObjectDetectManager Instance { get; private set; }
    private void Awake()
    {


        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시 유지하려면

    }

    [System.Serializable]
    public class ObjectReactionPair
    {
        public GameObject targetObject;
        public GameObject[] objectToActivate;
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

                if (pair.objectToActivate != null)
                {
                    foreach (var obj in pair.objectToActivate)
                    {
                        if (obj != null)
                            obj.SetActive(true);
                    }
                }
            }
        }
    }
}
