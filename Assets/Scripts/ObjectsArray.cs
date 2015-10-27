using UnityEngine;
using System.Collections;

public class ObjectsArray : MonoBehaviour {

    public GameObject target;
    public GameObject noTarget;
    private GameObject[] m_targetArray;
    private TargetAnimation[] targetAnimation;
    public bool realocating;
    float currentTime;
    public int maxTarget;
    public Transform top, bot, left, rigth;
    private Vector3 m_position;

	// Use this for initialization
	void Start () {
        m_targetArray = new GameObject[maxTarget];
        targetAnimation = new TargetAnimation[maxTarget];
        CreateArray(m_targetArray, target, noTarget);
        ActivateTarget();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (realocating) ActivateTarget();
	}

    void CreateArray(GameObject[] tArray, GameObject tGameObject, GameObject dGameObject)
    {
        for (int i = 0; i < maxTarget; i++)
        {
            GameObject t;
            if(i == 0)t = (GameObject)Instantiate(tGameObject, Vector3.zero, Quaternion.identity);
            else t = (GameObject)Instantiate(dGameObject, Vector3.zero, Quaternion.identity);
            targetAnimation[i] = t.GetComponent<TargetAnimation>();
            //m_material[i] = t.GetComponent<SpriteRenderer>();
            //t.SetActive(false);
            tArray[i] = t;
        }
    }

    public void ActivateTarget()
    {
        //int counter = 0;
        currentTime += Time.deltaTime;
        for (int i = 0; i < maxTarget; i++)
        {
            targetAnimation[i].desactivate = true;
        }
        
        if(currentTime >= targetAnimation[0].desactivateParticles.duration)
        {
            for (int i = 0; i < maxTarget; i++)
            {
                m_position = new Vector3((int)Random.Range(left.position.x, rigth.position.x), (int)Random.Range(bot.position.y, top.position.y), 0);
                m_targetArray[i].transform.position = m_position;
                m_targetArray[i].SetActive(true);
                //tArray[i].GetComponent<SpriteRenderer>().color = targetColor[Random.Range(0, targetColor.Length)];
                //counter++;
            }
            currentTime = 0;
            realocating = false;
        }      
    }

}
