using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class MenuManager : MonoBehaviour {

    LoadingScreen loadingScreen;
    bool stats;
    public GameObject menuCanvas, statsCanvas;

	// Use this for initialization
	void Start () {
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });
        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreen>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        if (!stats)
        {
            menuCanvas.SetActive(true);
            statsCanvas.SetActive(false);
        }
        else
        {
            menuCanvas.SetActive(false);
            statsCanvas.SetActive(true);
        }
    }

    public void PlayButton() 
    {

    }

    public void LevelAim()
    {
        loadingScreen.loadLevel2 = true;
    }

    public void LevelReaction()
    {
        loadingScreen.loadLevel1 = true;
    }


    public void StatsButton()
    {
        stats = !stats;
    }
}
