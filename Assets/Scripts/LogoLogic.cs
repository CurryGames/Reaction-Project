using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using ChartboostSDK;
using UnityEngine.SocialPlatforms;

public class LogoLogic : MonoBehaviour {

    private float _currentTime, _maxTime;
    public LoadingScreen loadingScreen;


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

        _maxTime = 2.0f;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime >= _maxTime)
        {
            LoadMenu();
        }
	}

    void LoadMenu()
    {
        loadingScreen.loadMenu = true;
        Chartboost.cacheInterstitial(CBLocation.Default);
        Chartboost.showInterstitial(CBLocation.Default);
    }
}
