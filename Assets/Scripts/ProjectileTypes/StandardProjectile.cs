using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class StandardProjectile : Projectile {

	public float bulletDecayTime = 5f;
	private float time = 0;
	private ParticleSystem p;

	public override int getType ()
	{
		return Projectile.PROJECTILE_STANDARD;
	}

	void Update() {
		if (projectileActive) {
			if (time >= bulletDecayTime) {
				Physics2D.IgnoreCollision (shotBy.GetComponent<Collider2D> (), GetComponent<Collider2D> (), false);
				gameObject.layer = LayerMask.NameToLayer ("CelestialBodiesLayer");
				ServerStartCelestialBody ();
			}
			if (time <= bulletDecayTime) {
				time += Time.deltaTime;
			}
		}
	}


    void OnCollisionEnter2D(Collision2D coll) {
		if (projectileActive) {           
            coll.gameObject.GetComponent<CelestialBody>().ServerProjectileCollision(gameObject);
            ServerStartCelestialBody();
            Debug.Log("PLAY DAMNIT");
            GetComponent<AudioSource>().Play();
        }
    }
}
