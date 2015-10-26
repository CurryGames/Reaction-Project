using UnityEngine;
using System.Collections;

public class BackgroundAnimation : MonoBehaviour {

    public Color[] backgroundColors;
    public Color currentColor, finalColor, initColor;
    public float currentTime, maxTime;
    public int randomColor;
    //public bool down;
    private Renderer m_myRenderer;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
        finalColor = backgroundColors[Random.Range(0, backgroundColors.Length)];
        m_myRenderer = GetComponent<Renderer>();
        initColor = m_myRenderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
        currentTime++;
        if(currentTime < maxTime*60)
        {
            currentColor.r = (float)Easing.Linear(currentTime, initColor.r, finalColor.r - initColor.r, maxTime * 60.0f);
            currentColor.b = (float)Easing.Linear(currentTime, initColor.b, finalColor.b - initColor.b, maxTime * 60.0f);
            currentColor.g = (float)Easing.Linear(currentTime, initColor.g, finalColor.g - initColor.g, maxTime * 60.0f);
        }
        else
        {
            randomColor = Random.Range(0, backgroundColors.Length);
            initColor = currentColor;
            while (finalColor == backgroundColors[randomColor])
            {
                randomColor = Random.Range(0, backgroundColors.Length);
            }
            finalColor = backgroundColors[randomColor];
            currentTime = 0;
        }

        m_myRenderer.material.color = currentColor;
	}
}
