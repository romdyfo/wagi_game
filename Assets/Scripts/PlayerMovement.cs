using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ĳ���� �̵� �ӵ�
    public Rigidbody2D rb;       // Rigidbody2D ������Ʈ
    public Animator animator;    // Animator ������Ʈ

    Vector2 movement;

    void Update()
    {
        // ����� �Է� ����
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // �ִϸ��̼� �Ķ���� ������Ʈ
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // ���⿡ ���� Boolean �Ķ���� ����
        if (movement.y > 0)
        {
            // ĳ���Ͱ� �������� �̵��� ��
            animator.SetBool("IsFacingUp", true);
            animator.SetBool("IsFacingDown", false);
        }
        else if (movement.y < 0)
        {
            // ĳ���Ͱ� �Ʒ������� �̵��� ��
            animator.SetBool("IsFacingUp", false);
            animator.SetBool("IsFacingDown", true);
        }
        else
        {
            // ĳ���Ͱ� ���� ���� �� ���� ���� ����
            // (Boolean �Ķ���ʹ� ���� ���¸� �״�� ����)
        }
    }

    void FixedUpdate()
    {
        // Rigidbody2D�� ����� ĳ���� �̵�
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
