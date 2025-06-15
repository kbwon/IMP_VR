using UnityEngine;
using UnityEngine.SceneManagement;

public class winCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(GameManager.Instance.gameObject);
            Destroy(ObjectDetectManager.Instance.gameObject);
            Destroy(MonsterWhereManager.Instance.gameObject);
            Destroy(PlayerInfo.Instance.gameObject);
            Destroy(CanEscapeManager.Instance.gameObject);
            YouWinOrDied.Instance.winOrDie = 1;
            SceneManager.LoadScene("Start_Scene");
        }
    }
}
