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

    private const string URL = "https://api.sleeper.app/v1/draft/999431784959942657/picks";

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
                ranker.RankPlayers();
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

                ErasePlayers(newPicks);
                recordedPicks = picks.draftPicks;
            }
        }
    }

    void ErasePlayers(DraftPick[] picks)
    {
        foreach (DraftPick pick in picks)
        {
            var player = FindObjectsOfType<DraggablePlayer>().FirstOrDefault(i => i.PlayerData.Equals(pick));

            if (player != null)
            {
                Destroy(player.gameObject);
            }
        }
    }
}
