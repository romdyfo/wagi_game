using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;   // 타이핑 할 객체 생성
    public string fullText =  "사실 우리 학교에는 억울하게 죽은 선생님 한 명이 있대. 그 학생이 환생하려면 인간의 영혼을 먹어야 해서 사람을 좀비로 만든다던데? 다시 원래대로 돌이키려면 그 학생의 노트에 적혀있는 치료제 제조 방법을 찾아야 한대!";
    public float typingSpeed = 0.05f;   // 타이핑하는 속도
    public AudioSource typingSound;  // 타이핑 효과음 추가

    public GameObject backgroundObject;  // 배경 오브젝트
    public GameObject squareObject;      // 사각형 오브젝트

    // 캐릭터 오브젝트
    public GameObject playerObject;
    public GameObject friendObject;

    public GameObject nextButtonObject; // 다음 대사 이동 버튼 오브젝트
    public GameObject dialogObject; // 대사 오브젝트

    public FadeController fadeController; // 페이드 효과를 담당하는 컨트롤러

    // Start is called before the first frame update
    void Start()
    {
        if (textComponent != null) 
        {
            textComponent.text = "";    // 처음에는 빈 텍스트로 시작
            StartCoroutine(TypeText());
        }
        else Debug.LogError("TextComponent가 설정되지 않았습니다.");
        
    }

    // TypeText 함수
    private IEnumerator TypeText()
    {
        if (textComponent == null) yield break; // textComponent가 null인 경우 break

        foreach (char letter in fullText.ToCharArray())
        {
            if (textComponent == null) yield break;

            textComponent.text += letter;   // 한 글자씩 추가하기

            if (letter != ' ' && typingSound != null)   // 글자 공백이 아닌 경우만 효과음 재생되도록
            {
                typingSound.Play();
            }
            yield return new WaitForSeconds(typingSpeed);   // 타이핑 속도에 맞춰서
        }

        // 타이핑이 끝난 후 4초 후에 배경과 사각형을 보이도록 활성화
        yield return new WaitForSeconds(4f);

        if (backgroundObject != null && squareObject != null && playerObject != null && friendObject != null && nextButtonObject != null && dialogObject != null)
        {
            backgroundObject.SetActive(true);   // 배경 활성화
            squareObject.SetActive(true);       // 사각형 활성화

            // 캐릭터 활성화
            playerObject.SetActive(true);
            friendObject.SetActive(true);

            // 대사 관련 활성화
            nextButtonObject.SetActive(true);
            dialogObject.SetActive(true);
        }
    }
}