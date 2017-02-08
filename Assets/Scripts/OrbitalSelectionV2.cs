using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class OrbitalSelectionV2 : MonoBehaviour {
	public Orbit orbit;
	public Planet planet;

    // This is run on server.
	public void CmdFire (int type, Vector2 target) {
        // Last object collected
        Debug.Log("CMD FIRED");
		GameObject objectToFire = null;

		foreach(GameObject obj in orbit.orbitObjects){
            Debug.Log("obj");
			Projectile proj = obj.GetComponent<Projectile> ();
			if (true) {
				objectToFire = obj;
			}
		}

		if (objectToFire == null) {
			return;
		}

        Debug.Log("FOUND TYPE");

		Projectile objProj = objectToFire.GetComponent<Projectile>();
        Vector2 velocityVector = target - (Vector2)objectToFire.transform.position;
        velocityVector = velocityVector.normalized * Mathf.Sqrt(planet.mass);

        objProj.ServerStartProjectile (velocityVector, planet.gameObject);

		orbit.orbitObjects.Remove (objectToFire);
	}
}
