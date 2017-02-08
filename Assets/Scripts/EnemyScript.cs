using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    private Rigidbody2D rb;
    private float decisionTime = 10;
    private float decisionTimer = 0;
    private GameObject player = null;
    public float speed = 2;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        decisionTimer += Time.deltaTime;
        if (player != null) {
            rb.velocity = (Vector2)(player.transform.position - transform.position).normalized * speed;
        }
        else if (decisionTimer >= decisionTime) {
            decisionTimer = 0;
            rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
            Debug.Log("Randomed");
        }
	}

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.CompareTag("Player")) {
            player = coll.gameObject;
        }
    }
}
