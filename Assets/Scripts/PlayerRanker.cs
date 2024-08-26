using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRanker : MonoBehaviour
{
    [SerializeField] int StartingQBs;
    [SerializeField] int StartingRBs;
    [SerializeField] int StartingWRs;
    [SerializeField] int StartingTEs;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<DraftPick> RankPlayers(DraftPicks picks)
    {
        var team = GameObject.FindObjectOfType<Team>();

        if (team.NextPick < 0)
        {
            return new();
        }

        picks.SortPicks(PlayerPrefs.GetString("tiers", string.Empty));

        var qbs = GetTopTier(picks, "QB");
        var rbs = GetTopTier(picks, "RB");
        var wrs = GetTopTier(picks, "WR");
        var tes = GetTopTier(picks, "TE");

        var qb = qbs.FirstOrDefault();
        var rb = rbs.FirstOrDefault();
        var wr = wrs.FirstOrDefault();
        var te = tes.FirstOrDefault();

        double qbValue = 0;
        double rbValue = 0;
        double wrValue = 0;
        double teValue = 0;

        int maxQBADP = GetMaxAdp(qbs);
        int maxRBADP = GetMaxAdp(rbs);
        int maxWRADP = GetMaxAdp(wrs);
        int maxTEADP = GetMaxAdp(tes);

        //if we actually have a pick after this upcoming pick
        var nextPick = team.PickAfterNext;
        if (nextPick > 0)
        {
            qbValue += EvaluateAdpToCurrentSelection(maxQBADP, nextPick);
            rbValue += EvaluateAdpToCurrentSelection(maxRBADP, nextPick);
            wrValue += EvaluateAdpToCurrentSelection(maxWRADP, nextPick);
            teValue += EvaluateAdpToCurrentSelection(maxTEADP, nextPick);
        }

        //now evaluate based on positional need
        
        qbValue += EvaluatePlayersTakenToCurrentSelection(StartingQBs, team.roster.Where(i => i.metadata.position == "QB").Count());
        rbValue += EvaluatePlayersTakenToCurrentSelection(StartingRBs, team.roster.Where(i => i.metadata.position == "RB").Count());
        wrValue += EvaluatePlayersTakenToCurrentSelection(StartingWRs, team.roster.Where(i => i.metadata.position == "WR").Count());
        teValue += EvaluatePlayersTakenToCurrentSelection(StartingTEs, team.roster.Where(i => i.metadata.position == "TE").Count());

        var values = new List<double>() { qbValue, rbValue, wrValue, teValue };
        values = values.OrderByDescending(x => x).ToList();
        List<DraftPick> returnable = new List<DraftPick>();

        while (values.Any())
        {
            var v = values.First();
            if (v == qbValue && qb != null)
            {
                returnable.Add(qb);
            }
            else if (v == rbValue && rb != null)
            {
                returnable.Add(rb);
            }
            else if (v == wrValue && wr != null)
            {
                returnable.Add(wr);
            }
            else if (v == teValue && te != null)
            {
                returnable.Add(te);
            }
            values.RemoveAt(0);
        }
        return returnable;
    }

    private List<DraftPick> GetTopTier(DraftPicks picks, string position)
    {
        List<DraftPick> topTier = new List<DraftPick>();
        var players = picks.draftPicks.Where(i => i.metadata.position == position && !i.isDrafted);
        foreach (DraftPick pick in players)
        {
            if (pick == players.First() || !pick.startOfTier)
            {
                topTier.Add(pick);
            }
            else
            {
                return topTier;
            }
        }
        return topTier;
    }

    private int GetMaxAdp(List<DraftPick> picks)
    {
        int maxAdp = 0;
        foreach (var pick in picks)
        {
            maxAdp = Mathf.Max(pick.adpRanking, maxAdp);
        }
        return maxAdp;
    }

    private double EvaluateAdpToCurrentSelection(int maxADP, int pickAfterNext)
    {
        var diff = maxADP - pickAfterNext;

        if (diff > 10)
        {
            return -1;
        }
        else if (diff > 3)
        {
            return -.5;
        }
        else if (diff > -3)
        {
            return .5;
        }
        return 1;
    }

    private double EvaluatePlayersTakenToCurrentSelection(int positionNecessary, int positionTaken)
    {
        var diff = positionNecessary - positionTaken;
        return Mathf.Max(0, diff / 2);
    }
}
