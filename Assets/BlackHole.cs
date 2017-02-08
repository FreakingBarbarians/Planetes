using UnityEngine;
using System.Collections;

public class BlackHole : CelestialBody {
    void OnCollisionEnter2D(Collision2D coll) {
        RpcAccreteObject(coll.gameObject);
    }
}
