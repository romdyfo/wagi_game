using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 캐릭터 이동 속도
    public Rigidbody2D rb;       // Rigidbody2D 컴포넌트
    public Animator animator;    // Animator 컴포넌트

    Vector2 movement;

    void Update()
    {
        // 사용자 입력 감지
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 애니메이션 파라미터 업데이트
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // 방향에 따라 Boolean 파라미터 설정
        if (movement.y > 0)
        {
            // 캐릭터가 위쪽으로 이동할 때
            animator.SetBool("IsFacingUp", true);
            animator.SetBool("IsFacingDown", false);
        }
        else if (movement.y < 0)
        {
            // 캐릭터가 아래쪽으로 이동할 때
            animator.SetBool("IsFacingUp", false);
            animator.SetBool("IsFacingDown", true);
        }
        else
        {
            // 캐릭터가 멈춰 있을 때 이전 방향 유지
            // (Boolean 파라미터는 이전 상태를 그대로 유지)
        }
    }

    void FixedUpdate()
    {
        // Rigidbody2D를 사용해 캐릭터 이동
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
