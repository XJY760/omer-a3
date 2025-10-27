using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class AdvancedButtonEffect : MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Hover Effect Settings")]
    public float hoverScale = 1.1f;
    public float hoverDuration = 0.3f;
    public Ease hoverEase = Ease.OutBack;

    [Header("Click Effect Settings")]
    public float clickScale = 0.9f;
    public float clickDuration = 0.2f;
    public Ease clickEase = Ease.OutQuad;

    [Header("Advanced Effects")]
    public bool enableColorChange = true;
    public Color hoverColor = new Color(1.1f, 1.1f, 1.1f, 1f);
    public Color clickColor = new Color(0.9f, 0.9f, 0.9f, 1f);
    public float colorDuration = 0.2f;

    private Vector3 _originalScale;
    private Color _originalColor;
    private Tweener _scaleTweener;
    private Tweener _colorTweener;
    private UnityEngine.UI.Image _buttonImage;

    void Start()
    {
        // 保存原始状态
        _originalScale = transform.localScale;

        // 获取按钮图像组件（如果有）
        _buttonImage = GetComponent<UnityEngine.UI.Image>();
        if (_buttonImage != null)
        {
            _originalColor = _buttonImage.color;
        }
    }

    #region 鼠标点击接口实现

    /// <summary>
    /// 鼠标按下时触发
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // 按下时快速缩小
        _scaleTweener?.Kill();
        _scaleTweener = transform.DOScale(_originalScale * clickScale, clickDuration * 0.3f)
            .SetEase(clickEase);

        // 颜色变化（如果启用）
        if (enableColorChange && _buttonImage != null)
        {
            _colorTweener?.Kill();
            _colorTweener = _buttonImage.DOColor(clickColor, colorDuration * 0.3f);
        }
    }

    /// <summary>
    /// 鼠标释放时触发
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        // 释放时恢复
        _scaleTweener?.Kill();
        _scaleTweener = transform.DOScale(_originalScale, clickDuration * 0.3f)
            .SetEase(clickEase);

        // 颜色恢复
        if (enableColorChange && _buttonImage != null)
        {
            _colorTweener?.Kill();
            _colorTweener = _buttonImage.DOColor(_originalColor, colorDuration * 0.3f);
        }
    }

    /// <summary>
    /// 鼠标点击完成时触发
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // 创建点击动画序列
        Sequence clickSequence = DOTween.Sequence();

        // 第一阶段：快速放大
        clickSequence.Append(transform.DOScale(_originalScale * hoverScale, clickDuration * 0.4f)
            .SetEase(Ease.OutBack));

        // 第二阶段：恢复原始大小，带弹性效果
        clickSequence.Append(transform.DOScale(_originalScale, clickDuration * 0.6f)
            .SetEase(Ease.OutElastic));

        // 颜色闪烁效果（如果启用）
        if (enableColorChange && _buttonImage != null)
        {
            Sequence colorSequence = DOTween.Sequence();
            colorSequence.Append(_buttonImage.DOColor(hoverColor, colorDuration * 0.3f));
            colorSequence.Append(_buttonImage.DOColor(_originalColor, colorDuration * 0.7f));
        }
    }

    #endregion

    #region 鼠标悬停接口实现

    /// <summary>
    /// 鼠标进入时触发
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 悬停时放大
        _scaleTweener?.Kill();
        _scaleTweener = transform.DOScale(_originalScale * hoverScale, hoverDuration)
            .SetEase(hoverEase);

        // 悬停时颜色变化（如果启用）
        if (enableColorChange && _buttonImage != null)
        {
            _colorTweener?.Kill();
            _colorTweener = _buttonImage.DOColor(hoverColor, colorDuration);
        }
    }

    /// <summary>
    /// 鼠标离开时触发
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        // 离开时恢复原始大小
        _scaleTweener?.Kill();
        _scaleTweener = transform.DOScale(_originalScale, hoverDuration)
            .SetEase(Ease.OutQuad);

        // 离开时颜色恢复（如果启用）
        if (enableColorChange && _buttonImage != null)
        {
            _colorTweener?.Kill();
            _colorTweener = _buttonImage.DOColor(_originalColor, colorDuration);
        }
    }

    #endregion

    /// <summary>
    /// 重置按钮状态
    /// </summary>
    public void ResetButton()
    {
        _scaleTweener?.Kill();
        _colorTweener?.Kill();

        transform.localScale = _originalScale;
        if (_buttonImage != null)
        {
            _buttonImage.color = _originalColor;
        }
    }

    void OnDisable()
    {
        // 禁用时重置状态
        ResetButton();
    }

    void OnDestroy()
    {
        // 销毁时清理动画
        _scaleTweener?.Kill();
        _colorTweener?.Kill();
    }
}