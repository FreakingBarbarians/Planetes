using UnityEngine;
using System.Collections;

public class TurnOffAfter : MonoBehaviour {
    public float time;
    public float timer;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer >= time) {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
	}
}
