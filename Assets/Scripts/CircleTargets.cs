﻿using UnityEngine;
using System.Collections;

public class CircleTargets : MonoBehaviour {

    SpriteRenderer fillCircle;
    Color color;
    ObjectsArray targetArray;
    public GameObject parentGameObject;
    private Transform m_myTransform;
    //public LayerMask layerMask;

    float duration;
    bool stoped = false;


    // Use this for initialization
    void Start () 
    {
        m_myTransform = transform;
        fillCircle = GetComponent<SpriteRenderer>();
        targetArray = GameObject.FindGameObjectWithTag("Spawn").GetComponent<ObjectsArray>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_myTransform.localScale -= new Vector3(0.2f, 0.2f, 0) * Time.deltaTime;

        if(m_myTransform.localScale.x <= 0)
        {
            m_myTransform.localScale = new Vector3(1, 1, 1);
            targetArray.ActivateParticles(parentGameObject.transform.position);
            parentGameObject.SetActive(false);
        }
        /*
        if (!stoped)
        {
            
            
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    //targetArray.realocating = true;
                    AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                    //audioManager.Play(audioManager.laser, audiSor, 1.0f);
                    TargetClicked();
                }
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    //targetArray.realocating = true;
                    AudioSource audiSor = gameObject.AddComponent<AudioSource>();
                    //audioManager.Play(audioManager.laser, audiSor, 1.0f);
                    TargetClicked();
                }
            }
            
        }
        */
	}
    
   
    /*
    void OnDisable()
    {
        
        
    }

    
    void TargetClicked()
    {
        stoped = true;
        //activate click animation
        Debug.Log("clicked!");
    }*/
}
