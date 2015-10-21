using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArcadeLogic : MonoBehaviour {

    public Text timeTxt, limitTxt, maxTime, levelTxt;
    public Slider limitSlider;
    public GameObject defeatCanvas, startCanvas;
    public LayerMask layerMask;

    private float currentTime;
    private float limitTime;
    public float levelTimer;
    public ObjectsArray targetArray;
    private int level;
    public bool playing;
    public bool defeat;

	// Use this for initialization
	void Start () 
    {
        playing = false;
        currentTime = 0;
        level = 1;
        limitTime = 1.5f;
        levelTimer = 0;
        limitSlider.maxValue = limitTime;
        limitSlider.value = limitSlider.maxValue;
        levelTxt.text = "Level " + level.ToString();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (playing)
        {
            currentTime += Time.deltaTime;
            limitSlider.value -= Time.deltaTime;
            levelTimer += Time.deltaTime;
            if (limitSlider.value <= 0)
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
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    targetArray.realocating = true;
                    TargetClicked();
                }
                
            }
            else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    targetArray.realocating = true;
                    TargetClicked();
                }

            }

        }

        if ((Input.GetButton("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            startCanvas.SetActive(false);
            playing = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit(); 
        
	}

    void TargetClicked()
    {
        limitSlider.value = limitSlider.maxValue;
    }

    void UpdateLevel()
    {
        level++;
        limitTime *= 0.95f;
        limitSlider.maxValue = limitTime;
        levelTimer = 0;
        levelTxt.text = "Level " + level.ToString();
    }

    void Defeat()
    {
        if (defeatCanvas.activeSelf == false) defeatCanvas.SetActive(true);
        maxTime.text = "You lasted " + currentTime.ToString("00.00") + " seconds!";
    }

    public void Reload()
    {
        Application.LoadLevel(0);
    }

}
