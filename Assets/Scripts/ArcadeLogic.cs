using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArcadeLogic : MonoBehaviour {

    public Text timeTxt, limitTxt;
    public Slider limitSlider;

    private float currentTime;
    private float limitTime;
    private float currentLimit;

	// Use this for initialization
	void Start () 
    {
        currentTime = 0;
        limitTime = 1.5f;
        currentLimit = limitTime;
        limitSlider.maxValue = limitTime;
        limitSlider.value = limitSlider.maxValue;
	}
	
	// Update is called once per frame
	void Update () 
    {
        currentTime += Time.deltaTime;
        limitSlider.value -= Time.deltaTime;
        if (limitSlider.value <= 0)
        {
            limitSlider.value = limitSlider.maxValue;
        }

        timeTxt.text = "Time: " + currentTime.ToString("00.00");
        limitTxt.text = "Limit: " + currentLimit.ToString("0.000") + "s";
	}
}
