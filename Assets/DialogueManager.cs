using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // 대화 텍스트를 표시할 TextMeshProUGUI
    public Button nextButton;             // 다음 버튼
    public GameObject backgroundObject;  // 배경 오브젝트
    public GameObject squareObject;      // 검정색 직사각형 오브젝트
    private int currentLineIndex = 0;     // 현재 대화의 인덱스

    public FadeController fadeController; // 페이드 효과를 담당하는 컨트롤러

    private string[] dialogueLines = new string[]  // 대화 배열
    {
        "야, 우리 학교 괴담 들어봤어?",
        "player: 뭐? 괴담이라니. 설마 또 지어낸 무서운 얘기 그런 거 하는 거야?",
        "아니, 이번엔 진짜야. 우리 학교에서 죽은 선생님이 귀신에 돼서 다른 학생들을 좀비로 만든대!",
        "player: 에이~ 그런 걸 어떻게 믿으라고~ 너가 지어낸 거 다 티나!",
        "아무래도 그런가..?"
    };

    // Start is called before the first frame update
    void Start()
    {
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(OnNextButtonClicked);  // 버튼 클릭 시 이벤트 등록
        }
        else
        {
            Debug.LogError("nextButton이 할당되지 않았습니다.");
        }

        if (fadeController != null)
        {
            fadeController.FadeIn();
        }
    }

    // 다음 버튼 클릭 시 호출되는 함수
    private void OnNextButtonClicked()
    {   
        if (currentLineIndex < dialogueLines.Length)
        {
            ShowDialogue(dialogueLines[currentLineIndex]);  // 대화 표시
            currentLineIndex++;  // 대화 인덱스 증가

            // 대화가 진행 중일 때는 페이드 아웃 화면을 숨기고
            // if (fadeController != null)
            // {
            //     fadeController.FadeIn();
            // }
        }
        else
        {
            // "아무래도 그런가..?" 대사 뒤에만 페이드 아웃 화면을 보이게 함
            if (currentLineIndex == dialogueLines.Length)
            {
                // 페이드 아웃을 실행
                if (fadeController != null)
                {
                    fadeController.FadeOut();  // 페이드 아웃
                }
            }

            // 페이드 아웃 후 추가 동작을 실행하고 싶다면 여기에 작성
            Debug.Log("대화가 모두 끝났습니다.");
        }
    }

    // 대화 텍스트 표시
    private void ShowDialogue(string dialogue)
    {
        dialogueText.text = dialogue;  // 대화 텍스트 갱신
    }
}
