using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    public float minX = -9.5f; // 이동 범위의 최소값
    public float maxX = 2.9f;  // 이동 범위의 최대값
    private Animator animator; // Animator 컴포넌트

    void Start()
    {
        // Animator 컴포넌트 자동으로 할당
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다. 이 스크립트는 Animator가 있는 오브젝트에 붙어 있어야 합니다.");
        }

        // 캐릭터를 초기 위치로 이동
        transform.position = new Vector3(0, 7, 0);
    }

    void Update()
    {
        if (animator == null) return; // Animator가 없으면 Update를 실행하지 않음

        // Animator에서 현재 애니메이션 상태 정보를 가져옴
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 현재 애니메이션의 진행률(0.0 ~ 1.0)을 가져옴
        float progress = stateInfo.normalizedTime % 1;

        // 애니메이션 진행률에 따라 x축 이동 (minX ~ maxX 사이로 움직임)
        float x = Mathf.Lerp(minX, maxX, Mathf.PingPong(progress * 2, 1));
        transform.position = new Vector3(x, 7, 0);
    }
}
