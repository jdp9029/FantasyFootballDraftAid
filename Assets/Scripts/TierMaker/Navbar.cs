using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Navbar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI qbNum;
    [SerializeField] TextMeshProUGUI rbNum;
    [SerializeField] TextMeshProUGUI wrNum;
    [SerializeField] TextMeshProUGUI teNum;
    [SerializeField] TextMeshProUGUI roundNum;
    [SerializeField] TextMeshProUGUI playersNum;
    [SerializeField] TextMeshProUGUI pickinFirstNum;
    [SerializeField] Button startDraft;

    [HideInInspector] public bool DraftStarted;
    [HideInInspector] PlayerRanker ranker;
    [HideInInspector] Team team;
    [HideInInspector] Navbar navbar;

    // Start is called before the first frame update
    void Start()
    {
        ranker = FindObjectOfType<PlayerRanker>();
        team = FindObjectOfType<Team>();
        navbar = FindObjectOfType<Navbar>();
        DraftStarted = false;
        startDraft.onClick.AddListener(StartButtonClicked);
    }

    private void StartButtonClicked()
    {
        if (DraftStarted)
        {
            return;
        }

        if (int.TryParse(RemoveLast(qbNum.text.ToString()), out var qbs) &&
            int.TryParse(RemoveLast(rbNum.text.ToString()), out var rbs) &&
            int.TryParse(RemoveLast(wrNum.text.ToString()), out var wrs) &&
            int.TryParse(RemoveLast(teNum.text.ToString()), out var tes) &&
            int.TryParse(RemoveLast(roundNum.text.ToString()), out var rounds) &&
            int.TryParse(RemoveLast(playersNum.text.ToString()), out var players) &&
            int.TryParse(RemoveLast(pickinFirstNum.text.ToString()), out var firstRoundPick))
        {
            StartDraft(qbs, rbs, wrs, tes, rounds, players, firstRoundPick);
            DraftStarted = true;
        }
    }

    private string RemoveLast(string s)
    {
        return s.Remove(s.Length - 1);
    }

    private void StartDraft(int qbs, int rbs, int wrs, int tes, int rounds, int players, int firstRoundPick)
    {
        ranker.StartingQBs = qbs;
        ranker.StartingRBs = rbs;
        ranker.StartingWRs = wrs;
        ranker.StartingTEs = tes;

        team.GeneratePicks(firstRoundPick, rounds, players);

        for (int i = 0; i < navbar.transform.childCount; i++)
        {
            navbar.transform.GetChild(i).gameObject.SetActive(i > 7);
        }
    }
}
