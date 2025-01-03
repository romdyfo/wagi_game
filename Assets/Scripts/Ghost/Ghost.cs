using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform player;
    public float speed = 3f; // 귀신의 이동 속도
    public float detectionRange = 10f; // 플레이어 감지 범위
    private Animator animator; // 귀신 애니메이터
    private bool isDying = false; // 귀신이 소멸 중인지 여부


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
        Debug.Log("귀신이 물약에 맞았습니다!");
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
            animator.SetTrigger("Die"); // 소멸 애니메이션 재생
        }

        yield return new WaitForSeconds(1.0f);

        Destroy(gameObject);
    }



}
