using UnityEngine;
using System.Collections;

public class SameScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = transform.root.localScale;
	}
}
