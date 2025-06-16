using System.Collections;
using UnityEngine;

public class DisappearOnWeaponHit : MonoBehaviour
{
    [Header("Object that disappears 3 seconds after being hit")]
    public GameObject targetObject;

    [Header("Tag that triggers the effect")]
    public string weaponTag = "Weapon";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(weaponTag))
        {
            StartCoroutine(RemoveAfterDelay(3f));
        }
    }

    private IEnumerator RemoveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (targetObject != null)
            Destroy(targetObject);
    }
}
