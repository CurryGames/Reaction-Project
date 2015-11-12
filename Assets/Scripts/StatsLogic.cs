using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Collections;

public class StatsLogic : MonoBehaviour {

    public Vector2 initScale, finalScale, currentScale;
    public float currentFrame, maxFrame;
    public bool scaling;
    public GameObject statButtons;
    private Transform m_myTransform;
    private NativeShare nativeShare;
    public Text reactBest, reactAverage, aimBest, aimTargets, aimAverage;

	// Use this for initialization
	void Start ()
    {
        m_myTransform = transform;
        initScale = new Vector2(0, 0);
	    finalScale = transform.localScale;
        m_myTransform.localScale = initScale;
        nativeShare = GetComponent<NativeShare>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        reactBest.text = "Record: " + PlayerPrefs.GetFloat("SemaphoreHS").ToString("000") + " ms";
        reactAverage.text = "Average: " + PlayerPrefs.GetFloat("ReactAverage").ToString("000") + " ms";
        aimBest.text = "Record: " + PlayerPrefs.GetFloat("ArcadeHS").ToString("00.00") + " s";
        aimTargets.text = "Targets: " + PlayerPrefs.GetFloat("ArcadeTargetsNum").ToString("0");
        aimAverage.text = "Average: " + PlayerPrefs.GetFloat("ArcadeAverage").ToString("00.00") + " s";

        if (scaling)
        {
            statButtons.SetActive(false);
            currentFrame++;
            if(currentFrame < maxFrame*60 )
            {
                currentScale.x = (float)Easing.CircEaseIn(currentFrame, initScale.x, finalScale.x - initScale.x, maxFrame * 60);
                currentScale.y = (float)Easing.CircEaseIn(currentFrame, initScale.y, finalScale.y - initScale.y, maxFrame * 60);
            }
            else
            {
                currentFrame = 0;
                statButtons.SetActive(true);
                scaling = false;
            }

            m_myTransform.localScale = currentScale;
        }
	}

    public void Reset()
    {
        PlayerPrefs.SetFloat("SemaphoreHS", 000);
        PlayerPrefs.SetFloat("ReactAverage", 0);
        PlayerPrefs.SetFloat("ReactPlayedNum", 0);
        PlayerPrefs.SetFloat("ArcadeHS", 0);
        PlayerPrefs.SetFloat("ArcadeTargetsNum", 0);
        PlayerPrefs.SetFloat("ArcadeAverage", 0);
        PlayerPrefs.SetFloat("ArcadeAverage", 0);
        PlayerPrefs.SetFloat("ArcadePlayedNum", 0);
    }

    public void Ranking()
    {
        Social.ShowLeaderboardUI();
    }

    public void Share()
    {
        nativeShare.ShareScreenshotWithText("Look my Stats in React-CurryGames");
    }

    void OnDisable()
    {
        m_myTransform.localScale = initScale;
        scaling = true;
    }
}
