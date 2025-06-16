using UnityEngine;

public class BathAndToiletManager : MonoBehaviour
{
    public GameObject[] toilet; // Array of toilet objects
    public GameObject[] bath; // Array of corresponding bathtub objects

    public GameObject key; // Key that appears after draining enough baths
    public Transform[] keytransform; // Spawn positions for the key based on toilet index

    public int drainCount = 0; // Tracks how many toilets have been flushed

    // Hides the key at the start
    void Start()
    {
        key.SetActive(false);
    }

    // Called when a specific toilet is flushed to drain its corresponding bath
    public void makeDrainBath(int toiletNumber)
    {
        bath[toiletNumber].GetComponent<Bath>().drainWater();
        drainCount++;
        Debug.Log("드레인카운트: " + drainCount);

        // When three toilets are flushed, spawn the key at the corresponding position
        if (drainCount == 3)
        {
            key.transform.position = keytransform[toiletNumber].position;
            key.SetActive(true);
            drainCount = 999; // Prevent further activation
        }
    }
}
