using UnityEngine;
using System.Collections;

public class ObjectsArray : MonoBehaviour {

    public GameObject target;
    private GameObject[] m_targetArray;
    public Color[] targetColor;
    public int maxTarget;
    public float minTime, maxTime;
    public Transform top, bot, left, rigth;
    private Vector3 m_position;
    private SpriteRenderer[] m_material;
    private float m_currentTime;
    private float m_maxTime;

	// Use this for initialization
	void Start () {
        m_targetArray = new GameObject[maxTarget];
        CreateArray(m_targetArray, target);
        m_maxTime = Random.Range(minTime, maxTime);
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_currentTime += Time.deltaTime;
        if(m_currentTime >= m_maxTime)
        {
            ActivateTarget(m_targetArray);
            m_maxTime = Random.Range(minTime, maxTime);
            m_currentTime = 0.0f;
        }
	}

    void CreateArray(GameObject[] tArray, GameObject tGameObject)
    {
        for (int i = 0; i < maxTarget; i++)
        {
            GameObject t = (GameObject)Instantiate(tGameObject, Vector3.zero, Quaternion.identity);
            //m_material[i] = t.GetComponent<SpriteRenderer>();
            t.SetActive(false);
            tArray[i] = t;
        }
    }

    void ActivateTarget(GameObject[] tArray)
    {
        int counter = 0;
        m_position = new Vector3(Random.Range(left.position.x, rigth.position.x), Random.Range(bot.position.y, top.position.y), 0);

        for (int i = 0; i < maxTarget; i++)
        {
            if (!tArray[i].activeInHierarchy && counter < 1)
            {

                tArray[i].transform.position = m_position;
                tArray[i].SetActive(true);
                tArray[i].GetComponent<SpriteRenderer>().color = targetColor[Random.Range(0, targetColor.Length)];
                counter++;
            }
        }
    }
}
