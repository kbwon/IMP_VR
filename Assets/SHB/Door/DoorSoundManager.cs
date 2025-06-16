using UnityEngine;

public class DoorSoundManager : MonoBehaviour
{
    public Sounds canOpen; // Sound group for successful door open
    public Sounds cannotOpen; // Sound group for failed door attempt

    // Plays a random sound from the 'can open' sound group
    public void canOpenSoundPlay()
    {
        canOpen.PlayRandomSound();
    }

    // Plays a random sound from the 'cannot open' sound group
    public void cannotOpenSoundPlay()
    {
        cannotOpen.PlayRandomSound();
    }
}
