using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    Transform myTransform;

	// Use this for initialization
	void Start () {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        myTransform.Rotate(new Vector3(0, 0, 6 * Time.deltaTime));
	}
}
