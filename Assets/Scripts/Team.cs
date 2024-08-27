using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Team : MonoBehaviour
{
    [HideInInspector] public List<int> picks;

    [HideInInspector] public int PicksDrafted;

    [HideInInspector] public int NextPick
    {
        get
        {
            var nextPick = picks.Where(i => i < PicksDrafted);
            if (nextPick.Any())
            {
                return nextPick.First();
            }
            return int.MinValue;
        }
    }


    [HideInInspector] public int PickAfterNext
    {
        get
        {
            var nextPick = picks.Where(i => i < PicksDrafted).ToArray();
            if (nextPick.Length > 1)
            {
                return nextPick[1];
            }
            return int.MinValue;
        }
    }

    [HideInInspector] public List<DraftPick> roster;

    [HideInInspector] public int FirstRoundPick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GeneratePicks(int firstRoundPick, int numRounds, int numPlayers)
    {
        FirstRoundPick = firstRoundPick;
        for (int round = 0; round < numRounds; round++)
        {
            //odd number rounds (because of the zero index on round #)
            if (round % 2 == 0)
            {
                var pick = (round * numPlayers) + firstRoundPick;
                picks.Add(pick);
            }

            else
            {
                var pick = ((round + 1) * numPlayers) - firstRoundPick + 1;
                picks.Add(pick);
            }
        }
    }
}
