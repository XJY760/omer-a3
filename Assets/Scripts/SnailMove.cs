using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMove : MonoBehaviour
{
    RectTransform imageTransform; // ��Inspector������Ҫ�ƶ���ͼƬ
    private Vector2 startPosition = new Vector2(-579f, -207f);
    private Vector2 endPosition = new Vector2(692f, -207f);
    private float moveDuration = 2f; // �ƶ�����ʱ�䣨�룩
    private bool isMoving = false;

    void Start()
    {
        imageTransform = GetComponent<RectTransform>();
        // ��ʼ��ͼƬλ��
        imageTransform.anchoredPosition = startPosition;
    }

    public void OnMoveButtonClick()
    {
        if (isMoving) return;
        isMoving = true;
        StartCoroutine(MoveImage());
    }

    private IEnumerator MoveImage()
    {
        isMoving = true;
        float elapsedTime = 0f;
        Vector2 currentPos = imageTransform.anchoredPosition;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / moveDuration;

            // ʹ��Lerpƽ���ƶ�
            imageTransform.anchoredPosition = Vector2.Lerp(currentPos, endPosition, progress);

            yield return null;
        }

        // ȷ������λ��׼ȷ
        imageTransform.anchoredPosition = endPosition;
        isMoving = false;
    }
}