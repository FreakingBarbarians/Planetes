using UnityEngine;
using System.Collections;

public class GoTowards : MonoBehaviour {
    public GameObject target;
    Rigidbody2D rb;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Vector2 pos = target.transform.position - transform.position;
        rb.velocity =  pos;
    }
}
