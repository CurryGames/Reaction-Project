using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using ChartboostSDK;
using UnityEngine.SocialPlatforms;

public class LogoLogic : MonoBehaviour {

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

        Application.LoadLevel(1);

        Chartboost.cacheInterstitial(CBLocation.Default);
        Chartboost.showInterstitial(CBLocation.Default);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
