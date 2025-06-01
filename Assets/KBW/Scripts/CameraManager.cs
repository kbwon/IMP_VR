using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.SceneView;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public bool isCameraMode = false; 

    [SerializeField] 
    private List<GameObject> hiddenObjects;

    [SerializeField]
    private Transform cameraPos;

    private float maxDistance = 1000f;
    private float viewAngle = 75f;

    public LayerMask monsterLayer;

    private Volume cameraFilter;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        cameraFilter = GetComponentInChildren<Volume>();
    }

    void Update()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            Vector3 toMonster = monster.transform.position - cameraPos.position;
            float angle = Vector3.Angle(cameraPos.forward, toMonster);

            testStatue statue = monster.GetComponent<testStatue>();

            if (angle < viewAngle)
            {
                // 시야각 안에 있을 경우 Raycast로 시야 확보 확인
                Ray ray = new Ray(cameraPos.position, toMonster.normalized);
                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, monsterLayer))
                {
                    if (hit.collider.gameObject == monster)
                    {
                        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
                        statue.isStared = true;
                        continue;
                    }
                }
            }

            // 시야 밖이거나 Ray에 가려졌을 경우
            statue.isStared = false;
            Debug.DrawRay(cameraPos.position, toMonster.normalized * maxDistance, Color.red);
        }
    }

    public void EnterCameraMode()
    {
        isCameraMode = true;
        Debug.Log("isCameraMode: " + isCameraMode);
        cameraFilter.enabled = true;
        foreach (var obj in hiddenObjects)
        {
            SetMode(obj, true);
            /*Transform[] children = obj.GetComponentsInChildren<Transform>();
            foreach (Transform child in obj.transform)
            {
                if (child.CompareTag("NormalMode"))
                    child.gameObject.SetActive(false);
                else if (child.CompareTag("CameraMode"))
                    child.gameObject.SetActive(true);
            }*/
        }            
    }

    public void ExitCameraMode()
    {
        isCameraMode = false;
        Debug.Log("isCameraMode: " + isCameraMode);
        cameraFilter.enabled = false;
        foreach (var obj in hiddenObjects)
        {
            SetMode(obj, false);
            /*Transform[] children = obj.GetComponentsInChildren<Transform>();
            foreach (Transform child in obj.transform)
            {
                if (child.CompareTag("NormalMode"))
                    child.gameObject.SetActive(true);
                else if (child.CompareTag("CameraMode"))
                    child.gameObject.SetActive(false);
            }*/
        }
    }

    private void SetMode(GameObject obj, bool isCameraMode)
    {
        Transform on = obj.transform.Find("On");
        Transform off = obj.transform.Find("Off");

        if (on != null) on.gameObject.SetActive(isCameraMode);
        if (off != null) off.gameObject.SetActive(!isCameraMode);
    }

    public void RegisterHiddenObject(GameObject obj)
    {
        if (!hiddenObjects.Contains(obj))
            hiddenObjects.Add(obj);
    }
}
