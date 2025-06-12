using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveDraftStats : MonoBehaviour
{
    public int NumQbs { get; set; }
    public int NumRbs { get; set; }
    public int NumWrs { get; set; }
    public int NumTes { get; set; }
    public int NumRounds { get; set; }
    public int NumLeagueMembers { get; set; }
    public int FirstRoundPick { get; set; }

    // Start is called before the first frame update
    void Start() 
    {
        SceneManager.LoadScene("Draft Screen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
