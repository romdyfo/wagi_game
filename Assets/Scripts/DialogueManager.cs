using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리 할라고 추가

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // 대화 텍스트를 표시할 TextMeshProUGUI
    public Button nextButton;            // 다음 버튼
    public GameObject backgroundObject;  // 배경 오브젝트
    public GameObject squareObject;      // 검정색 직사각형 오브젝트

    public BeginCharacter greenCharacter; // Green 캐릭터의 BeginCharacter 컴포넌트
    public BeginCharacter pinkCharacter;  // Pink 캐릭터의 BeginCharacter 컴포넌트

    private int currentLineIndex = 0;    // 현재 대화의 인덱스

    public FadeController fadeController; // 페이드 효과를 담당하는 컨트롤러
    private bool dialogueEnded = false;  // 대화가 끝났는지 여부

    private string[] dialogueLines = new string[]  // 대화 배열
    {
        "야, 우리 학교 괴담 들어봤어?",
        "뭐? 괴담이라니. 설마 또 지어낸 무서운 얘기 그런 거 하는 거야?",
        "아니, 이번엔 진짜야. 우리 학교에서 죽은 선생님이 귀신에 돼서 다른 학생들을 좀비로 만든대!",
        "에이~ 그런 걸 어떻게 믿으라고~ 너가 지어낸 거 다 티나!",
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
            Debug.LogError("nextButton이 할당되지 않음");
        }

        if (fadeController != null)
        {
            fadeController.FadeIn();
        }

        UpdateCharacterImages(); // 첫 대사에 맞는 이미지 설정
    }

    // 다음 버튼 클릭 시 호출되는 함수
    private void OnNextButtonClicked()
    {
        if (dialogueEnded)
        {
            return;
        }

        if (currentLineIndex < dialogueLines.Length)
        {
            ShowDialogue(dialogueLines[currentLineIndex]);  // 대화 표시
            UpdateCharacterImages(); // 대사에 따라 캐릭터 이미지 업데이트
            currentLineIndex++;  // 대화 인덱스 증가
        }
        else
        {
            dialogueEnded = true; // 대화 종료

            if (fadeController != null)
            {
                fadeController.FadeOut();  // 페이드 아웃
            }

            // 일정 시간 대기 후 비기닝 종료료
            StartCoroutine(EndSceneAfterDelay());
        }
    }

    // 대화 텍스트 표시
    private void ShowDialogue(string dialogue)
    {
        if (dialogueText != null)
        {
            dialogueText.text = dialogue;  // 대화 텍스트 갱신
        }
        else
        {
            Debug.LogError("dialogueText null");
        }
    }

    // 캐릭터 이미지를 대사에 맞게 업데이트
    private void UpdateCharacterImages()
    {
        // // 대사에 따라 캐릭터 이미지를 업데이트
        // if (dialogueLines[currentLineIndex].StartsWith("green:"))
        // {
        //     greenCharacter.ShowSide();
        //     pinkCharacter.ShowFront();
        // }
        // else if (dialogueLines[currentLineIndex].StartsWith("pink:"))
        // {
        //     greenCharacter.ShowFront();
        //     pinkCharacter.ShowSide();
        // }

        if (currentLineIndex % 2 == 0) // 인덱스가 짝수면 green 캐릭터가 대화
        {
            greenCharacter.ShowSide();
            pinkCharacter.ShowFront();
        }
        else // 인덱스가 홀수면 pink 캐릭터가 대화
        {
            greenCharacter.ShowFront();
            pinkCharacter.ShowSide();
        }
    }

    // 비기닝 씬 종료 대기 후 실행
    private IEnumerator EndSceneAfterDelay()
    {
        yield return new WaitForSeconds(2f); // 페이드 아웃 후 2초 대기
        Debug.Log("비기닝 종료, 게임 시작");
        SceneManager.LoadScene("SampleScene");
    }
}
