using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

// fix this

public class Orbit : MonoBehaviour {

    public List<GameObject> orbitObjects = new List<GameObject>();
    public List<GameObject> consumingObjects = new List<GameObject>();
    public Planet planet;
    public GameObject localOrbit;
    private CircleCollider2D orbit;

    bool shouldRotate;
    float rotateDirection;


    // Use this for initialization
    void Start() {
        orbit = localOrbit.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        foreach (GameObject obj in orbitObjects) {
            if (obj == null) {
                orbitObjects.Remove(obj);
                break;
            }
            float angle = myMathf.get360Angle(obj.transform.position - transform.position);
            float sin = Mathf.Sin(Mathf.Deg2Rad * angle);
            float cos = Mathf.Cos(Mathf.Deg2Rad * angle);

            Vector2 closestPosition = (Vector2)transform.position + new Vector2(cos, sin) * orbit.radius * transform.root.transform.localScale.x;

            Debug.DrawLine(transform.position, closestPosition, Color.red);

            if (shouldRotate) {
                Vector2 rotationVector = new Vector2(-sin, cos) * rotateDirection;
                obj.transform.position += (Vector3)rotationVector * Time.deltaTime;
                Debug.DrawLine(obj.transform.position, obj.transform.position + (Vector3)rotationVector * Time.deltaTime);
            }

            Vector2 deltaVector = closestPosition - (Vector2)obj.transform.position;
            obj.transform.position += (Vector3)deltaVector * Time.deltaTime;
        }

        foreach (GameObject obj in consumingObjects) {
            Vector2 dv = transform.position - obj.transform.position;
            obj.transform.position += (Vector3)dv * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        // Do stuff that will add the object to the orbit.
        ServerAddOrbit(coll.gameObject);
    }

    private void ServerAddOrbit(GameObject orbitObject) {

        CelestialBody body;

        if (!(body = orbitObject.GetComponent<CelestialBody>())) {
            return;
        }

        if (body.mass > 0.5 * planet.mass) {
            return;
        }


        if (orbitObject.GetComponent<Projectile>().projectileActive) {
            return;
        }

        if (!orbitObjects.Contains(orbitObject) && !orbitObject.CompareTag("NoPickup")) {

            if (orbitObject.GetComponent<DebrisTimer>()) {
                Destroy(orbitObject.GetComponent<DebrisTimer>());
            }

            orbitObjects.Add(orbitObject);

            // If It is a LIVING player, then replace
            Planet player;
            if ((player = orbitObject.GetComponent<Planet>())) {
                if (player.alive) {
                }
            }
            orbitObject.layer = LayerMask.NameToLayer("OrbitLayer");
        }
    }

    public void rotate(float direction) {
        shouldRotate = true;
        rotateDirection = direction;
    }


    public void stopRotate() {
        shouldRotate = false;
    }

    // Still getting null objects.


    public void ServerConsume() {
        foreach (GameObject obj in orbitObjects) {
            if (obj == null) {
                orbitObjects.Remove(obj);
            }
            GoTowards go = obj.AddComponent<GoTowards>();
            go.target = localOrbit;
            obj.tag = "NoPickup";
            obj.layer = LayerMask.NameToLayer("CelestialBodiesLayer");
            planet.pickingUp.Add(obj);
        }
        orbitObjects.Clear();
    }

    private void RpcClientConsume() {
        foreach (GameObject obj in orbitObjects) {
            if (obj == null) {
                orbitObjects.Remove(obj);
            }
            GoTowards go = obj.AddComponent<GoTowards>();
            go.target = localOrbit;
            obj.tag = "NoPickup";
            obj.layer = LayerMask.NameToLayer("CelestialBodiesLayer");
            planet.pickingUp.Add(obj);
        }
        orbitObjects.Clear();
    }
}

//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class OrbitObject {
//    public Vector2 orbitPosition;
//    public GameObject orbitObject;

//    public OrbitObject(GameObject orbitObject, Vector2 orbitPosition) {
//        this.orbitPosition = orbitPosition;
//        this.orbitObject = orbitObject;
//    }

//}

//public class Orbit : MonoBehaviour {

//    public List<OrbitObject> orbitObjects = new List<OrbitObject>();
//    private CircleCollider2D orbit;


//    // Use this for initialization
//    void Start () {
//        orbit = localOrbit.GetComponent<CircleCollider2D>();
//	}

//	// Update is called once per frame
//	void Update () {
//        foreach (OrbitObject obj in orbitObjects) {
//            obj.orbitObject.transform.position = transform.position + (Vector3)obj.orbitPosition * transform.root.transform.localScale.x;
//        }
//	}

//    void OnTriggerEnter2D(Collider2D coll) {
//        // Do stuff that will add the object to the orbit.

//        float angle = myMathf.get360Angle(coll.transform.position - transform.position);
//        float sin = Mathf.Sin(Mathf.Deg2Rad * angle);
//        float cos = Mathf.Cos(Mathf.Deg2Rad * angle);
//        Debug.Log("Orbital Object Detected at Angle " + angle + " sin: " + sin + " cos: " + cos);

//        Vector2 position = new Vector2(cos, sin) * orbit.radius;

//        // Modifications to the object's properties
//        coll.localOrbit.GetComponent<Rigidbody2D>().isKinematic = true;
//        coll.localOrbit.layer = LayerMask.NameToLayer("OrbitLayer");

//        OrbitObject orbitObject = new OrbitObject(coll.localOrbit, position);
//        orbitObjects.Add(orbitObject);
//    }
//}

