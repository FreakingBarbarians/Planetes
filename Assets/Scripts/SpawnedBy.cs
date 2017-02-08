using UnityEngine;
using System.Collections;

public class SpawnedBy : MonoBehaviour {
	public AsteroidSpawner spawner;

	void OnDestroy(){
		spawner.spawnedObjectDestroyed ();
	}
}
