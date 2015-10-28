using UnityEngine;
using System.Collections;

public class ReScaleOnActive : MonoBehaviour {

    public GameObject childObject;

    // Use this for initialization
    void OnEnable()
    {
        childObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
