using UnityEngine;

public class BathAndToiletManager : MonoBehaviour
{
    public GameObject[] toilet;
    public GameObject[] bath;

    public void makeDrainBath(int toiletNumber)
    {
        bath[toiletNumber].GetComponent<Bath>().drainWater();
    }
}
