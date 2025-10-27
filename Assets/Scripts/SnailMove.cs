using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMove : MonoBehaviour
{
    RectTransform imageTransform; // 在Inspector中拖入要移动的图片
    private Vector2 startPosition = new Vector2(-579f, -207f);
    private Vector2 endPosition = new Vector2(692f, -207f);
    private float moveDuration = 2f; // 移动持续时间（秒）
    private bool isMoving = false;

    void Start()
    {
        imageTransform = GetComponent<RectTransform>();
        // 初始化图片位置
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

            // 使用Lerp平滑移动
            imageTransform.anchoredPosition = Vector2.Lerp(currentPos, endPosition, progress);

            yield return null;
        }

        // 确保最终位置准确
        imageTransform.anchoredPosition = endPosition;
        isMoving = false;
    }
}