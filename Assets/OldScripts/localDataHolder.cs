using UnityEngine;
using System.Collections;

public class localDataHolder : MonoBehaviour {

    public string playerName;
    public static localDataHolder localData;

    void Awake() {
        localData = this;
    } 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
