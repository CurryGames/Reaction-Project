using UnityEngine;
using System.Collections;

public class ObjectsArray : MonoBehaviour {

    public GameObject target;
    public ParticleSystem destroyParticles;
    public ParticleSystem[] destroyParticlesArray;
    //public GameObject noTarget;
    private GameObject[] m_targetArray;
    public bool playing;
    public float currentTime, maxTime;
    public int maxTarget;
    public Transform top, bot, left, rigth;
    private Vector3 m_position;
    public int lifes = 3;
    public GameObject[] lifesImage = new GameObject[3];

	// Use this for initialization
	void Start () {
        m_targetArray = new GameObject[maxTarget];
        destroyParticlesArray = new ParticleSystem[maxTarget];
        CreateArray(m_targetArray, target, destroyParticlesArray, destroyParticles);
        //ActivateTarget();
        maxTime = Random.Range(0.5f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (playing)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= maxTime)
            {
                ActivateTarget();
                currentTime = 0;
                maxTime = Random.Range(0.2f, 0.8f);
            }
        }
	}

    void CreateArray(GameObject[] tArray, GameObject tGameObject, ParticleSystem[] pArray, ParticleSystem pSystem)
    {
        for (int i = 0; i < maxTarget; i++)
        {
            GameObject t;
            ParticleSystem p;
            t = (GameObject)Instantiate(tGameObject, Vector3.zero, Quaternion.identity);
            p = (ParticleSystem)Instantiate(pSystem, Vector3.zero, Quaternion.identity);
            //targetAnimation[i] = t.GetComponent<TargetAnimation>();
            //m_material[i] = t.GetComponent<SpriteRenderer>();
            t.SetActive(false);
            tArray[i] = t;
            pArray[i] = p;
        }
    }

    public void ActivateTarget()
    {
        int counter = 0;
        //currentTime += Time.deltaTime;
        
        
        for (int i = 0; i < maxTarget; i++)
        {
            if (counter < 1 && !m_targetArray[i].activeInHierarchy)
            {
                m_position = new Vector3((int)Random.Range(left.position.x, rigth.position.x), (int)Random.Range(bot.position.y, top.position.y), 0);
                m_targetArray[i].transform.position = m_position;
                m_targetArray[i].SetActive(true);
                //tArray[i].GetComponent<SpriteRenderer>().color = targetColor[Random.Range(0, targetColor.Length)];
                counter++;
            }
        }
        //currentTime = 0;
        //realocating = false;
   
    }

    public void ActivateParticles(Vector3 position)
    {

        int counter = 0;

        for (int i = 0; i < maxTarget; i++)
        {
            if (counter < 1 && !destroyParticlesArray[i].isPlaying)
            {
                destroyParticlesArray[i].transform.position = position;
                destroyParticlesArray[i].Play();
                //tArray[i].GetComponent<SpriteRenderer>().color = targetColor[Random.Range(0, targetColor.Length)];
                counter++;
            }
        }
    }

    public void RestLife()
    {
        lifes--;
        if(lifes >= 0)lifesImage[lifes].SetActive(false);
    }
}
