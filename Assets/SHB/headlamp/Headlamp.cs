using UnityEngine;
using Unity.XR.CoreUtils;

public class Headlamp : MonoBehaviour
{
    public Light spotlight;

    private float flickerSeed;
    public float flickerSpeed = 2.5f;  // 값이 클수록 깜빡임이 빠름
    private float flickerAmount = 1f;  // 최대 밝기

    void Start()
    {
        flickerSeed = Random.Range(0f, 100f);  // 각 라이트마다 랜덤성 부여
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(flickerSeed, Time.time * flickerSpeed);
        float intensity = Mathf.Lerp(0f, flickerAmount, noise * noise);  // 비선형으로 흔들림 조정
        spotlight.intensity = intensity;
    }

    public void getHeadlamp()
    {
        PlayerInfo.Instance.items.Add("Headlamp");
        PlayerInfo.Instance.printAll();
        Destroy(gameObject);
    }
}
