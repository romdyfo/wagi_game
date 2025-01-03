using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform player; // Player Transform
    public float speed = 3f; // �ͽ��� �̵� �ӵ�
    public float detectionRange = 10f; // �÷��̾ �����ϴ� ����

    //private bool isChasing = false; // �ͽ��� ���� ������ ����

    void Start()
    {
        // �±׷� Player ������Ʈ ã��
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer.transform; // Transform ����
        }
        else
        {
            Debug.LogError("Player �±װ� ������ ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }

    void Update()
    {
        if (player == null) return;

        // ���� ���� (�Ÿ� ���� ����)
        ChasePlayer();
    }


    void ChasePlayer()
    {
        // �÷��̾ ���� �̵�
        Vector2 direction = (player.position - transform.position).normalized;
        Vector3 newPosition = transform.position + (Vector3)direction * speed * Time.deltaTime;
        newPosition.z = -10f; // Z�� �� ����
        transform.position = newPosition;

        // ��������Ʈ ������ (���� Scale ����)
        Vector3 currentScale = transform.localScale;
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }


}
