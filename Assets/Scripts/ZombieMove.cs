using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    public Vector3 startPosition; // 초기 위치
    public float minXOffset; // 최소 이동 범위
    public float maxXOffset; // 최대 이동 범위
    private Animator animator; // Animator 컴포넌트
    private SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private float previousX; // 이전 프레임의 x 좌표
    private float startTime; // 애니메이션 시작 시간

    public Sprite studentSprite; // 이 좀비가 변환될 학생 스프라이트

    private bool isStudent = false; // 좀비가 학생으로 변환되었는지 여부

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // 초기 위치 설정
        transform.position = startPosition;
        previousX = transform.position.x;

        // 랜덤한 애니메이션 시작 시간
        startTime = Random.Range(0f, 1f);
    }

    void Update()
    {
        // 학생으로 변환된 경우 이동 중지
        if (isStudent) return;

        // 좀비 이동 처리
        MoveZombie();
    }

    void MoveZombie()
    {
        if (animator == null || spriteRenderer == null || rb == null) return;

        // 애니메이션 진행률 계산
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float progress = (stateInfo.normalizedTime + startTime) % 1;

        // 이동 계산
        float x = Mathf.Lerp(startPosition.x + minXOffset, startPosition.x + maxXOffset, Mathf.PingPong(progress * 2, 1));
        transform.position = new Vector3(x, startPosition.y, startPosition.z);

        // 이동 방향에 따라 스프라이트 뒤집기
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

    public void TransformToStudent()
    {
        if (isStudent) return;

        isStudent = true;

        if (animator != null)
        {
            animator.runtimeAnimatorController = null;
            Debug.Log($"{gameObject.name}: 애니메이터 컨트롤러 제거 완료");
        }

        if (studentSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = studentSprite;
            Debug.Log($"{gameObject.name}: 학생 스프라이트로 변환 완료");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: 학생 스프라이트가 설정되지 않았습니다!");
        }

        // 크기 조정
        transform.localScale = new Vector3(0.4f, 0.4f, 1f);
        Debug.Log($"{gameObject.name}: 학생 크기 조정 완료");

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Debug.Log($"{gameObject.name}: Rigidbody 이동 중지 및 Kinematic 설정 완료");
        }
    }

}
