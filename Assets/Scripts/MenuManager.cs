using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayButton() 
    {

    }

    public void ArcadeButton()
    {
        Application.LoadLevel("ArcadeLevel");
    }

    public void OptionsButton()
    {

    }
}
