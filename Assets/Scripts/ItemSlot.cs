using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Assertions.Must;
using System.Linq;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Position Pos;
    public int tierNumber;

    public void Update()
    {
        transform.parent.Find("Tier Label").GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{Pos.ToString()}s Tier {tierNumber}";

        switch (Pos)
        {
            case Position.QB:
                GetComponent<Image>().color = new Color(1, 0, 0, .8f);
                transform.parent.Find("Tier Label").GetComponent<Image>().color = Color.red;
                break;
            case Position.RB:
                GetComponent<Image>().color = new Color(0, 1, 0, .8f);
                transform.parent.Find("Tier Label").GetComponent<Image>().color = Color.green;
                break;
            case Position.WR:
                GetComponent<Image>().color = new Color(0, 0, 1, .8f);
                transform.parent.Find("Tier Label").GetComponent<Image>().color = Color.blue;
                break;
            case Position.TE:
                GetComponent<Image>().color = new Color(1, 0, 1, .8f);
                transform.parent.Find("Tier Label").GetComponent<Image>().color = Color.magenta;
                break;
        }
    }

    public void AddPlayerToTier(DraggablePlayer player, float sizeChange = 25, RectTransform itemsInitialParent = null, object mousePosition = null)
    {
        transform.parent.GetComponent<LayoutElement>().preferredHeight += sizeChange;
        GetComponent<RectTransform>().sizeDelta += new Vector2(0, sizeChange);
        transform.parent.Find("Tier Label").GetComponent<RectTransform>().anchoredPosition = .5f * new Vector2(0, GetComponent<RectTransform>().sizeDelta.y);

        if (itemsInitialParent != null)
        {
            itemsInitialParent.transform.parent.GetComponent<LayoutElement>().preferredHeight -= sizeChange;
            itemsInitialParent.sizeDelta -= new Vector2(0, sizeChange);
            itemsInitialParent.parent.Find("Tier Label").GetComponent<RectTransform>().anchoredPosition = .5f * new Vector2(0, itemsInitialParent.sizeDelta.y);
        }

        player.initialParent = GetComponent<RectTransform>();

        if (mousePosition != null && mousePosition is Vector3 pos)
        {
            //loop through the EXISTING children of the tier
            for (int i = 0; i < transform.childCount; i++)
            {
                //if we find a good slot to insert it
                if (transform.GetChild(i).position.y < pos.y)
                {
                    player.transform.SetParent(transform, false);
                    while (transform.GetChild(i) != player.transform)
                    {
                        transform.GetChild(i).SetAsLastSibling();
                    }
                    return;
                }
            }
        }
        player.transform.SetParent(transform, false);
    }

    public List<DraftPick> ReturnPlayersInTier()
    {
        var returnable = new List<DraftPick>();
        for (int i = 0; i < transform.childCount;i++)
        {
            returnable.Add(transform.GetChild(i).GetComponent<DraggablePlayer>().PlayerData);
        }
        return returnable;
    }

    public enum Position
    {
        QB, RB, WR, TE, OTHER
    }
}
