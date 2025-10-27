using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeImageController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    public float delayBetweenFades = 0.5f;

    private void Start()
    {
        FadeOutThenIn();
    }

    public void FadeOutThenIn()
    {
        // 确保Image是激活状态
        fadeImage.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        // 第一步：淡出（变黑/不透明）
        sequence.Append(fadeImage.DOColor(new Color(0, 0, 0, 1), 0));

        // 第二步：延迟
        sequence.AppendInterval(delayBetweenFades);

        // 第三步：淡入（变透明）
        sequence.Append(fadeImage.DOColor(new Color(0, 0, 0, 0), fadeDuration));

        // 动画完成后禁用Image（可选）
        sequence.OnComplete(() => {
            fadeImage.gameObject.SetActive(false);
            //Debug.Log("淡出淡入动画完成");
        });
    }
}