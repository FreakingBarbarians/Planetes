using UnityEngine;
using System.Collections;

public class DisableAfter : MonoBehaviour {

    public float time;
    private float timer;

    public GameObject toDisable;


	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > time) {
            toDisable.SetActive(false);
        }
	}
}
