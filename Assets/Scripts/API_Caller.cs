using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class API_Caller : MonoBehaviour
{
    [SerializeField] RectTransform FirstSlot;
    [SerializeField] RectTransform SecondSlot;
    [SerializeField] RectTransform ThirdSlot;
    [SerializeField] RectTransform FourthSlot;
    [SerializeField] RectTransform TopFourPlayerPrefab;

    private const string URL = "https://api.sleeper.app/v1/draft/1130987556792897536/picks";

    private float timer = 0;
    private bool clearedTiers = false;
    private bool updatedRankings = false;

    private DraftPick[] recordedPicks = new DraftPick[0];

    private PlayerRanker ranker;
    private Team team;

    private void Start()
    {
        ranker = FindObjectOfType<PlayerRanker>();
        team = FindObjectOfType<Team>();
    }

    private void Update()
    {
        if (FindObjectOfType<Navbar>().DraftStarted)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                GetDraftData();
                timer = 0;
                clearedTiers = false;
                updatedRankings = false;
            }

            if (timer > 1 && !clearedTiers)
            {
                DraggablePlayer.ClearEmptyTiers();
                clearedTiers = true;
            }

            if (timer > 2 && !updatedRankings)
            {
                updatedRankings = true;
                var players = ranker.RankPlayers();

                if (FirstSlot.childCount > 0)
                {
                    Destroy(FirstSlot.GetChild(0).gameObject);
                }
                if (SecondSlot.childCount > 0)
                {
                    Destroy(SecondSlot.GetChild(0).gameObject);
                }
                if (ThirdSlot.childCount > 0)
                {
                    Destroy(ThirdSlot.GetChild(0).gameObject);
                }
                if (FourthSlot.childCount > 0)
                {
                    Destroy(FourthSlot.GetChild(0).gameObject);
                }

                if (players.Count > 0)
                {
                    SetupChild(players[0], FirstSlot);
                }
                if (players.Count > 1)
                {
                    SetupChild(players[1], SecondSlot);
                }
                if (players.Count > 2)
                {
                    SetupChild(players[2], ThirdSlot);
                }
                if (players.Count > 3)
                {
                    SetupChild(players[3], FourthSlot);
                }
            }
        }
    }

    public void GetDraftData()
    {
        StartCoroutine(LoadDraftData());
    }

    IEnumerator LoadDraftData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log($"Error:{request.error}");
            }
            else
            {
                string json = "{\"draftPicks\":[" + request.downloadHandler.text[1..] + '}';

                DraftPicks picks = JsonUtility.FromJson<DraftPicks>(json);
                team.PicksDrafted = picks.draftPicks.Length;

                var newPicks = picks.draftPicks.Where(i => !recordedPicks.Contains(i)).ToArray();

                foreach (var pick in newPicks.Where(i => i.draft_slot == team.FirstRoundPick))
                {
                    team.roster.Add(pick);
                }    

                ErasePlayers(newPicks);
                recordedPicks = picks.draftPicks;
            }
        }
    }

    void ErasePlayers(DraftPick[] picks)
    {
        foreach (DraftPick pick in picks)
        {
            Debug.Log(pick.metadata.first_name);
            var player = FindObjectsOfType<DraggablePlayer>().FirstOrDefault(i => i.PlayerData.Equals(pick));

            if (player != null)
            {
                Destroy(player.gameObject);
            }
        }
    }

    void SetupChild(DraftPick pick, RectTransform slot)
    {
        var player = Instantiate(TopFourPlayerPrefab, slot);
        player.Find("Player Label").GetComponent<TextMeshProUGUI>().text = $"{pick.metadata.first_name} {pick.metadata.last_name}";
        player.Find("Team Background").GetComponent<Image>().color = TierManager.TeamColor(pick);
        player.Find("Team Label").GetComponent<TextMeshProUGUI>().text = $"{pick.metadata.team}";
    }
}
