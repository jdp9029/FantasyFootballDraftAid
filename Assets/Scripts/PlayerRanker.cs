using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerRanker : MonoBehaviour
{



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
        picks.SortPicks(PlayerPrefs.GetString("tiers", string.Empty));

        var qb = picks.draftPicks.First(i => i.metadata.position == "QB");
        var rb = picks.draftPicks.First(i => i.metadata.position == "RB");
        var wr = picks.draftPicks.First(i => i.metadata.position == "WR");
        var te = picks.draftPicks.First(i => i.metadata.position == "TE");

        var qbValue = 0;
        var rbValue = 0;
        var wrValue = 0;
        var teValue = 0;

        var players = new List<DraftPick>() { qb, rb, wr, te };
        var playerValues = new List<double>() { qbValue, rbValue, wrValue, teValue };

        for (int i = 0; i < players.Count; i++)
        {
            var player = players[i];
            var playerValue = playerValues[i];

            //factor by adp at the end of the tier

            //factor by how many of each positions been selected
        }
    }
}
