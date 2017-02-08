using UnityEngine;
using System.Collections;

public class enableafter : MonoBehaviour {

    public GameObject enableObject;
    public float time;
    public float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer >= time) {
            enableObject.SetActive(true);
            Destroy(this);
        }
        timer += Time.deltaTime;
	}
}
