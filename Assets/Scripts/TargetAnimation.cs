using UnityEngine;
using System.Collections;

public class TargetAnimation : MonoBehaviour {

    public ParticleSystem activateParticle, desactivateParticles;
    public GameObject targetSprite;
    public float currentTime;
    public bool activate;
    public bool desactivate;

	// Use this for initialization
	void Start () {
        activate = true;
	}

    void OnActive()
    {
        activate = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (activate) ActivateAnimation();
        //if (desactivate) DesactivateAnimation();
	}

    void ActivateAnimation()
    {
        targetSprite.SetActive(false);
        activateParticle.Play();
        currentTime += Time.deltaTime;
        if(currentTime >= activateParticle.duration)
        {
            targetSprite.SetActive(true);
            activateParticle.Stop();
            currentTime = 0.0f;
            activate = false;
        }
    }

   /* void DesactivateAnimation()
    {
        targetSprite.SetActive(false);
        desactivateParticles.Play();
        currentTime += Time.deltaTime;
        if (currentTime >= desactivateParticles.duration)
        {
            desactivateParticles.Stop();
            currentTime = 0.0f;
            activate = true;
            desactivate = false;
        }
    }*/
}
