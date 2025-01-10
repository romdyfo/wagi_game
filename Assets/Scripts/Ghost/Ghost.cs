using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
    public Transform player;
    public float speed = 3f; // 귀신 이동 속도
    public float detectionRange = 10f; // 플레이어 감지 범위인데 안씀
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
            Debug.LogError("Player 태그가 설정된 오브젝트를 찾을 수 없습니다!");
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

        // 플레이어와 충돌 시 게임 오버 처리
        // 테스트 시 주석 처리 하고 테스트하기
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

        // 오브젝트를 비활성화하기 전에 모든 필요 작업을 마무리, 이거 필수
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        TransformAllZombiesToStudents();

        yield return new WaitForSeconds(clearScreenDelay); // 2초 후 클리어 화면으로 전환됨

        Debug.Log("클리어 화면으로 전환 중...");
        SceneManager.LoadScene(clearSceneName);
    }

    private void GameOver()
    {
        Debug.Log("귀신이 플레이어와 충돌했습니다! 게임 오버.");
        SceneManager.LoadScene("ending");
    }

    private void TransformAllZombiesToStudents()
    {
        ZombieMove[] zombies = FindObjectsOfType<ZombieMove>();
        foreach (ZombieMove zombie in zombies)
        {
            zombie.TransformToStudent();
        }

        Debug.Log("모든 좀비가 학생으로 변환되었습니다!");
    }
}
