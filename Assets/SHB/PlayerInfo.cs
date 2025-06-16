using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance { get; private set; }

    public GameObject hud;
    public List<int> keyNumberList = new();
    public List<string> items = new();
    public bool camcoder = false;
    public int playerWhere = 0; // 0 = hallway
    public bool isPlayerChased = false;
    public bool chasedByBookhead = false;
    public bool chasedByDoll = false;
    public bool isDead = false;
    public GameObject dieSphere;

    void Start()
    {
        hud.SetActive(false);
    }

    private void Awake()
    {
        // Ensure singleton instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes

        keyNumberList.Add(99);
        keyNumberList.Add(1);
        dieSphere.SetActive(false);
    }

    public void printAll()
    {
        Debug.Log("Items: " + string.Join(", ", items));
        Debug.Log("Key Numbers: " + string.Join(", ", keyNumberList));
    }

    public void printPlayerWhere()
    {
        Debug.Log("Player in " + playerWhere);
    }

    public void whenPlayerDied()
    {
        dieSphere.SetActive(true);
        // Start coroutine to fade in death screen shader
        StartCoroutine(FadeInDieSphere());

        // Destroy remaining objects
        Destroy(GameManager.Instance.gameObject);
        Destroy(ObjectDetectManager.Instance.gameObject);
        Destroy(MonsterWhereManager.Instance.gameObject);
        Destroy(CanEscapeManager.Instance.gameObject);
        YouWinOrDied.Instance.winOrDie = 2;

        // Load start scene after 2 seconds
        Invoke(nameof(LoadStartScene), 2f);
    }

    private IEnumerator FadeInDieSphere()
    {
        // dieSphere must have a MeshRenderer or SpriteRenderer
        Renderer renderer = dieSphere.GetComponent<Renderer>();
        Material mat = renderer.material;

        Color color = mat.color;
        color.a = 0f;
        mat.color = color;

        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            color.a = Mathf.Lerp(0f, 1f, t);
            mat.color = color;

            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        mat.color = color;
    }

    private void LoadStartScene()
    {
        SceneManager.LoadScene("Start_Scene");
    }
}
