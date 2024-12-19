using System.Collections;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public float speed = 7f;      // �⺻ �̵� �ӵ�
    public int walkCount = 15;    // �� �� �Է����� �̵��� ���� ��
    private int currentWalkCount;

    private Vector3 vector;

    public float runSpeed = 2f;   // �޸��� �߰� �ӵ�
    private float applyRunSpeed;
    //private bool applyRunFlag = false;

    private bool canMove = true;  // �̵� ���� ����

    private Animator animator;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            // �޸��� ��� üũ
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
            }
            else
            {
                applyRunSpeed = 0;
            }

            // ���� ���� ����
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            // �밢�� �̵� ����
            if (vector.x != 0)
                vector.y = 0;

            // �ִϸ����� ���� ����
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            // �浹 ����
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(vector.x * speed, vector.y * speed);

            boxCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null)
                break;

            // �̵� �ִϸ��̼� Ȱ��ȭ
            animator.SetBool("Walking", true);

            // �̵� ó��
            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed) * Time.deltaTime,
                                    vector.y * (speed + applyRunSpeed) * Time.deltaTime,
                                    0);
                currentWalkCount++;
                yield return null; // ���� �����ӱ��� ���
            }

            currentWalkCount = 0; // �̵� Ƚ�� �ʱ�ȭ
        }

        animator.SetBool("Walking", false); // �̵� ���� �ִϸ��̼�
        canMove = true; // �̵� ���� ���·� ����
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false; // �̵� �߿��� �߰� �Է� ����
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
