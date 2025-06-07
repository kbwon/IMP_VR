using UnityEngine;

public class BathAndToiletManager : MonoBehaviour
{
    public GameObject[] toilet;
    public GameObject[] bath;

    public GameObject key;
    public Transform[] keytransform;

    public int drainCount = 0;

    void Start()
    {
        key.SetActive(false);
    }

    public void makeDrainBath(int toiletNumber)
    {
        bath[toiletNumber].GetComponent<Bath>().drainWater();
        drainCount++;
        Debug.Log("드레인카운트: " + drainCount);

        if (drainCount == 3)
        {
            key.transform.position = keytransform[toiletNumber].position;
            key.SetActive(true);
            drainCount = 999;
        }
    }
}
