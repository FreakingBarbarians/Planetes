using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CircleCollider2D))]
public class GravityPowerUp : MonoBehaviour {
    private Planet planet;
    public float powerUpTime = 5;
    private float timer = 0;
    private CircleCollider2D bounds;

    // Use this for initialization

	void Start () {
        planet = transform.root.GetComponent<Planet>();
    }

    void Update() {
        if (timer >= powerUpTime) {
            GameObject.Destroy(gameObject);
            
        }
        timer += Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D coll) {
        Vector2 moveTowards = transform.position - coll.transform.position;
        coll.gameObject.transform.position += (Vector3) moveTowards.normalized * Time.deltaTime;
    }
}
