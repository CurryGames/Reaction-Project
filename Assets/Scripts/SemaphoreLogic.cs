using UnityEngine;
using UnityEngine.UI;
using ChartboostSDK;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class SemaphoreLogic : MonoBehaviour {

    public enum SemaphoreState { START, PLAYING, WAITING, DEFEAT }


    public SemaphoreState state;
    public float currentTime, maxTime, reactionTime, totalReaction;
    public int lifes;
    public GameObject redSignal, greenSignal, startCanvas, defeatCanvas, waitingCanvas;
    public bool onGreen;
    public LayerMask targetMask, noTargetMask;
    public AudioManager audioManager;
    public NativeShare nativeShare;
    LoadingScreen loadingScreen;
    public float[] marks;
    public float reactPlayedNum, reactAverage;
    public Text[] marksText = new Text[5];
    public Text totalReactionText, waitingText, hsReaction;

	// Use this for initialization
	void Start () {
        marks = new float[5];
        for (int i = 0; i < 5; i++)
        {
            marksText[i].text = i + 1 +  ". -";
        }
        Chartboost.cacheInterstitial(CBLocation.Default);
        reactAverage = PlayerPrefs.GetFloat("ReactAverage");
        reactPlayedNum = PlayerPrefs.GetFloat("ReactPlayedNum");

        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreen>();
        loadingScreen.showAd++;

        reactPlayedNum++;
        redSignal.SetActive(true);
        greenSignal.SetActive(false);
        maxTime = Random.Range(1.0f, 3.5f);
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < lifes; i++)
        {
            if (marks[i] == 500) marksText[i].color = Color.red;
            else if (marks[i] >= 1) marksText[i].color = Color.green;
        }

        switch(state)
        {

            case SemaphoreState.START:
            {
                if ((Input.GetButtonDown("Fire1") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
                {
                    startCanvas.SetActive(false);
                    state = SemaphoreState.PLAYING;
                }
                break;
            }
            case SemaphoreState.PLAYING:
            {
                if (!onGreen)
                {
                    if (!redSignal.activeInHierarchy) redSignal.SetActive(true);
                    currentTime += Time.deltaTime*1000;
                    if (currentTime >= maxTime * 1000)
                    {
                        onGreen = true;
                        currentTime = 0;
                        reactionTime = 0.0f;
                        redSignal.SetActive(false);
                        greenSignal.SetActive(true);
                    }

                    if (Input.GetButtonDown("Fire1"))
                    {
                        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                        audioManager.Play(audioManager.error, audiSor, 1.0f);
                        reactionTime = 500;
                        Clicking();
                    }
                    else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                        audioManager.Play(audioManager.laser, audiSor, 1.0f);
                        reactionTime = 500;
                        Clicking();
                    }

                    }
                else
                {
                    reactionTime += Time.deltaTime * 1000;
                    if (Input.GetButtonDown("Fire1"))
                    {
                        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                        audioManager.Play(audioManager.laser, audiSor, 1.0f);
                        Clicking();
                    }
                    else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                        audioManager.Play(audioManager.laser, audiSor, 1.0f);
                        Clicking();
                    }
                }
                break;
            }
            case SemaphoreState.WAITING:
            {
                if (redSignal.activeInHierarchy) redSignal.SetActive(false);
                if (lifes > 4)
                {
                        if(loadingScreen.showAd >= 5)
                        {
                            Chartboost.showInterstitial(CBLocation.Default);   
                        }
                        defeatCanvas.SetActive(true);

                        for (int i = 1; i < 4; i++)
                    {
                        totalReaction += marks[i];
                    }

                    totalReaction /= lifes;
                    totalReactionText.text = "Average: " + totalReaction.ToString("000") + " ms";
                    reactAverage = ((reactAverage*(reactPlayedNum - 1)) + totalReaction) / reactPlayedNum;
                    state = SemaphoreState.DEFEAT;
                }
                else
                    {
                        
                        waitingCanvas.SetActive(true);
                        waitingText.text = "Your time:\n" + reactionTime.ToString("000") + " ms";

                        if ((Input.GetButtonDown("Fire1") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
                        {
                            waitingCanvas.SetActive(false);
                            currentTime = 0;
                            state = SemaphoreState.PLAYING;
                        }
                    }
                break;
            }
            case SemaphoreState.DEFEAT:
            {

                if (PlayerPrefs.GetFloat("SemaphoreHS") > totalReaction)
                {
                    PlayerPrefs.SetFloat("SemaphoreHS", totalReaction);

                    Social.ReportScore((long)totalReaction, "CgkI2s7ZnpIMEAIQAg", (bool success) =>
                    {
                        // handle success or failure
                    });
                }
                else if (PlayerPrefs.GetFloat("SemaphoreHS") == 0)
                {
                    PlayerPrefs.SetFloat("SemaphoreHS", totalReaction);

                    Social.ReportScore((long)totalReaction, "CgkI2s7ZnpIMEAIQAg", (bool success) =>
                    {
                        // handle success or failure
                    });
                }
                 

                    PlayerPrefs.SetFloat("ReactAverage",reactAverage);
                    PlayerPrefs.SetFloat("ReactPlayedNum", reactPlayedNum);

                    hsReaction.text = "Best Time: " + PlayerPrefs.GetFloat("SemaphoreHS").ToString("000") + " ms";
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel(0);
	}

    public void Reload()
    {
        Application.LoadLevel(2);
    }

    public void Menu()
    {
        Application.LoadLevel(1);
    }

    void Clicking()
    {

        int thisMark = lifes + 1;
        marks[lifes] = reactionTime;
        marksText[lifes].text = thisMark.ToString() + ". " + reactionTime.ToString("000");

        lifes++;
        //redSignal.SetActive(true);
        greenSignal.SetActive(false);
        maxTime = Random.Range(1.0f, 3.5f);

        state = SemaphoreState.WAITING;
        onGreen = false;
    }

    public void ShareScore()
    {
        nativeShare.ShareScreenshotWithText("I lasted " + currentTime.ToString("00.00") + " seconds in Reaction Booster!");
    }
}
