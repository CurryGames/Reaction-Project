using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class MenuManager : MonoBehaviour {

    LoadingScreen loadingScreen;
    bool stats;
    public GameObject menuCanvas, statsCanvas;

	// Use this for initialization
	void Start () {

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });


        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreen>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //PlayGamesPlatform.Instance.SignOut();
            Application.Quit();
        }
        
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


    public void LevelAim()
    {
        loadingScreen.loadLevel2 = true;
    }

    public void LevelReaction()
    {
        loadingScreen.loadLevel1 = true;
    }

    public void CurryGamesButton()
    {
        Application.OpenURL("http://currygames.itch.io/");
    }

    public void StatsButton()
    {
        stats = !stats;
    }
}
