using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

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

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("DraftStarted"))
        {
            DraftStarted = bool.Parse(PlayerPrefs.GetString("DraftStarted"));
        }    
        else
        {
            DraftStarted = false;
            PlayerPrefs.SetString("DraftStarted", DraftStarted.ToString());
        }
        startDraft.onClick.AddListener(StartButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            var ranker = FindObjectOfType<PlayerRanker>();

            ranker.StartingQBs = qbs;
            ranker.StartingRBs = rbs;
            ranker.StartingWRs = wrs;
            ranker.StartingTEs = tes;

            var team = FindObjectOfType<Team>();
            team.GeneratePicks(firstRoundPick, rounds, players);

            DraftStarted = true;
            PlayerPrefs.SetString("DraftStarted", DraftStarted.ToString());

            startDraft.GetComponent<Image>().color = Color.red;
        }
    }

    private string RemoveLast(string s)
    {
        return s.Remove(s.Length - 1);
    }
}
