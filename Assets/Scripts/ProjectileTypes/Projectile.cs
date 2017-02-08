using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Projectile : MonoBehaviour {
	// Projectile Types
	public static int PROJECTILE_PARENT = 0;
	public static int PROJECTILE_STANDARD = 1;
	public static int PROJECTILE_SPLIT = 2;
	public static float BASE_SPEED = 2;


	public bool projectileActive = false;
    public GameObject shotBy;
    public readonly int type;

	public virtual int getType(){
		return 0;
	}

    
	public virtual void ServerStartProjectile (Vector2 direction, GameObject shotBy) {
        // both
        this.shotBy = shotBy;
        // Server
        GetComponent<Rigidbody2D> ().velocity = calculateSpeed (direction);
        // both
        projectileActive = true;
        // both
        gameObject.layer = LayerMask.NameToLayer ("ProjectilesLayer");
        // both
        Physics2D.IgnoreCollision (shotBy.GetComponent<Collider2D> (), GetComponent<Collider2D> (), true);
        RpcClientStartProjectile(gameObject, shotBy);
	}

    public virtual void RpcClientStartProjectile(GameObject g, GameObject shotBy) {
        // both
        projectileActive = true;
        // both
        gameObject.layer = LayerMask.NameToLayer("ProjectilesLayer");
        // both
        Debug.Log(gameObject.name);
        Physics2D.IgnoreCollision(shotBy.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        g.GetComponent<Projectile>().shotBy = shotBy;
    }

	void OnCollisionEnter2D(Collision2D coll) {
        coll.gameObject.GetComponent<CelestialBody>().ServerProjectileCollision(gameObject);
        ServerStartCelestialBody();

    }

	public virtual void ServerStartCelestialBody(){
		gameObject.layer = LayerMask.NameToLayer ("CelestialBodiesLayer");
		projectileActive = false;
		Physics2D.IgnoreCollision (shotBy.GetComponent<Collider2D> (), GetComponent<Collider2D> (), false);
	}

	protected Vector2 calculateSpeed(Vector2 direction){
		CelestialBody cb = GetComponent<CelestialBody> ();

        return direction * cb.getDensity();
	}
}
