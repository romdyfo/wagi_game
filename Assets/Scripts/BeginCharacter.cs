using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine. UI;  // UI 관련 컴포넌트 사용용

public class BeginCharacter : MonoBehaviour
{
    public Image characterImage; // 캐릭터 이미지를 바꿀 UI Image
    public Sprite frontImage;    // 정면 이미지
    public Sprite sideImage;     // 측면 이미지

    // 정면 이미지를 표시
    public void ShowFront()
    {
        Debug.Log(gameObject.name + ": ShowFront 호출됨");
        if (characterImage != null && frontImage != null)
        {
            characterImage.sprite = frontImage;
        }
        else
        {
            Debug.LogError("characterImage 또는 frontImage가 할당되지 않음");
        }
    }

    // 측면 이미지를 표시
    public void ShowSide()
    {
        Debug.Log(gameObject.name + ": ShowSide 호출됨");
        if (characterImage != null && sideImage != null)
        {
            characterImage.sprite = sideImage;
        }
        else
        {
            Debug.LogError("characterImage 또는 sideImage가 할당되지 않음");
        }
    }
}