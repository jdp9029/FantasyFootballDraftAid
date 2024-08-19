using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TierManager : MonoBehaviour
{
    [SerializeField] GameObject Tier;

    [SerializeField] public GameObject DraggablePlayerPrefab;
    [SerializeField] public GameObject QBScroll;
    [SerializeField] public GameObject RBScroll;
    [SerializeField] public GameObject WRScroll;
    [SerializeField] public GameObject TEScroll;

    bool tiersExist;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<StartingPlayerList>().LoadInitialPlayers();
        DraftPicks picks = new DraftPicks() { draftPicks = FindObjectOfType<StartingPlayerList>().InitialPlayers.ToArray() };
        DisplayPlayers(picks);
    }

    private void DisplayPlayers(DraftPicks picks)
    {
        var qbTierNumber = 0;
        var rbTierNumber = 0;
        var wrTierNumber = 0;
        var teTierNumber = 0;

        var lastQBADP = int.MinValue;
        var lastRBADP = int.MinValue;
        var lastWRADP = int.MinValue;
        var lastTEADP = int.MinValue;

        GameObject qbs = null;
        GameObject rbs = null;
        GameObject wrs = null;
        GameObject tes = null;

        string tiers = PlayerPrefs.GetString("tiers", string.Empty);
        tiersExist = tiers != string.Empty;
        if (tiersExist)
        {
            picks.SortPicks(tiers);
        }

        foreach (var draftPick in picks.draftPicks)
        {
            switch (draftPick.metadata.position.ToLower())
            {
                case "qb":
                    SetupDraftPick(draftPick, ref qbs, QBScroll, ItemSlot.Position.QB, ref qbTierNumber, ref lastQBADP);
                    break;
                case "rb":
                    SetupDraftPick(draftPick, ref rbs, RBScroll, ItemSlot.Position.RB, ref rbTierNumber, ref lastRBADP);
                    break;
                case "wr":
                    SetupDraftPick(draftPick, ref wrs, WRScroll, ItemSlot.Position.WR, ref wrTierNumber, ref lastWRADP);
                    break;
                case "te":
                    SetupDraftPick(draftPick, ref tes, TEScroll, ItemSlot.Position.TE, ref teTierNumber, ref lastTEADP);
                    break;
            }
        }
    }

    public ItemSlot CreateTier(ref GameObject tier, GameObject scrollParent, int tierNumber, ItemSlot.Position position)
    {
        tier = Instantiate(Tier, scrollParent.transform).transform.Find("Players").gameObject;
        tier.GetComponent<ItemSlot>().tierNumber = tierNumber;
        tier.GetComponent<ItemSlot>().Pos = position;
        return tier.GetComponent<ItemSlot>();
    }

    public GameObject ReturnTierParent(ItemSlot.Position position)
    {
        if (position == ItemSlot.Position.QB)
        {
            return QBScroll;
        }
        if (position == ItemSlot.Position.RB)
        {
            return RBScroll;
        }
        if (position == ItemSlot.Position.WR)
        {
            return WRScroll;
        }
        return TEScroll;
    }

    public void SaveTiers()
    {
        List<GameObject> positions = new List<GameObject> { QBScroll, RBScroll, WRScroll, TEScroll };
        string final = "";

        foreach (var position in positions)
        {
            final += DraftPicks.TierDelimiter;
            for (int i = 0; i < position.transform.childCount; i++)
            {
                var playerParent = position.transform.GetChild(i).Find("Players");
                for (int j = 0; j < playerParent.childCount; j++)
                {
                    var player = playerParent.GetChild(j).GetComponent<DraggablePlayer>();
                    final += player.PlayerData.adpRanking.ToString();
                    final += DraftPicks.PlayerDelimiter;
                }
                final += DraftPicks.TierDelimiter;
            }
        }
        PlayerPrefs.SetString("tiers", final);
    }

    private void SetupDraftPick(DraftPick pick, ref GameObject tier, GameObject scrollParent, ItemSlot.Position position, ref int tierNumber, ref int lastPosADP)
    {
        GameObject obj = Instantiate(DraggablePlayerPrefab, scrollParent.transform);
        obj.GetComponent<DraggablePlayer>().PlayerData = pick;
        obj.GetComponent<DraggablePlayer>().Position = position;

        if (tiersExist)
        {
            if (pick.startOfTier)
            {
                tierNumber++;
                CreateTier(ref tier, scrollParent, tierNumber, position);
            }
        }
        else
        {
            var newTierThreshhold = 12;
            switch (position)
            {
                case ItemSlot.Position.RB:
                case ItemSlot.Position.WR:
                    newTierThreshhold = 8;
                    break;
            }

            if (pick.adpRanking > lastPosADP + newTierThreshhold)
            {
                tierNumber++;
                lastPosADP = pick.adpRanking;
                CreateTier(ref tier, scrollParent, tierNumber, position);
            }
        }

        tier.GetComponent<ItemSlot>().AddPlayerToTier(obj.GetComponent<DraggablePlayer>());
        obj.transform.Find("Player Label").GetComponent<TextMeshProUGUI>().text = $"{pick.metadata.first_name} {pick.metadata.last_name}";

        obj.transform.Find("Team Label").GetComponent<TextMeshProUGUI>().text = $"{pick.metadata.team}";

        Color color = Color.white;
        #region color setup
        switch (pick.metadata.team)
        {
            case "ARI":
            case "ATL":
            case "SF":
            case "TB":
            case "KC":
                color = Color.red;
                break;

            case "DAL":
            case "NYG":
            case "SEA":
            case "BUF":
            case "NE":
                color = Color.blue;
                break;

            //Royal Blue
            case "CAR":
            case "DET":
            case "LAR":
            case "IND":
            case "LAC":
            case "MIA":
            case "TEN":
                color = new Color32(105, 176, 235, 255);
                break;

            //Navy
            case "CHI":
            case "HOU":
                color = new Color32(0, 0, 139, 255);
                break;

            //Green
            case "GB":
            case "PHI":
            case "NYJ":
                color = new Color32(1, 50, 32, 255);
                break;

            //Purple
            case "MIN":
            case "BAL":
                color = new Color32(75, 0, 130, 255);
                break;

            //Tan
            case "NO":
                color = new Color32(210, 180, 140, 255);
                break;

            //Maroon
            case "WAS":
                color = new Color32(128, 0, 0, 255);
                break;

            //Orange
            case "CIN":
            case "CLE":
            case "DEN":
                color = new Color32(255, 165, 0, 255);
                break;

            //Yellow
            case "JAX":
                color = new Color32(155, 135, 12, 255);
                break;

            case "LV":
            case "PIT":
                color = Color.black;
                break;
        }
        #endregion

        obj.transform.Find("Team Background").GetComponent<Image>().color = color;
    }
}
