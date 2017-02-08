using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Warphole : MonoBehaviour {

    public Warphole exit;
    public List<GameObject> recieving = new List<GameObject>();


    void OnTriggerEnter2D(Collider2D coll) {
        
        if (recieving.Contains(coll.gameObject)) {
            return;
        }

        Debug.Log(coll.gameObject.name);

        exit.recieving.Add(coll.gameObject);
        coll.gameObject.transform.root.position = exit.transform.position;
    }

    void OnTriggerExit2D(Collider2D coll) {
        recieving.Remove(coll.gameObject);
    }
}
