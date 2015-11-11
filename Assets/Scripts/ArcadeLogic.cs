using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ChartboostSDK;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class ArcadeLogic : MonoBehaviour {

    public Text timeTxt, maxTime, recordTxt, hsText;
    public GameObject defeatCanvas, startCanvas;
    public LayerMask layerMask, noTargetMask;
    public BackgroundAnimation background;
    public NativeShare nativeShare;
    LoadingScreen loadingScreen;
    private float arcadePlayedNum, arcadeTargetsNum, arcadeAverage;
    private float currentTime;
    private float gameOverTime;
    bool gameOverSoundDisplay;
    private AudioManager audioManager;
    public ObjectsArray targetArray;
    public bool playing;
    public bool defeat;

	// Use this for initialization
	void Start () 
    {
        /*if (PlayerPrefs.GetFloat("ArcadeHS") == null)
        {
            PlayerPrefs.SetFloat("ArcadeHS", 0);

        }*/
        arcadePlayedNum = PlayerPrefs.GetFloat("ArcadePlayedNum");
        arcadeAverage = PlayerPrefs.GetFloat("ArcadeAverage");
        arcadeTargetsNum = PlayerPrefs.GetFloat("ArcadeTargetsNum");
        arcadePlayedNum++;

        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreen>();
        loadingScreen.showAd+=2;

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        playing = false;
        currentTime = 0;
        recordTxt.text = "Record: " + PlayerPrefs.GetFloat("ArcadeHS").ToString("00.00");
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (playing)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 20)
            {
                audioManager.AccelerateSound();
                if (background.maxTime >= 0.5f) background.maxTime -= 0.0004f;
            }

            if (targetArray.lifes <= 0)
            {
                playing = false;
                audioManager.acSound = false;
                audioManager.StopMusic();
                AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                audioManager.Play(audioManager.explosion, audiSor, 1.0f);
                Defeat();
            }

            recordTxt.text = "Record: " + PlayerPrefs.GetFloat("ArcadeHS").ToString("00.00"); 
            timeTxt.text = "Time: " + currentTime.ToString("00.00");

            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Clicking(ray);

            }
            else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                Clicking(ray);
            }


        }
        else if(!playing && !startCanvas.activeInHierarchy)
        {
            gameOverTime += Time.deltaTime;

            if(gameOverTime >= audioManager.explosion.length && !gameOverSoundDisplay)
            {
                AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                audioManager.PlayLoop(audioManager.bassTone, audiSor, 1.0f);
                gameOverSoundDisplay = true;
            }
        }

        if ((Input.GetButton("Fire1") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && startCanvas.activeInHierarchy)
        {
            startCanvas.SetActive(false);
            playing = true;
            targetArray.playing = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel(0);
        
	}

    void TargetClicked()
    {
        arcadeTargetsNum++;
    }
        
    void Defeat()
    {
        if (defeatCanvas.activeSelf == false) defeatCanvas.SetActive(true);
        maxTime.text = "You lasted " + currentTime.ToString("00.00") + " seconds!";

        if (loadingScreen.showAd >= 5)
        {
            Chartboost.showInterstitial(CBLocation.Default);
        }

        if (PlayerPrefs.GetFloat("ArcadeHS") < currentTime)
        {
            PlayerPrefs.SetFloat("ArcadeHS", currentTime);

            Social.ReportScore((long)(currentTime*100), "CgkI2s7ZnpIMEAIQCA", (bool success) => {
                // handle success or failure
            });

        }

        arcadeAverage = (arcadeAverage* (arcadePlayedNum-1) + currentTime) / arcadePlayedNum;

        PlayerPrefs.SetFloat("ArcadeAverage", arcadeAverage);
        PlayerPrefs.SetFloat("ArcadePlayedNum", arcadePlayedNum);
        PlayerPrefs.SetFloat("ArcadeTargetsNum", arcadeTargetsNum);

        

        hsText.text = "Best Time: " + PlayerPrefs.GetFloat("ArcadeHS").ToString("00.00");

    }

    public void LevelReload()
    {
        Application.LoadLevel(3);
    }

    public void LevelMenu()
    {
        Application.LoadLevel(1);
    }

    public void Clicking(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            //targetArray.realocating = true;
            targetArray.ActivateParticles(hit.transform.gameObject.transform.position);
            hit.transform.parent.gameObject.SetActive(false);
            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            audioManager.Play(audioManager.laser, audiSor, 1.0f);
            TargetClicked();
        }
        
    }

    public void ShareScore()
    {
        nativeShare.ShareScreenshotWithText("I lasted " + currentTime.ToString("00.00") + " seconds in Reaction Booster!");
    }

}
