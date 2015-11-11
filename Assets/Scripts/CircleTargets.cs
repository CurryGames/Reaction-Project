using UnityEngine;
using System.Collections;

public class CircleTargets : MonoBehaviour {

    ObjectsArray targetArray;
    public GameObject parentGameObject;
    private Transform m_myTransform;
    private AudioManager audioManager;
    private FailBackground failBackground;
    //public LayerMask layerMask;

    public float duration;


    // Use this for initialization
    void Start () 
    {
        m_myTransform = transform;
        targetArray = GameObject.FindGameObjectWithTag("Spawn").GetComponent<ObjectsArray>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        failBackground = GameObject.FindGameObjectWithTag("FailBackground").GetComponent<FailBackground>();

    }
	
	// Update is called once per frame
	void Update () 
    {
        m_myTransform.localScale -= new Vector3(1/duration, 1/duration, 0) * Time.deltaTime;

        if(m_myTransform.localScale.x <= 0)
        {
            targetArray.RestLife();
            m_myTransform.localScale = new Vector3(1, 1, 1);
            targetArray.ActivateParticles(parentGameObject.transform.position);
            parentGameObject.SetActive(false);
            AudioSource audiSor = audioManager.gameObject.AddComponent<AudioSource>();
            if (failBackground.animActive) failBackground.ResetAnim();
            else failBackground.animActive = true;
            audioManager.Play(audioManager.error, audiSor, 1.0f);
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
