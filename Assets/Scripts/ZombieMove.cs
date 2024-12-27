using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    public Vector3 startPosition; // 초기 위치 (인스펙터에서 설정 가능)
    public float minXOffset; // 초기 위치를 기준으로 최소 이동 범위 오프셋
    public float maxXOffset; // 초기 위치를 기준으로 최대 이동 범위 오프셋
    private Animator animator; // Animator 컴포넌트
    private SpriteRenderer spriteRenderer; // SpriteRenderer 컴포넌트
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private float previousX; // 이전 프레임의 x 좌표를 저장


    void Start()
    {
        // Animator, SpriteRenderer, Rigidbody2D 컴포넌트 자동 할당
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다. 이 스크립트는 Animator가 있는 오브젝트에 붙어 있어야 합니다.");
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer 컴포넌트를 찾을 수 없습니다. 이 스크립트는 SpriteRenderer가 있는 오브젝트에 붙어 있어야 합니다.");
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D 컴포넌트를 찾을 수 없습니다. 이 스크립트는 Rigidbody2D가 있는 오브젝트에 붙어 있어야 합니다.");
        }

        // 초기 위치 설정
        transform.position = startPosition;
        previousX = transform.position.x;

        Debug.Log($"[Start] 초기 위치 설정: {transform.position}");
    }

    void Update()
    {
        if (animator == null || spriteRenderer == null || rb == null) return; // 필수 컴포넌트가 없으면 실행 중단

        // Animator에서 현재 애니메이션 상태 정보를 가져옴
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 현재 애니메이션의 진행률(0.0 ~ 1.0)을 가져옴
        float progress = stateInfo.normalizedTime % 1;

        // 애니메이션 진행률에 따라 x축 이동 (minX ~ maxX 사이로 움직임)
        float x = Mathf.Lerp(startPosition.x + minXOffset, startPosition.x + maxXOffset, Mathf.PingPong(progress * 2, 1));
        transform.position = new Vector3(x, startPosition.y, startPosition.z);

        // 이동 방향에 따라 스프라이트 방향 변경
        Vector3 currentScale = transform.localScale;

        if (x < previousX)
        {
            // 왼쪽으로 이동 중
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
        else if (x > previousX)
        {
            // 오른쪽으로 이동 중
            transform.localScale = new Vector3(-Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }

        // 현재 x 좌표를 다음 프레임을 위해 저장
        previousX = x;
    }
}
