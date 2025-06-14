using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CheckSocket : MonoBehaviour
{
    public XRSocketInteractor socketInteractor;
    public bool hasTriggered = false;

    void Update()
    {
        if (hasTriggered == true) return;
        if (socketInteractor == null) return;
        if (socketInteractor.hasSelection)
        {
            IXRSelectInteractable selected = socketInteractor.GetOldestInteractableSelected();

            if (selected != null && selected.transform.CompareTag("Headlamp"))
            {
                Debug.Log("HeadLamp가 소켓에 꽂혀 있음!");
                hasTriggered = true;

                // HeadLamp를 Player의 자식으로 변경
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    selected.transform.SetParent(player.transform);
                }
                else
                {
                    Debug.LogWarning("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
                }
            }
        }
    }
}
