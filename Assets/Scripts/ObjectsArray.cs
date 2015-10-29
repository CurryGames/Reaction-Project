using UnityEngine;
using System.Collections;

public class ObjectsArray : MonoBehaviour {

    public GameObject target;
    public ParticleSystem destroyParticles;
    public ParticleSystem[] destroyParticlesArray;
    //public GameObject noTarget;
    private GameObject[,] m_targetArray;
    public bool playing;
    public float currentTime, maxTime, maxRan, minRan;
    public int maxTarget;
    public Transform top, bot, left, rigth;
    //private Vector3 m_position;
    //private Vector3[] m_positionArray;
    public int lifes = 3;
    public Vector2 wordSize = new Vector2 (8,4);
    public GameObject[] lifesImage = new GameObject[3];

	// Use this for initialization
	void Start () {
        maxTarget = (int)(wordSize.x * wordSize.y);
        m_targetArray = new GameObject[(int)wordSize.x, (int)wordSize.y];
        destroyParticlesArray = new ParticleSystem[maxTarget];
        //m_positionArray = new Vector3[maxTarget];
        CreateArray(m_targetArray, target, destroyParticlesArray, destroyParticles);
        //ActivateTarget();
        minRan = 0.45f;
        maxRan = 0.8f;
        maxTime = Random.Range(minRan, maxRan);
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
                maxTime = Random.Range(minRan, maxRan);
            }

            if (lifes <= 0) playing = false;

            if (minRan >= 0.2f) minRan -= 0.005f * Time.deltaTime;
            if (maxRan >= 0.2) maxRan -= 0.007f * Time.deltaTime;
        }
	}

    void CreateArray(GameObject[,] tArray, GameObject tGameObject, ParticleSystem[] pArray, ParticleSystem pSystem)
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * wordSize.x / 2 - Vector3.up * wordSize.y / 2;

        for (int x = 0; x < wordSize.x; x++)
        {
            for (int y = 0; y < wordSize.y; y++)
            {
                GameObject t;
                ParticleSystem p;
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * 2 + 1f) + Vector3.up * (y * 2 + 1f);
                t = (GameObject)Instantiate(tGameObject, worldPoint, Quaternion.identity);
                p = (ParticleSystem)Instantiate(pSystem, Vector3.one, Quaternion.identity);
                
                //t.transform.position = worldPoint;

                //targetAnimation[i] = t.GetComponent<TargetAnimation>();
                //m_material[i] = t.GetComponent<SpriteRenderer>();
                t.SetActive(false);

                tArray[x, y] = t;
                pArray[x] = p;
                
            }
        }

    }

    public void ActivateTarget()
    {
        int counter = 0;
        //currentTime += Time.deltaTime;
        int x = Mathf.FloorToInt( Random.Range(0f, wordSize.x));
        int y = Mathf.FloorToInt(Random.Range(0f, wordSize.y));

        if (counter < 1 && !m_targetArray[x, y].activeInHierarchy)
        {
            //m_position = new Vector3(Random.Range((int)left.position.x, (int)rigth.position.x), Random.Range((int)bot.position.y, (int)top.position.y), 0);
            //m_positionArray[i] = m_position;

            //m_targetArray[i].transform.position = m_position;
            m_targetArray[x, y].SetActive(true);

            //tArray[i].GetComponent<SpriteRenderer>().color = targetColor[Random.Range(0, targetColor.Length)];
            counter++;
        }
        else if(m_targetArray[x, y].activeInHierarchy)
        {
            x = Mathf.FloorToInt(Random.Range(0f, wordSize.x));
            y = Mathf.FloorToInt(Random.Range(0f, wordSize.y));
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
