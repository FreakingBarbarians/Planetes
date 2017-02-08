using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class SplitProjectile : Projectile {

	public GameObject splitProjectilePrefab;
	public int averageSplits;
	public float splitSpeedMultiplier;
	public float splitAngle;


	private CelestialBody cb;
	private float decayTime = 0.3f;
	private float timer = 0f;

	public override int getType ()
	{
		return PROJECTILE_SPLIT;
	}

	void Start(){
		cb = GetComponent<CelestialBody> ();
	}

	void Update(){
		if (projectileActive) {
			timer += Time.deltaTime;
			if (timer >= decayTime) {
				ServerSplit();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (projectileActive) {
			coll.collider.gameObject.GetComponent<CelestialBody> ().ServerProjectileCollision (gameObject);
            ServerStartCelestialBody();
            Debug.Log("PLAY DAMNIT");
            GetComponent<AudioSource>().Play();
        }
	}

	private void ServerSplit(){
		List<GameObject> splitObjects = new List<GameObject> ();

		float mass = cb.mass;
		float size = cb.size;

		float massRemaining = mass;
		float sizeRemaining = size;

		float averageMass = mass / averageSplits;
		float averageSize = size / averageSplits;

		while (massRemaining > 0 && sizeRemaining > 0) {
			float tmass = averageMass + Random.Range (-averageMass / 2, averageMass);
			float tsize = averageSize + Random.Range (-averageSize / 2, averageSize / 2);

			if (tmass > massRemaining) {
				tmass = massRemaining;
			} 
			if (tsize > sizeRemaining) {
				tsize = sizeRemaining;
			}

			sizeRemaining -= tsize;
			massRemaining -= tmass;

			GameObject splitProjectile = GameObject.Instantiate<GameObject> (splitProjectilePrefab);

            NetworkServer.Spawn(splitProjectile);

            CelestialBody spcb = splitProjectile.GetComponent<CelestialBody> ();
			Rigidbody2D rb = splitProjectile.GetComponent<Rigidbody2D> ();
			Projectile projectile = splitProjectile.GetComponent<Projectile> ();
			Vector2 direction = myMathf.rotate(gameObject.GetComponent<Rigidbody2D>().velocity, Mathf.Deg2Rad * Random.Range(-splitAngle/2, splitAngle/2));
			direction *= splitSpeedMultiplier;
			projectile.ServerStartProjectile (direction, shotBy);
			splitProjectile.transform.position = transform.position;
			spcb.mass = tmass;
			spcb.size = tsize;
			cb.updateMass ();
			cb.updateSize ();
			splitObjects.Add (splitProjectile);
		}

		for (int i = 0; i < splitObjects.Count; i++) {
            GameObject g = splitObjects[i];
            
            for (int y = i + 1; y < splitObjects.Count; y++) {
				Physics2D.IgnoreCollision (splitObjects [i].GetComponent<Collider2D>(), splitObjects [y].GetComponent<Collider2D>());
			}
		}
        NetworkServer.UnSpawn(gameObject);
		GameObject.Destroy (gameObject);
	}
}
