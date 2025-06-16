using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ObjectDetectManager : MonoBehaviour
{
    public static ObjectDetectManager Instance { get; private set; }
    public GameObject sitDoll;
    public Transform spawnHeadlightTransform;
    public GameObject headLight;

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
        public GameObject targetObject; // Object to monitor

        public GameObject[] objectToActivate; // Objects to activate after target disappears

        public UnityEvent onDisappearAction; // Optional function to call when target disappears
        [HideInInspector] public bool hasDisappeared = false;
    }

    public ObjectReactionPair[] objectPairs;

    void Start()
    {
        // Deactivate all linked objects at the beginning
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
        // Check for disappearance of target objects
        foreach (var pair in objectPairs)
        {
            if (pair.targetObject == null && !pair.hasDisappeared)
            {
                pair.hasDisappeared = true;

                // Activate linked objects
                if (pair.objectToActivate != null)
                {
                    foreach (var obj in pair.objectToActivate)
                    {
                        if (obj != null)
                            obj.SetActive(true);
                    }
                }

                // Invoke optional custom event
                pair.onDisappearAction?.Invoke();
            }
        }
    }

    public void whenIGotKeyNumber5()
    {
        PlayerInfo.Instance.isPlayerChased = true;
        PlayerInfo.Instance.chasedByDoll = true;
        GameManager.Instance.dollMonsterObject.transform.position = sitDoll.transform.position;
        GameManager.Instance.dollMonsterObject.transform.rotation = sitDoll.transform.rotation;
        GameManager.Instance.dollMonsterObject.GetComponent<NavMeshAgent>().speed = 1.0f;
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

        target.position = endPos; // Final position correction
        GameManager.Instance.dollMonsterObject.SetActive(false);
        GameManager.Instance.ToggleDollBehavior(true);
    }

    public void RemoveAllLightsInScene()
    {
        Light[] allLights = Object.FindObjectsByType<Light>(FindObjectsSortMode.None);

        Instantiate(headLight, spawnHeadlightTransform.position, Quaternion.identity);

        foreach (Light light in allLights)
        {
            var extra = light.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalLightData>();
            if (extra != null)
                Destroy(extra); // Remove URP-dependent component

            Destroy(light); // Remove Light
        }

        // Clear baked lighting
        LightmapSettings.lightmaps = new LightmapData[0];
        LightmapSettings.lightProbes = null;

        Debug.Log($"Removed {allLights.Length} lights and cleared baked lighting.");
    }
}
