using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DeleteOnEnter : NetworkBehaviour {
    void OnTriggerEnter2D(Collider2D coll) {
        GameObject.Destroy(coll.gameObject);
    }
}
