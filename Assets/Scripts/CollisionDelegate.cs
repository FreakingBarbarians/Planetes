using UnityEngine;
using System.Collections;

public class CollisionDelegate : MonoBehaviour {
    public Planet planet;

    void OnCollisionEnter2D(Collision2D coll) {
        Debug.Log("Collided");
        planet.RpcAccreteObject(coll.gameObject);
    }

    void OnCollisionStay2D(Collision2D coll) {
        Debug.Log("Collided");
        planet.RpcAccreteObject(coll.gameObject);
    }
}
