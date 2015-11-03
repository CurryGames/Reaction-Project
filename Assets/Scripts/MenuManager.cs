using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    LoadingScreen loadingScreen;

	// Use this for initialization
	void Start () {
        loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreen>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void PlayButton() 
    {

    }

    public void LevelAim()
    {
        loadingScreen.loadLevel2 = true;
    }

    public void LevelReaction()
    {
        loadingScreen.loadLevel1 = true;
    }


    public void OptionsButton()
    {

    }
}
