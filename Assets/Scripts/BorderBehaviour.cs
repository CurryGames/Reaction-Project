using UnityEngine;
using System.Collections;

public class BorderBehaviour : MonoBehaviour {

    private Transform _myTrans;
    private Vector3 _scaleRate, _initScale;
    public BackgroundAnimation background;
    public Color color;
    SpriteRenderer _sprite;

	// Use this for initialization
	void Start () 
    {
        _myTrans = GetComponent<Transform>();
        _scaleRate = new Vector3(4, 4, 0);
        //_myTrans.localScale = new Vector3(32, 18, 1);
        color = background.currentColor;
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = color;
        _initScale = new Vector3(26, 26, 1);
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        _myTrans.localScale -= _scaleRate * Time.deltaTime;
        if (_myTrans.localScale.x <= 16)
        {
            color.a -= 1 * Time.deltaTime;
            _sprite.color = color;

            if (color.a <= 0)
            {
                Reset();
            }
        }

        
	}

    void Reset()
    {
        _myTrans.localScale = _initScale;
        color = background.currentColor;
        color.a = 1;
        _sprite.color = color;
    }
}
