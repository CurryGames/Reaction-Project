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
    public float[] marks;
    public Text[] marksText = new Text[5];
    public Text totalReactionText, waitingText;

	// Use this for initialization
	void Start () {
        marks = new float[5];
        redSignal.SetActive(true);
        greenSignal.SetActive(false);
        maxTime = Random.Range(1.0f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {

        switch(state)
        {

            case SemaphoreState.START:
            {
                if ((Input.GetButton("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
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
                    currentTime += Time.deltaTime;
                    if (currentTime >= maxTime)
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
                    reactionTime += Time.deltaTime;
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
                    totalReactionText.text = "Total: " + totatlReaction.ToString("0.00") + "S";
                    state = SemaphoreState.DEFEAT;
                }
                else
                    {
                        waitingCanvas.SetActive(true);
                        waitingText.text = "total sore:\n" + reactionTime.ToString("0.00");
                        if ((Input.GetButton("Jump") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
                        {
                            waitingCanvas.SetActive(false);
                            state = SemaphoreState.PLAYING;
                        }
                    }
                break;
            }
            case SemaphoreState.DEFEAT:
            {
                break;
            }
        }

        

        

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit(); 
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
            reactionTime = 2.0f;
        }

        int thisMark = lifes + 1;
        marks[lifes] = reactionTime;
        marksText[lifes].text = thisMark.ToString() + ". " + reactionTime.ToString("0.00") + "S";
        lifes++;
        redSignal.SetActive(true);
        greenSignal.SetActive(false);
        maxTime = Random.Range(1.0f, 2.0f);
        state = SemaphoreState.WAITING;
        onGreen = false;
    }
}
