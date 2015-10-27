using UnityEngine;
using System.Collections;

public class RayBehaviour : MonoBehaviour {

    Transform myTrans;
    public Color[] color;
    SpriteRenderer sprite;
    Vector3 initPos, initScale;
    private bool fading;
    private float fadeTimer = 0;

	// Use this for initialization
	void Start () 
    {
        myTrans = this.transform;
        sprite = GetComponent<SpriteRenderer>();
        //sprite.color = color[Random.Range(0, 5)];
        sprite.color = color[0];
        initPos = myTrans.position;
        initScale = myTrans.localScale;
        fading = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (myTrans.localScale.x < 3000)
        {
            // Ratio 200:1
            myTrans.localScale += new Vector3(1000, 0, 0) * Time.deltaTime;
            myTrans.Translate(5 * Time.deltaTime, 0, 0);
            
        }
        else fading = true;

        if (fading)
        {
            fadeTimer += Time.deltaTime;
            if (fadeTimer >= 3)
            {
                color[0].a -= 0.5f * Time.deltaTime;
                sprite.color = color[0];
                if (color[0].a <= 0) Restart();
            }          
        }

	}

    void Restart()
    {
        myTrans.position = initPos;
        myTrans.localScale = initScale;
        color[0].a = 1;
        sprite.color = color[0];
        fading = false;
        fadeTimer = 0;
    }
}
