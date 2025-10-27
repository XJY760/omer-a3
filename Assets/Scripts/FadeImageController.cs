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
        // ȷ��Image�Ǽ���״̬
        fadeImage.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();

        // ��һ�������������/��͸����
        sequence.Append(fadeImage.DOColor(new Color(0, 0, 0, 1), 0));

        // �ڶ������ӳ�
        sequence.AppendInterval(delayBetweenFades);

        // �����������루��͸����
        sequence.Append(fadeImage.DOColor(new Color(0, 0, 0, 0), fadeDuration));

        // ������ɺ����Image����ѡ��
        sequence.OnComplete(() => {
            fadeImage.gameObject.SetActive(false);
            //Debug.Log("�������붯�����");
        });
    }
}