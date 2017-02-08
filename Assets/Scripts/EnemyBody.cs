using UnityEngine;
using System.Collections;

public class EnemyBody : CelestialBody {
    public override void ServerProjectileCollision(GameObject projectile) {
        GameObject.Destroy(this);
    }
}
