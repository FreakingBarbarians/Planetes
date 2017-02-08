using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class LobbySelectionField : NetworkBehaviour {
    public List<GameObject> collisions = new List<GameObject>();
    public ListNames nameList;

    void OnTriggerEnter2D(Collider2D coll) {
        if (!isServer) {
            return;
        }

        nameList.addName(coll.gameObject.transform.root.gameObject);
    }

    void OnTriggerExit2D(Collider2D coll) {
        if (!isServer) {
            return;
        }

        nameList.removeName(coll.gameObject.transform.root.gameObject);
    }
}
