using UnityEngine;

public class Potion : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ghost")) // �ͽŰ� �浹 Ȯ��
        {
            Destroy(gameObject); // ���� ����
        }
    }
}
