using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    public Vector3 startPosition; // 초기 위치 (인스펙터에서 설정 가능)
    public float minXOffset; // 최소 이동 범위 오프셋
    public float maxXOffset; // 최대 이동 범위 오프셋
    private Animator animator; // Animator 컴포넌트
    private SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private float previousX; // 이전 프레임의 x 좌표를 저장
    private float startTime; // 애니메이션 시작 시간 (각 좀비마다 다르게 설정)

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // 초기 위치 설정
        transform.position = startPosition;
        previousX = transform.position.x;

        // 각 좀비마다 애니메이션 진행 상태를 다르게 설정하기 위해 startTime을 다르게 설정
        startTime = Random.Range(0f, 1f); // 0과 1 사이의 랜덤 값 부여
    }

    void Update()
    {
        if (animator == null || spriteRenderer == null || rb == null) return;

        // 애니메이션 진행률 계산
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float progress = (stateInfo.normalizedTime + startTime) % 1;

        // 애니메이션 진행에 따라 x축 이동 (minX ~ maxX 사이로 움직임)
        float x = Mathf.Lerp(startPosition.x + minXOffset, startPosition.x + maxXOffset, Mathf.PingPong(progress * 2, 1));
        transform.position = new Vector3(x, startPosition.y, startPosition.z);

        // 이동 방향에 따라 스프라이트 방향 변경
        Vector3 currentScale = transform.localScale;
        if (x < previousX)
        {
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
        else if (x > previousX)
        {
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }

        previousX = x;
    }
}

