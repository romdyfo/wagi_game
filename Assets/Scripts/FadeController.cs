using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage; // FadeImage 참조
    public float fadeDuration = 1.0f; // 페이드 지속 시간

    private void Start()
    {
        // 초기 상태 설정 (완전 투명)
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;
        }
    }

    public void FadeOut()   // 색이 나타남
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // 점진적으로 불투명
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f; // 완전히 불투명
        fadeImage.color = color;
    }

    public void FadeIn()        // 색이 점차적으로 꺼짐 --> 근데 이건 필요 없음
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration)); // 점진적으로 투명
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f; // 완전히 투명
        fadeImage.color = color;
    }
}
