using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Planet : CelestialBody {

    public Vector2 moveVector;
    public Camera playerCam;
    public float speed;
    public GameObject asteroidPrefab;
    public GameObject orbit;
    public Text nameplate;

    private string playerName;

    public int team;

    public bool alive = true;
    public int cameraZoom = 1;
    public bool controlsCamera = true;

    public List<GameObject> pickingUp = new List<GameObject>();

    public bool boosting = false;
    private int moveMultiplier = 1;
    public bool shedding = false;

    public ParticleSystem particles;
    public ParticleSystem shedEmitter;

    public AudioSource pickupSound;
    public AudioSource deathSound;
    public AudioSource boostSound;
    public AudioSource shedSound;

    // Use this for initialization
    void Start() {

        updateSize();
        updateMass();

        // good

        playerCam = Camera.main;
        particles.Pause();
        shedEmitter.Pause();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (mass < 0) {
            Destroy(gameObject);
        }

        if (boosting) {
            moveMultiplier = 5;
            this.mass -= 10 * Time.deltaTime;
            this.size -= 10 * Time.deltaTime;
            updateSize();
            updateMass();
        } else {
            moveMultiplier = 1;
        }

        if (shedding) {
            this.mass *= (1f - 0.01f * Time.deltaTime);
            this.size *= (1f - 0.1f * Time.deltaTime);
            updateSize();
            updateMass();
        } else {
        }
        speed = (mass / size) * 10;
        playerCam.orthographicSize = 5 * transform.localScale.x * cameraZoom;
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, playerCam.transform.position.z);
        playerCam.transform.position = newPos;
        transform.position += (Vector3)moveVector * speed * Time.deltaTime * moveMultiplier;
        rb.velocity *= 0.9f;
    }

    public void move(Vector2 moveVector) {
        this.moveVector = moveVector;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        Debug.Log("Collision");


            pickingUp.Remove(coll.gameObject);
            RpcAccreteObject(coll.gameObject);
            pickupSound.Play();

    }

    void OnCollisionStay2D(Collision2D coll) {

        // rpc accrete

            pickingUp.Remove(coll.gameObject);
            RpcAccreteObject(coll.gameObject);

    }

    public string getName() {
        return playerName;
    }

    public void setName(string name) {
        playerName = name;
        nameplate.text = name;
    }

    // server

    public override void ServerProjectileCollision(GameObject projectile) {
        mass -= projectile.gameObject.GetComponent<CelestialBody>().mass;
        size -= projectile.gameObject.GetComponent<CelestialBody>().size;
        if (mass < 0 || size < 0) {
            Destroy(gameObject);
        }
        updateSize();
        updateMass();
    }

    public void boost() {
        boosting = true;
        particles.Play();
        boostSound.Play();
    }

    public void unBoost() {
        boosting = false;
        ParticleSystem.EmissionModule em = particles.emission;
        particles.Pause();
        boostSound.Stop();
    }

    public void shed() {
        shedding = true;
        shedSound.Play();
        shedEmitter.Play();
    }

    public void unshed() {
        shedding = false;
        shedSound.Stop();
        shedEmitter.Stop();
    }

    void OnDestroy() {
        AudioSource.PlayClipAtPoint(deathSound.clip, transform.position);
        GameOverScript.script.EndGame();
    }
}
