using UnityEngine;
using System.Collections;

public class EnemyProjectile : Projectile {

    void Start() {
        ServerStartProjectile(Vector2.zero, null);
    }

    public override void ServerStartProjectile(Vector2 direction, GameObject shotBy) {
        projectileActive = true;
    }

    override public void ServerStartCelestialBody() {
        GameObject.Destroy(gameObject);
    }
}
