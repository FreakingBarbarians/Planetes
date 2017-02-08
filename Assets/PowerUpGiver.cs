using UnityEngine;
using System.Collections;

public class PowerUpGiver : MonoBehaviour {
    public GameObject powerUpPrefab;
    public AudioSource powerUpSound;

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.GetComponent<Planet>() != null) {
            GetComponent<SpriteRenderer>().enabled = false;
            GameObject powerUp = GameObject.Instantiate<GameObject>(powerUpPrefab);
            powerUp.transform.position = coll.transform.position;
            powerUp.transform.SetParent(coll.gameObject.transform);
            powerUpSound.Play();
            Destroy(gameObject, 0.5f);
        }
    }
}
