using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DraftPick
{
    public string player_id;
    public string picked_by;
    public string roster_id;
    public int round;
    public int draft_slot;
    public int pick_no;
    public MetaData metadata;
    public string is_keeper;
    public string draft_id;


    //not serialized
    public int adpRanking;
    public bool startOfTier;
    public bool isDrafted;

    public bool Equals(DraftPick other)
    {
        return other.metadata.first_name == this.metadata.first_name &&
            other.metadata.last_name == this.metadata.last_name &&
            other.metadata.position == this.metadata.position &&
            other.metadata.team == this.metadata.team;
    }
}

[Serializable]
public class DraftPicks
{
    public DraftPick[] draftPicks;

    public static char TierDelimiter = '|';
    public static char PlayerDelimiter = '.';

    public void SortPicks(string loadedData)
    {
        List<DraftPick> list = new();
        
        string[] players = loadedData.Split(PlayerDelimiter);

        foreach (string player in players)
        {
            bool newTier = player.Contains(TierDelimiter);
            var newplayer = player.Replace(TierDelimiter.ToString(), "");

            if (!string.IsNullOrWhiteSpace(newplayer))
            {
                var playerlookup = draftPicks.First(i => i.adpRanking == int.Parse(newplayer));
                list.Add(playerlookup);
                playerlookup.startOfTier = newTier;
            }
        }

        draftPicks = list.ToArray();
    }
}

[Serializable]
public class MetaData
{
    public string team;
    public string status;
    public string sport;
    public string position;
    public string player_id;
    public string number;
    public string news_updated;
    public string last_name;
    public string injury_status;
    public string first_name;
}