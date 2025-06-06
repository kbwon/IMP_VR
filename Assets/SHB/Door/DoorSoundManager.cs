using UnityEngine;

public class DoorSoundManager : MonoBehaviour
{
    public Sounds canOpen;
    public Sounds cannotOpen;

    public void canOpenSoundPlay()
    {
        canOpen.PlayRandomSound();
        Debug.Log("canopen Sound");
    }

    public void cannotOpenSoundPlay()
    {
        cannotOpen.PlayRandomSound();
        Debug.Log("cannot open sound");
    }
}
