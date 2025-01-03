using UnityEngine;

public class Potion : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost")) // 귀신과 충돌 확인
        {
            Destroy(gameObject); // 포션 제거
        }
    }
}
