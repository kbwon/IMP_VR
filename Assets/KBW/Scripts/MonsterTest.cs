using UnityEngine;

public class MonsterTest : MonoBehaviour
{
    public float detectionRadius = 1.5f;
    public LayerMask playerLayer;

    void Update()
    {
        /*Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);

        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    Debug.Log("Physics overlap sphere 판정");
                    // 여기서 사망 처리 호출
                }
            }
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("몬스터 쪽 트리거 판정");
        }
    }
}
