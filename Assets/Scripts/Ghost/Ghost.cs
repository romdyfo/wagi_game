using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
    public Transform player;
    public float speed = 3f; // �ͽ� �̵� �ӵ�
    public float detectionRange = 10f; // �÷��̾� ���� �����ε� �Ⱦ�
    private Animator animator;
    private bool isDying = false;

    public float clearScreenDelay = 2.0f;
    public string clearSceneName = "GameClear";

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
        if (player == null || isDying) return;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Potion") && !isDying)
        {
            isDying = true;
            StartCoroutine(Die());
        }

        // �÷��̾�� �浹 �� ���� ���� ó��
        // �׽�Ʈ �� �ּ� ó�� �ϰ� �׽�Ʈ�ϱ�
        if (collision.CompareTag("Player"))
        {
            GameOver();
        }
    }
    private System.Collections.IEnumerator Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        yield return new WaitForSeconds(1.0f);

        // ������Ʈ�� ��Ȱ��ȭ�ϱ� ���� ��� �ʿ� �۾��� ������, �̰� �ʼ�
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        TransformAllZombiesToStudents();

        yield return new WaitForSeconds(clearScreenDelay); // 2�� �� Ŭ���� ȭ������ ��ȯ��

        Debug.Log("Ŭ���� ȭ������ ��ȯ ��...");
        SceneManager.LoadScene(clearSceneName);
    }

    private void GameOver()
    {
        Debug.Log("�ͽ��� �÷��̾�� �浹�߽��ϴ�! ���� ����.");
        SceneManager.LoadScene("ending");
    }

    private void TransformAllZombiesToStudents()
    {
        ZombieMove[] zombies = FindObjectsOfType<ZombieMove>();
        foreach (ZombieMove zombie in zombies)
        {
            zombie.TransformToStudent();
        }

        Debug.Log("��� ���� �л����� ��ȯ�Ǿ����ϴ�!");
    }
}
