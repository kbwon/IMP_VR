using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionMenu;

    public void StartBtn()
    {
        SceneManager.LoadScene("SHB Ver3");
    }

    public void ToOption()
    {
        startMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void ToReturn()
    {
        startMenu.SetActive(true);
        optionMenu.SetActive(false);
    }
}
