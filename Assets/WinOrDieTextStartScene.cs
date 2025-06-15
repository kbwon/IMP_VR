using UnityEngine;

public class WinOrDieTextStartScene : MonoBehaviour
{
    public GameObject dieText;
    public GameObject winText;

    void Update()
    {
        if (YouWinOrDied.Instance.winOrDie == 1)
        {
            dieText.SetActive(false);
            winText.SetActive(true);
        }

        else if (YouWinOrDied.Instance.winOrDie == 2)
        {
            dieText.SetActive(true);
            winText.SetActive(false);
        }

        else
        {
            dieText.SetActive(false);
            winText.SetActive(false);
        }
    }
}
