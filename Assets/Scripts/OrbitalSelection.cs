//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//// Client side
//public class OrbitalSelection : MonoBehaviour {
//    // Need endpoint tracking
//
//    public CircleCollider2D orbitCollider;
//    public Orbit orbit;
//    public Vector2 target = Vector2.up;
//    public int degreeIntervals = 1;
//    public float intervalAmounts = 5;
//    private Mesh myMesh;
//    private MeshFilter myMeshFilter;
//    private List<Vector3> vertices = new List<Vector3>();
//    private List<int> triangles = new List<int>();
//    private List<GameObject> removedOrbit = new List<GameObject>();
//
//    private float charge = 0;
//    private bool charging = false;
//
//    // Use this for initialization
//    void Start() {
//        myMesh = new Mesh();
//        myMesh.MarkDynamic();
//        myMeshFilter = GetComponent<MeshFilter>();
//        myMeshFilter.mesh = myMesh;
//    }
//
//    // Update is called once per frame
//    void Update() {
//        showMesh();
//        if (charging) {
//            if (charge <= 100f) {
//                charge += Time.deltaTime*10;
//            }
//        }
//    }
//
//    void showMesh() {
//        myMesh.Clear();
//        vertices.Clear();
//        triangles.Clear();
//
//        float baseAngle = myMathf.get360Angle(target - (Vector2)transform.position);
//        float upAngle = baseAngle + degreeIntervals * intervalAmounts;
//        float downAngle = baseAngle - degreeIntervals * intervalAmounts;
//
//        // Vector2 rightVector = (Vector2)transform.position + new Vector2(Mathf.Cos(downAngle * Mathf.Deg2Rad), Mathf.Sin(downAngle * Mathf.Deg2Rad)) * orbitCollider.radius * transform.root.transform.localScale.x;
//        // Vector2 middleVector = (Vector2)transform.position + new Vector2(Mathf.Cos(baseAngle * Mathf.Deg2Rad), Mathf.Sin(baseAngle * Mathf.Deg2Rad)) * orbitCollider.radius * transform.root.transform.localScale.x;
//        // Vector2 leftVector = (Vector2)transform.position + new Vector2(Mathf.Cos(upAngle * Mathf.Deg2Rad), Mathf.Sin(upAngle * Mathf.Deg2Rad)) * orbitCollider.radius * transform.root.transform.localScale.x;
//
//        for (int i = -degreeIntervals; i <= degreeIntervals; i++) {
//            vertices.Add(convertAngleToNearVector(baseAngle + i * intervalAmounts));
//            vertices.Add(convertAngleToFarVector(baseAngle + i * intervalAmounts));
//        }
//
//        for (int i = 0; i < vertices.Count; i++) {
//            if (i % 2 == 0) {
//                if (i == 0) {
//                    triangles.Add(i);
//                    triangles.Add(i + 3);
//                    triangles.Add(i + 1);
//                }
//                else if (i == vertices.Count - 2) {
//                    triangles.Add(i);
//                    triangles.Add(i + 1);
//                    triangles.Add(i - 2);
//                }
//                else {
//                    triangles.Add(i);
//                    triangles.Add(i + 1);
//                    triangles.Add(i - 2);
//
//                    triangles.Add(i);
//                    triangles.Add(i + 3);
//                    triangles.Add(i + 1);
//                }
//            }
//        }
//        if (degreeIntervals > 0) {
//            myMesh.vertices = vertices.ToArray();
//            myMesh.triangles = triangles.ToArray();
//        }
//    }
//
//    private Vector2 convertAngleToNearVector(float angle) {
//        return (new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * orbitCollider.radius);
//    }
//
//    private Vector2 convertAngleToFarVector(float angle) {
//        return convertAngleToNearVector(angle) * 1.5f;
//    }
//
//    public void setTarget(Vector2 thatTarget) {
//        target = thatTarget;
//    }
//
//    public void fireEach(float force) {
//        removedOrbit.Clear();
//        bool specialCase = false;
//
//        float baseAngle = myMathf.get360Angle(target - (Vector2)transform.position);
//        float upAngle = baseAngle + degreeIntervals * intervalAmounts;
//        float downAngle = baseAngle - degreeIntervals * intervalAmounts;
//
//        if (downAngle < 0) {
//            specialCase = true;
//            downAngle = 360 - downAngle;
//        }
//
//        foreach (GameObject obj in orbit.orbitObjects) {
//            float angle = myMathf.get360Angle(obj.transform.position - transform.position);
//
//            if (!specialCase) {
//                if (angle >= downAngle && angle <= upAngle) {
//                    fire(obj, force);
//                }
//            }
//            else {
//                if (angle <= upAngle || angle >= downAngle) {
//                    fire(obj, force);
//                }
//            }
//        }
//        foreach (GameObject obj in removedOrbit) {
//            orbit.orbitObjects.Remove(obj);
//        }
//    }
//
//    private void fire(GameObject obj, float force) {
//        obj.layer = LayerMask.NameToLayer("ProjectilesLayer");
//        removedOrbit.Add(obj);
//        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
//
//		// Give the projectile velocity
//        Vector2 velocityVector = obj.transform.position - transform.position;
//        velocityVector *= force;
//        rb.AddForce(velocityVector, ForceMode2D.Impulse);
//
//		Projectile p = obj.GetComponent<Projectile>();
//		p.shotBy = transform.root.gameObject;
//		Physics2D.IgnoreCollision (obj.GetComponent<Collider2D> (), orbit.planet.GetComponent<Collider2D> ());
//
//		p.startProjectile();
//    }
//
//    public void startCharge() {
//        charging = true;
//    }
//
//    public void discharge() {
//        charging = false;
//        fireEach(charge);
//        charge = 0;
//    }
//
//}
