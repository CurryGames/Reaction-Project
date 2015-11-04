using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArcadeLogic : MonoBehaviour {

    public Text timeTxt, limitTxt, maxTime, levelTxt, hsText;
    public GameObject defeatCanvas, startCanvas;
    public LayerMask layerMask, noTargetMask;
    public BackgroundAnimation background;
    public NativeShare nativeShare;

    private float arcadePlayedNum, arcadeTargetsNum, arcadeAverage;
    private float currentTime;
    private float limitTime;
    private AudioManager audioManager;
    public float levelTimer;
    public ObjectsArray targetArray;
    private int level;
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

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        playing = false;
        currentTime = 0;
        level = 1;
        limitTime = 1.5f;
        levelTimer = 0;
        levelTxt.text = "Level " + level.ToString();
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (playing)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 25)
            {
                audioManager.AccelerateSound();
                if (background.maxTime >= 0.5f) background.maxTime -= 0.0002f;
            }

            levelTimer += Time.deltaTime;
            if (targetArray.lifes <= 0)
            {
                playing = false;
                Defeat();
            }

            if (levelTimer >= 6) UpdateLevel();
    

            timeTxt.text = "Time: " + currentTime.ToString("00.00");
            limitTxt.text = "Limit: " + limitTime.ToString("0.000");

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

        if ((Input.GetButton("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && startCanvas.activeInHierarchy)
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

    void UpdateLevel()
    {
        level++;
        limitTime *= 0.95f;
        levelTimer = 0;
        levelTxt.text = "Level " + level.ToString();
    }

    void Defeat()
    {
        if (defeatCanvas.activeSelf == false) defeatCanvas.SetActive(true);
        maxTime.text = "You lasted " + currentTime.ToString("00.00") + " seconds!";

        if (PlayerPrefs.GetFloat("ArcadeHS") < currentTime)
        {
            PlayerPrefs.SetFloat("ArcadeHS", currentTime);
        }
        arcadeAverage = (arcadeAverage + currentTime) / arcadePlayedNum;
        PlayerPrefs.SetFloat("ArcadeAverage", arcadeAverage);
        PlayerPrefs.SetFloat("ArcadePlayedNum", arcadePlayedNum);
        PlayerPrefs.SetFloat("ArcadeTargetsNum", arcadeTargetsNum);

        hsText.text = "Best Time: " + PlayerPrefs.GetFloat("ArcadeHS").ToString();

    }

    public void Reload()
    {
        Application.LoadLevel(0);
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
        nativeShare.ShareScreenshotWithText("I lasted " + currentTime.ToString("00.00") + " seconds! in React-CurryGames");
    }

}
