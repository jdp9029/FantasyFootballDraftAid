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
    private const string URL = "https://api.sleeper.app/v1/draft/999431784959942657/picks";

    private float timer = 0;

    private void Update()
    {
        if (FindObjectOfType<Navbar>().DraftStarted)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                GetDraftData();
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
            }
        }
    }
}
