using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePlayer : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [SerializeField] public RectTransform initialParent;
    [SerializeField] public DraftPick PlayerData;
    [SerializeField] public ItemSlot.Position Position;

    [SerializeField] GameObject TierPrefab;

    private int initialPosition;

    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) == transform)
            {
                initialPosition = i;
                break;
            }
        }

        transform.parent = canvas.transform;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += new Vector2(0, eventData.delta.y) / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        var tierManager = FindObjectOfType<TierManager>();

        var mousePosition = eventData.pointerDrag.GetComponent<RectTransform>().position;
        var tierParent = tierManager.ReturnTierParent(Position);
        List<ItemSlot> tiers = new();

        for (int i = 0; i < tierParent.transform.childCount; i++)
        {
            tiers.Add(tierParent.transform.GetChild(i).Find("Players").GetComponent<ItemSlot>());
        }

        ItemSlot slot = tiers.FirstOrDefault(i => IsBetween(i.GetComponent<RectTransform>(), mousePosition.y));

        if (slot != default)
        {
            slot.AddPlayerToTier(this, 25, initialParent, mousePosition);
        }
        else
        {
            int newTierNumber = 1;
            foreach (var slotRect in tiers.Select(i => i.GetComponent<RectTransform>()))
            {
                if (slotRect.transform.position.y > mousePosition.y)
                {
                    newTierNumber++;
                }
                else
                {
                    GameObject scrollParent = tierManager.ReturnTierParent(Position);
                    GameObject newTier = null;

                    slot = tierManager.CreateTier(ref newTier, scrollParent, newTierNumber, Position);

                    for (int i = newTierNumber - 1; i < tierParent.transform.childCount - 1;)
                    {
                        var obj = scrollParent.transform.GetChild(i);

                        if (slot == obj.Find("Players").GetComponent<ItemSlot>())
                        {
                            break;
                        }
                        
                        obj.SetAsLastSibling();
                        obj.Find("Players").GetComponent<ItemSlot>().tierNumber++;
                    }
                    
                    newTier.GetComponent<ItemSlot>().AddPlayerToTier(this, 25, initialParent, mousePosition);
                    break;
                }
            }
        }

        ClearEmptyTiers();
        tierManager.SaveTiers();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    private bool IsBetween(RectTransform rectTransform, float mousePosY)
    {
        var top = rectTransform.position.y + (rectTransform.rect.height / 2);
        var bottom = rectTransform.position.y - (rectTransform.rect.height / 2);
        return mousePosY < top && mousePosY > bottom;
    }

    private void ClearEmptyTiers()
    {
        var tierManager = FindObjectOfType<TierManager>();
        var qbscroll = tierManager.QBScroll;
        var rbscroll = tierManager.RBScroll;
        var wrscroll = tierManager.WRScroll;
        var tescroll = tierManager.TEScroll;

        List<GameObject> scrollables = new List<GameObject>() { qbscroll, rbscroll, wrscroll, tescroll };

        foreach(var scrollable in scrollables )
        {
            for (int i = 0; i < scrollable.transform.childCount; i++)
            {
                var child = scrollable.transform.GetChild(i).Find("Players");
                if (child.childCount == 0)
                {
                    Destroy(child.parent.gameObject);
                    for (int j = i; j < scrollable.transform.childCount; j++)
                    {
                        scrollable.transform.GetChild(j).Find("Players").GetComponent<ItemSlot>().tierNumber--;
                    }    
                }
            }
        }    
    }

}
