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
        // ����ԭʼ״̬
        _originalScale = transform.localScale;

        // ��ȡ��ťͼ�����������У�
        _buttonImage = GetComponent<UnityEngine.UI.Image>();
        if (_buttonImage != null)
        {
            _originalColor = _buttonImage.color;
        }
    }

    #region ������ӿ�ʵ��

    /// <summary>
    /// ��갴��ʱ����
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // ����ʱ������С
        _scaleTweener?.Kill();
        _scaleTweener = transform.DOScale(_originalScale * clickScale, clickDuration * 0.3f)
            .SetEase(clickEase);

        // ��ɫ�仯��������ã�
        if (enableColorChange && _buttonImage != null)
        {
            _colorTweener?.Kill();
            _colorTweener = _buttonImage.DOColor(clickColor, colorDuration * 0.3f);
        }
    }

    /// <summary>
    /// ����ͷ�ʱ����
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        // �ͷ�ʱ�ָ�
        _scaleTweener?.Kill();
        _scaleTweener = transform.DOScale(_originalScale, clickDuration * 0.3f)
            .SetEase(clickEase);

        // ��ɫ�ָ�
        if (enableColorChange && _buttonImage != null)
        {
            _colorTweener?.Kill();
            _colorTweener = _buttonImage.DOColor(_originalColor, colorDuration * 0.3f);
        }
    }

    /// <summary>
    /// ��������ʱ����
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // ���������������
        Sequence clickSequence = DOTween.Sequence();

        // ��һ�׶Σ����ٷŴ�
        clickSequence.Append(transform.DOScale(_originalScale * hoverScale, clickDuration * 0.4f)
            .SetEase(Ease.OutBack));

        // �ڶ��׶Σ��ָ�ԭʼ��С��������Ч��
        clickSequence.Append(transform.DOScale(_originalScale, clickDuration * 0.6f)
            .SetEase(Ease.OutElastic));

        // ��ɫ��˸Ч����������ã�
        if (enableColorChange && _buttonImage != null)
        {
            Sequence colorSequence = DOTween.Sequence();
            colorSequence.Append(_buttonImage.DOColor(hoverColor, colorDuration * 0.3f));
            colorSequence.Append(_buttonImage.DOColor(_originalColor, colorDuration * 0.7f));
        }
    }

    #endregion

    #region �����ͣ�ӿ�ʵ��

    /// <summary>
    /// ������ʱ����
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ͣʱ�Ŵ�
        _scaleTweener?.Kill();
        _scaleTweener = transform.DOScale(_originalScale * hoverScale, hoverDuration)
            .SetEase(hoverEase);

        // ��ͣʱ��ɫ�仯��������ã�
        if (enableColorChange && _buttonImage != null)
        {
            _colorTweener?.Kill();
            _colorTweener = _buttonImage.DOColor(hoverColor, colorDuration);
        }
    }

    /// <summary>
    /// ����뿪ʱ����
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        // �뿪ʱ�ָ�ԭʼ��С
        _scaleTweener?.Kill();
        _scaleTweener = transform.DOScale(_originalScale, hoverDuration)
            .SetEase(Ease.OutQuad);

        // �뿪ʱ��ɫ�ָ���������ã�
        if (enableColorChange && _buttonImage != null)
        {
            _colorTweener?.Kill();
            _colorTweener = _buttonImage.DOColor(_originalColor, colorDuration);
        }
    }

    #endregion

    /// <summary>
    /// ���ð�ť״̬
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
        // ����ʱ����״̬
        ResetButton();
    }

    void OnDestroy()
    {
        // ����ʱ������
        _scaleTweener?.Kill();
        _colorTweener?.Kill();
    }
}