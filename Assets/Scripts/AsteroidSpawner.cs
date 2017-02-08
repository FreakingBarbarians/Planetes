using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class AsteroidSpawner : MonoBehaviour {

	public List<GameObject> spawnableAsteroids = new List<GameObject>();
	public List<float> spawnProbability = new List<float> ();
	public float timePerSpawn = 5f;
	public float timePerSpawnJitter = 0f;
	public int spawnCount = 1;
	public int spawnCountJitter = 0;
	public int maxSpawned = 10;
	public bool active = true;

	private CircleCollider2D bounds;
	private float probabilitySum;
	private float timer;
	private float timeTillSpawn;
	private int spawned;

	// Use this for initialization
	void Start () {
		spawned = 0;
		timer = 0;
		probabilitySum = 0;
		timeTillSpawn = timePerSpawn + Random.Range (-timePerSpawnJitter, timePerSpawnJitter);
		bounds = gameObject.GetComponent<CircleCollider2D> ();
		foreach (float probability in spawnProbability) {
			probabilitySum += probability;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			timer += Time.deltaTime;
			if (timer >= timeTillSpawn) {
				CmdSpawn ();
			}
		}
	}

	void CmdSpawn(){
		timer = 0;
		for(int i = 0; i < spawnCount + Random.Range(-spawnCountJitter, spawnCountJitter); i ++){
			if (spawned >= maxSpawned) {
				break;
			}
			float typeSeed = Random.Range (0, probabilitySum);
			GameObject toSpawnPrefab = getType (typeSeed);
			GameObject spawnedPrefab = GameObject.Instantiate<GameObject> (toSpawnPrefab);
			Vector2 localPos = new Vector2 (Random.Range (-bounds.radius, bounds.radius), Random.Range (-bounds.radius, bounds.radius));
			CelestialBody cb = spawnedPrefab.GetComponent<CelestialBody> ();
			float mass = cb.standardMass + Mathf.Abs (Random.Range (-cb.sMassJitter, cb.sMassJitter));
			float size = cb.standardSize + Mathf.Abs (Random.Range (-cb.sSizeJitter, cb.sSizeJitter));
			cb.mass = mass;
			cb.size = size; 
			spawnedPrefab.transform.position = localPos + (Vector2)transform.position;
			spawned++;
			spawnedPrefab.AddComponent<SpawnedBy> ().spawner = this;
		}
	}

	GameObject getType(float typeSeed){
		int i = 0;
		while (i < spawnProbability.Count) {
			if (typeSeed > spawnProbability [i]) {
				return spawnableAsteroids [i];
			}
			i++;
		}
		return spawnableAsteroids[i-1];
	}

	public void spawnedObjectDestroyed(){
		spawned--;
	}
}
