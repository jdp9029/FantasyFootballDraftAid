using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Navbar : MonoBehaviour
{
    [SerializeField] RectTransform[] Togglables;
    [SerializeField] Button startDraft;
    [SerializeField] GameObject launchPrefab;
    //[SerializeField] GameObject DraftStatsPrefab;

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

        foreach (var obj in Togglables)
        {
            var up = obj.Find("Up").GetComponent<Button>();
            var down = obj.Find("Down").GetComponent<Button>();

            up.onClick.AddListener(delegate
            {
                if (DraftStarted)
                {
                    return;
                }
                var val = int.Parse(obj.Find("Value").GetComponent<TextMeshProUGUI>().text);
                obj.Find("Value").GetComponent<TextMeshProUGUI>().text = (val + 1).ToString();
            });

            down.onClick.AddListener(delegate
            {
                if (DraftStarted)
                {
                    return;
                }
                var val = int.Parse(obj.Find("Value").GetComponent<TextMeshProUGUI>().text);
                obj.Find("Value").GetComponent<TextMeshProUGUI>().text = (val - 1).ToString();
            });
        }
    }

    private void StartButtonClicked()
    {
        if (DraftStarted)
        {
            return;
        }

        var qbs = int.Parse(Togglables[0].Find("Value").GetComponent<TextMeshProUGUI>().text);
        var rbs = int.Parse(Togglables[1].Find("Value").GetComponent<TextMeshProUGUI>().text);
        var wrs = int.Parse(Togglables[2].Find("Value").GetComponent<TextMeshProUGUI>().text);
        var tes = int.Parse(Togglables[3].Find("Value").GetComponent<TextMeshProUGUI>().text);
        var rounds = int.Parse(Togglables[4].Find("Value").GetComponent<TextMeshProUGUI>().text);
        var members = int.Parse(Togglables[5].Find("Value").GetComponent<TextMeshProUGUI>().text);
        var firstRoundPick = int.Parse(Togglables[6].Find("Value").GetComponent<TextMeshProUGUI>().text);
        //LaunchApiCaller(qbs, rbs, wrs, tes, rounds, members, firstRoundPick);
        //LoadDraftScreen(qbs, rbs, wrs, tes, rounds, members, firstRoundPick);
    }

    private void LoadDraftScreen(int qbs, int rbs, int wrs, int tes, int rounds, int players, int firstRoundPick)
    {
        /*var obj = Instantiate(DraftStatsPrefab);
        var draftStats = obj.GetComponent<ActiveDraftStats>();
        draftStats.NumQbs = qbs;
        draftStats.NumRbs = rbs;
        draftStats.NumWrs = wrs;
        draftStats.NumTes = tes;
        draftStats.NumRounds = rounds;
        draftStats.NumLeagueMembers = players;
        draftStats.FirstRoundPick = firstRoundPick;
        DontDestroyOnLoad(obj);*/
    }

    #region legacy
    private void LaunchApiCaller(int qbs, int rbs, int wrs, int tes, int rounds, int players, int firstRoundPick)
    {
        var launchObject = GameObject.Instantiate(launchPrefab, FindObjectOfType<Canvas>().GetComponent<RectTransform>());
        launchObject.GetComponent<RectTransform>().Find("Button").GetComponent<Button>().onClick.AddListener(delegate
        {
            var code = GUIUtility.systemCopyBuffer.Replace("https://sleeper.app/draft/nfl/", "").Replace("https://sleeper.com/draft/nfl/", "");

            if (ulong.TryParse(code, out _))
            {
                StartDraft(qbs, rbs, wrs, tes, rounds, players, firstRoundPick);
                FindObjectOfType<API_Caller>().URL = $"https://api.sleeper.app/v1/draft/{code}/picks";
                DraftStarted = true;
                launchObject.SetActive(false);
            }
            else
            {
                Debug.Log(code);
            }
        });
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
    #endregion
}
