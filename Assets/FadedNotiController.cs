using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadedNotiController : MonoBehaviour
{
    [Header("UI Elements")]
    public RawImage topBar;      // Thanh trên
    public RawImage bottomBar;   // Thanh dưới
    public Image contentImage;   // Nội dung chính (bên trong có text)

    [Header("Animation Settings")]
    public float barMoveDistance = 350f;  // Khoảng cách 2 thanh di chuyển
    public float barDuration = 0.5f;      // Thời gian mở thanh
    public float contentDelay = 0.5f;     // Delay trước khi hiện nội dung
    public float contentFadeDuration = 0.1f; 

    private Vector2 topStartPos;
    private Vector2 bottomStartPos;

    private void Awake()
    {
        // Lưu vị trí gốc
        topStartPos = topBar.rectTransform.anchoredPosition;
        bottomStartPos = bottomBar.rectTransform.anchoredPosition;

        // Ẩn content ban đầu
        contentImage.color = new Color(1, 1, 1, 0);
    }

    public void ShowNotification()
    {
        // Reset vị trí ban đầu
        topBar.rectTransform.anchoredPosition = topStartPos;
        bottomBar.rectTransform.anchoredPosition = bottomStartPos;
        contentImage.color = new Color(1, 1, 1, 0);

        Sequence seq = DOTween.Sequence();

        // Thanh trên đi lên
        seq.Append(topBar.rectTransform.DOAnchorPosY(topStartPos.y + barMoveDistance, barDuration).SetEase(Ease.OutQuad));

        // Thanh dưới đi xuống (song song với thanh trên)
        seq.Join(bottomBar.rectTransform.DOAnchorPosY(bottomStartPos.y - barMoveDistance, barDuration).SetEase(Ease.OutQuad));

        // Sau khi mở, hiện dần content
        seq.AppendInterval(contentDelay);
        seq.Append(contentImage.DOFade(1f, contentFadeDuration).SetEase(Ease.InOutQuad));
    }

    public void HideNotification()
    {
        Sequence seq = DOTween.Sequence();

        // Ẩn content
        seq.Append(contentImage.DOFade(0f, contentFadeDuration));

        // Đưa 2 thanh về vị trí ban đầu
        seq.Append(topBar.rectTransform.DOAnchorPosY(topStartPos.y, barDuration).SetEase(Ease.InQuad));
        seq.Join(bottomBar.rectTransform.DOAnchorPosY(bottomStartPos.y, barDuration).SetEase(Ease.InQuad));
    }
}
