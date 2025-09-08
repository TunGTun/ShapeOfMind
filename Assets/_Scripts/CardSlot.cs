using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Card card = eventData.pointerDrag.GetComponent<Card>();
        if (card != null)
        {
            if (transform.childCount == 0)
            {
                card.transform.SetParent(transform);
                card.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                Card topCard = transform.GetChild(transform.childCount - 1).GetComponent<Card>();
            }
        }
    }
}
