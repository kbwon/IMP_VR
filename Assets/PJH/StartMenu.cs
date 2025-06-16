using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionMenu;

    // Called when the Start button is pressed
    // Loads the main game scene named "SHB Ver3"
    public void StartBtn()
    {
        SceneManager.LoadScene("SHB Ver3");
    }

    // Switches from Start Menu to Options Menu
    public void ToOption()
    {
        startMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    // Switches back to Start Menu
    public void ToReturn()
    {
        startMenu.SetActive(true);
        optionMenu.SetActive(false);
    }
}
