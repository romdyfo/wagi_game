using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform player; // Player Transform
    public float speed = 3f; // 귀신의 이동 속도
    public float detectionRange = 10f; // 플레이어를 감지하는 범위

    //private bool isChasing = false; // 귀신이 추적 중인지 여부

    void Start()
    {
        // 태그로 Player 오브젝트 찾기
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer.transform; // Transform 연결
        }
        else
        {
            Debug.LogError("Player 태그가 설정된 오브젝트를 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        if (player == null) return;

        // 추적 동작 (거리 조건 제거)
        ChasePlayer();
    }


    void ChasePlayer()
    {
        // 플레이어를 향해 이동
        Vector2 direction = (player.position - transform.position).normalized;
        Vector3 newPosition = transform.position + (Vector3)direction * speed * Time.deltaTime;
        newPosition.z = -10f; // Z축 값 고정
        transform.position = newPosition;

        // 스프라이트 뒤집기 (기존 Scale 유지)
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
