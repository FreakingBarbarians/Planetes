using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class BoundedAsteroidSpawner : AsteroidSpawner {

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log("NO");
        RpcDelete(other.gameObject);
    }

    void RpcDelete(GameObject g) {
        GameObject.Destroy(g);
    }

}
