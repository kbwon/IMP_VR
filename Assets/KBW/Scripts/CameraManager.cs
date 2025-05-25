using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] 
    private List<GameObject> hiddenObjects;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void EnterViewMode()
    {
        foreach (var obj in hiddenObjects) 
            obj.SetActive(true);
    }

    public void ExitViewMode()
    {
        foreach (var obj in hiddenObjects) 
            obj.SetActive(false);
    }

    public void RegisterHiddenObject(GameObject obj)
    {
        if (!hiddenObjects.Contains(obj))
            hiddenObjects.Add(obj);
    }
}
