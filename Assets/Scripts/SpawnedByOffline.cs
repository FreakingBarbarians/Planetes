using UnityEngine;
using System.Collections;

public class SpawnedByOffline : MonoBehaviour {
    public BoundedAsteroidSpawner spawner;

    void OnDestroy() {
        spawner.spawnedObjectDestroyed();
    }
}
