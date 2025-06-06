using UnityEngine;

public class ObjectDetectManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectReactionPair
    {
        public GameObject targetObject;         // 예: keyNumber2
        public GameObject objectToActivate;     // 예: 사라졌을 때 등장할 피자국 등
        [HideInInspector] public bool hasDisappeared = false;
    }

    public ObjectReactionPair[] objectPairs;

    void Start()
    {
        foreach (var pair in objectPairs)
        {
            if (pair.objectToActivate != null)
                pair.objectToActivate.SetActive(false);
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
                    pair.objectToActivate.SetActive(true);
            }
        }
    }
}
