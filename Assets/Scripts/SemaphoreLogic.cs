using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SemaphoreLogic : MonoBehaviour {

    public enum SemaphoreState { START, PLAYING, WAITING, DEFEAT }


    public SemaphoreState state;
    public float currentTime, maxTime, reactionTime, totatlReaction;
    public int lifes;
    public GameObject redSignal, greenSignal, startCanvas, defeatCanvas, waitingCanvas;
    public bool onGreen;
    public LayerMask targetMask, noTargetMask;
    public AudioManager audioManager;
    public NativeShare nativeShare;
    public float[] marks;
    public Text[] marksText = new Text[5];
    public Text totalReactionText, waitingText, hsReaction;

	// Use this for initialization
	void Start () {
        marks = new float[5];
        redSignal.SetActive(true);
        greenSignal.SetActive(false);
        maxTime = Random.Range(1.0f, 3.5f);
	}
	
	// Update is called once per frame
	void Update () {

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
                    currentTime += Time.deltaTime*1000;
                    if (currentTime >= maxTime * 1000)
                    {
                        onGreen = true;
                        currentTime = 0;
                        reactionTime = 0.0f;
                        redSignal.SetActive(false);
                        greenSignal.SetActive(true);
                    }
                }
                else
                {
                    reactionTime += Time.deltaTime * 1000;
                }

                if (Input.GetButtonDown("Fire1"))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Clicking(ray);
                }
                else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    Clicking(ray);
                }
                break;
            }
            case SemaphoreState.WAITING:
            {
                if (lifes > 4)
                {
                    defeatCanvas.SetActive(true);
                    for (int i = 0; i <= 4; i++)
                    {
                        totatlReaction += marks[i];
                    }
                    totatlReaction /= 5;
                    redSignal.SetActive(false);
                    totalReactionText.text = "Average: " + totatlReaction.ToString("000") + "ms";
                    state = SemaphoreState.DEFEAT;
                }
                else
                    {
                        waitingCanvas.SetActive(true);
                        waitingText.text = "Your time:\n" + reactionTime.ToString("000") + "ms";
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
                    if (PlayerPrefs.GetFloat("SemaphoreHS") < currentTime)
                    {
                        PlayerPrefs.SetFloat("SemaphoreHS", currentTime);
                    }

                    hsReaction.text = "Best Time: " + PlayerPrefs.GetFloat("SemaphoreHS").ToString();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel(0);
	}

    public void Reload()
    {
        Application.LoadLevel(0);
    }

    void Clicking(Ray ray)
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetMask))
        {
            AudioSource audiSor = gameObject.AddComponent<AudioSource>();
            audioManager.Play(audioManager.laser, audiSor, 1.0f);
        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, noTargetMask))
        {
            reactionTime = 500;
        }

        int thisMark = lifes + 1;
        marks[lifes] = reactionTime;
        marksText[lifes].text = thisMark.ToString() + ". " + reactionTime.ToString("000");
        lifes++;
        redSignal.SetActive(true);
        greenSignal.SetActive(false);
        maxTime = Random.Range(1.0f, 3.5f);

        state = SemaphoreState.WAITING;
        onGreen = false;
    }

    public void ShareScore()
    {
        nativeShare.ShareScreenshotWithText("I lasted " + currentTime.ToString("00.00") + " seconds! in React-CurryGames");
    }
}
