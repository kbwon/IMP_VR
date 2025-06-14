using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.SceneView;

public class CameraManager : MonoBehaviour
{
    // CameraManager.instance.~ 로 접근 가능
    public static CameraManager Instance;

    // 아이템에 카메라 현재 상태 전달하는 변수
    public bool isCameraMode = false; 

    // 아이템 개수 많지 않으면 이쪽에 아이템 등록
    [SerializeField] 
    private List<GameObject> hiddenObjects;

    private float maxDistance = 1000f;

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
        // 여러 몬스터 확인해야 할 때
        /*GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            Vector3 toMonster = monster.transform.position - Camera.main.transform.position;
            float angle = Vector3.Angle(Camera.main.transform.forward, toMonster);

            testStatue statue = monster.GetComponent<testStatue>();

            if (angle < viewAngle)
            {
                // 시야각 안에 있을 경우 Raycast로 시야 확보 확인
                Ray ray = new Ray(Camera.main.transform.position, toMonster.normalized);
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
            Debug.DrawRay(Camera.main.transform.position, toMonster.normalized * maxDistance, Color.red);
        }*/
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider == null) Debug.Log("콜라이더 감지 X");

        if (hit.collider.gameObject.CompareTag("Monster"))
        {
            Debug.Log("플레이어 쪽 컨트롤러 콜리전 판정");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("플레이어 쪽 트리거 판정");
        }
    }
}
