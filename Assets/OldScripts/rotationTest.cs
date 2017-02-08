using UnityEngine;
using System.Collections;

public class rotationTest : MonoBehaviour {
	Vector2 up = Vector2.up;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (Vector2.zero, up);
		if (Input.GetKeyDown (KeyCode.D))
		up = myMathf.rotate (up, Mathf.Deg2Rad*1);
	}
}
