using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AimLogic : MonoBehaviour
{

    public Text timeTxt, maxTime;
    public GameObject defeatCanvas, startCanvas;
    public LayerMask layerMask, noTargetMask;

    private float currentTime;
    private AudioManager audioManager;
    public ObjectsArray targetArray;
    public bool playing;
    public bool defeat;
    public float spawnTime;
    private float delay;

    // Use this for initialization
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        playing = true;
        currentTime = 0;
        delay = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            currentTime += Time.deltaTime;
            timeTxt.text = "Time: " + currentTime.ToString("00.00");
            delay -= Time.deltaTime;
            if (delay <= 0) Spawn();

            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    targetArray.realocating = true;
                    AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                    audioManager.Play(audioManager.laser, audiSor, 1.0f);
                    TargetClicked();
                }
                /*else if (Physics.Raycast(ray, out hit, Mathf.Infinity, noTargetMask))
                {
                    playing = false;
                    Defeat();
                }*/
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    targetArray.realocating = true;
                    AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                    audioManager.Play(audioManager.laser, audiSor, 1.0f);
                    TargetClicked();
                }
                /*else if (Physics.Raycast(ray, out hit, Mathf.Infinity, noTargetMask))
                {
                    playing = false;
                    Defeat();
                }*/
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

    }

    void Spawn()
    {
        //targetArray.SpawnScalingTargets()
    }

    void TargetClicked()
    {
        //limitSlider.value = limitSlider.maxValue;
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
