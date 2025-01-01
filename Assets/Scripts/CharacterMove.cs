using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moving : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;

    public float speed = 7f; // 기본 이동 속도
    public int walkCount = 15; // 한 번 입력당 이동할 프레임 수
    private int currentWalkCount;

    private Vector3 vector;

    public float runSpeed = 2f; // 달리기 추가 속도
    private float applyRunSpeed;

    private bool canMove = true; // 이동 가능 여부

    private Animator animator;

    private GameObject[] hearts; // 하트 배열
    public string zombieTag = "Zombie"; // 좀비 태그 이름

    private bool isHandlingCollision = false; // 충돌 처리 중인지 추적

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        // 하트 배열 초기화
        hearts = GameObject.FindGameObjectsWithTag("Heart");
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            // 달리기 여부 체크
            applyRunSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : 0;

            // 방향 설정
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            // 대각선 이동 방지
            if (vector.x != 0)
                vector.y = 0;

            // 애니메이션 방향 설정
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            // 충돌 체크
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

            currentWalkCount = 0; // 이동 카운트 초기화
        }

        animator.SetBool("Walking", false); // 이동 중지 애니메이션
        canMove = true; // 이동 가능 상태로 전환
    }

    void Update()
    {
        if (canMove && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            canMove = false; // 이동 중 추가 입력 방지
            StartCoroutine(MoveCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 이미 충돌 처리 중이라면 무시
        if (isHandlingCollision)
            return;

        // 충돌한 오브젝트가 좀비라면
        if (collision.CompareTag(zombieTag))
        {
            HandleCollisionWithZombie();
        }
    }

    private void HandleCollisionWithZombie()
    {
        isHandlingCollision = true; // 충돌 처리 시작

        // 활성화된 하트 하나 비활성화
        foreach (GameObject heart in hearts)
        {
            if (heart.activeSelf)
            {
                heart.SetActive(false);
                break; // 한 번만 처리하고 종료
            }
        }

        // 남은 하트가 없으면 게임 종료 --> 게임 오버 창 띄우기기
        if (CheckHeartsRemaining() == 0)
        {
            // EndGame();
            SceneManager.LoadScene("ending");
        }
        else
        {
            // 충돌 처리 재활성화 딜레이
            Invoke(nameof(ResetCollisionHandling), 1.5f);
        }
    }

    private int CheckHeartsRemaining()
    {
        int activeHearts = 0;
        foreach (GameObject heart in hearts)
        {
            if (heart.activeSelf)
                activeHearts++;
        }
        return activeHearts;
    }

    private void EndGame()
    {
        Debug.Log("게임 종료!");

        // 에디터 환경에서는 실행 중지, 빌드된 애플리케이션에서는 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
        #else
        Application.Quit(); // 애플리케이션 종료
        #endif
    }

    private void ResetCollisionHandling()
    {
        isHandlingCollision = false; // 충돌 처리 완료
    }
}
