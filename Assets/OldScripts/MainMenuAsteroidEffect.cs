using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class MainMenuAsteroidEffect : NetworkBehaviour {

    CircleCollider2D bounds;
    public GameObject asteroidPrefab;
    private float timer = 0;
    public float spawnInterval = 5; 

    // Use this for initialization
    void Start () {
        bounds = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer) {
            return;
        }

        if (timer >= spawnInterval) {
            timer = 0;

            Vector2 alongRadius = new Vector2(Mathf.Cos(Random.Range(0, 10)), Mathf.Sin(Random.Range(0, 10)));
            alongRadius *= bounds.radius;
            alongRadius += (Vector2)transform.position;

            Vector2 towardsCenter = (Vector2)transform.position - alongRadius;
            towardsCenter.Normalize();
            towardsCenter *= Random.Range(1, 1.5f);

            GameObject spawnedAsteroid = GameObject.Instantiate<GameObject>(asteroidPrefab);
            NetworkServer.Spawn(spawnedAsteroid);
            CelestialBody cb = spawnedAsteroid.GetComponent<CelestialBody>();
            Projectile proj = spawnedAsteroid.GetComponent<Projectile>();

            cb.mass = Random.Range(30, 100);
            cb.size = Random.Range(10, 100);
            cb.updateSize();
            cb.updateMass();
            cb.updateColor();
            spawnedAsteroid.transform.position = alongRadius;
            proj.ServerStartProjectile(towardsCenter, gameObject);
        }
        timer += Time.deltaTime;
	}
}
