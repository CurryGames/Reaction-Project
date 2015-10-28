using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
        Application.LoadLevel(2);
    }

    public void LevelReaction()
    {
        Application.LoadLevel(1);
    }

    public void LevelColor()
    {
        Application.LoadLevel(3);
    }

    public void OptionsButton()
    {

    }
}
