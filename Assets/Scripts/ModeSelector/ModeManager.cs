using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour
{
    [SerializeField] Button redraftButton;
    [SerializeField] Button dynastyButton;
    [SerializeField] Button goButton;

    private bool? _isDynasty = null;

    // Start is called before the first frame update
    void Start()
    {
        redraftButton.GetComponent<Image>().color = Color.white;
        dynastyButton.GetComponent<Image>().color = Color.white;
        redraftButton.onClick.AddListener(delegate
        {
            _isDynasty = false;
            redraftButton.GetComponent<Image>().color = Color.cyan;
            dynastyButton.GetComponent<Image>().color = Color.white;
            goButton.GetComponent<Image>().color = Color.green;
        });
        dynastyButton.onClick.AddListener(delegate
        {
            _isDynasty = true;
            redraftButton.GetComponent<Image>().color = Color.white;
            dynastyButton.GetComponent<Image>().color = Color.cyan;
            goButton.GetComponent<Image>().color = Color.green;
        });
        goButton.onClick.AddListener(delegate
        {
            if (_isDynasty.HasValue)
            {
                StartingPlayerList.IsDynasty = _isDynasty.Value;
                SceneManager.LoadScene("TierMaker");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
