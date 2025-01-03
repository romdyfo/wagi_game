using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform player;
    public float speed = 3f; // �ͽ��� �̵� �ӵ�
    public float detectionRange = 10f; // �÷��̾� ���� ����
    private Animator animator; // �ͽ� �ִϸ�����
    private bool isDying = false; // �ͽ��� �Ҹ� ������ ����


    void Start()
    {
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }
        else
        {
            Debug.LogError("Player �±װ� ������ ������Ʈ�� ã�� �� �����ϴ�!");
        }
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        ChasePlayer();
    }


    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        Vector3 newPosition = transform.position + (Vector3)direction * speed * Time.deltaTime;
        newPosition.z = -10f;
        transform.position = newPosition;

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

    public void TakeDamage()
    {
        Debug.Log("�ͽ��� ���࿡ �¾ҽ��ϴ�!");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potion") && !isDying)
        {
            isDying = true;
            StartCoroutine(Die());
        }
    }

    private System.Collections.IEnumerator Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die"); // �Ҹ� �ִϸ��̼� ���
        }

        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }



}
