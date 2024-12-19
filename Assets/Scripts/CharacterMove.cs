using System.Collections;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public float speed = 7f;      // 기본 이동 속도
    public int walkCount = 15;    // 한 번 입력으로 이동할 스텝 수
    private int currentWalkCount;

    private Vector3 vector;

    public float runSpeed = 2f;   // 달리기 추가 속도
    private float applyRunSpeed;
    //private bool applyRunFlag = false;

    private bool canMove = true;  // 이동 가능 여부

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
            // 달리기 모드 체크
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
            }
            else
            {
                applyRunSpeed = 0;
            }

            // 방향 벡터 설정
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            // 대각선 이동 방지
            if (vector.x != 0)
                vector.y = 0;

            // 애니메이터 방향 설정
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            // 충돌 감지
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(vector.x * speed, vector.y * speed);

            boxCollider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            if (hit.transform != null)
                break;

            // 이동 애니메이션 활성화
            animator.SetBool("Walking", true);

            // 이동 처리
            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * (speed + applyRunSpeed) * Time.deltaTime,
                                    vector.y * (speed + applyRunSpeed) * Time.deltaTime,
                                    0);
                currentWalkCount++;
                yield return null; // 다음 프레임까지 대기
            }

            currentWalkCount = 0; // 이동 횟수 초기화
        }

        animator.SetBool("Walking", false); // 이동 중지 애니메이션
        canMove = true; // 이동 가능 상태로 변경
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false; // 이동 중에는 추가 입력 금지
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
