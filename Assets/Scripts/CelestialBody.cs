using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
public class CelestialBody : MonoBehaviour {

    public float standardMass;
    public float sMassJitter;
    public float standardSize;
    public float sSizeJitter;

    public float mass;
    public float size;

    public Rigidbody2D rb;
    public CircleCollider2D bounds;
    public SpriteRenderer sp;

    void Start() {
        bounds = GetComponent<CircleCollider2D>();
        rb.isKinematic = false;
        updateMass();
        updateSize();
        updateColor();
    }

    public float getHP() {
        return size * mass;
    }


    public float getDensity() {
        return mass / size;
    }

    public void updateMass() {
        rb.mass = mass;
    }

    public void updateSize() {
        float scale = Mathf.Sqrt(size / (3.141f * 4f * Mathf.Pow(2 * bounds.radius, 2)));
        transform.root.localScale = new Vector3(scale, scale, 1);
    }

    public void RpcUpdateMass() {
        updateMass();
    }

    public void RpcUpdateSize() {
        updateSize();
    }

    public void updateColor() {
        Color col = new Color();
        col.r = 1 - getDensity() / 10;
        col.b = 1 - getDensity() / 10;
        col.g = 1 - getDensity() / 10;
        col.a = 1;
        sp.color = col;
    }

    // Should only be server side detection of this but should be callde on the client
    public virtual void ServerProjectileCollision(GameObject projectile) {
        // server
        List<GameObject> spawnedObjects = new List<GameObject>();
        CelestialBody projectileStats = projectile.GetComponent<CelestialBody>();
        Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
        float velocity = projectileBody.velocity.magnitude;
        float mass = projectileStats.mass;
        float density = projectileStats.getDensity();
        float energyTransferred = Mathf.Pow(velocity, 2) * mass * density;
        Vector2 collisionVector = projectile.transform.position - transform.position;

        if (this.mass / 2 >= projectileStats.mass) {
            RpcAccreteObject(projectile);
            return;
        }
        else {
            if (energyTransferred < getHP()) {
                float massPerSize = getDensity();
                float newSize = mass * (getHP() - energyTransferred) / getHP();
                float remainingSize = size - newSize;
                float energyTransferredLarge = energyTransferred * (newSize / size);

                GameObject largeMass = GameObject.Instantiate<GameObject>(gameObject);

                CelestialBody largeBody = largeMass.GetComponent<CelestialBody>();
                Rigidbody2D largeRigid = largeMass.GetComponent<Rigidbody2D>();

                largeBody.size = newSize;
                largeBody.mass = newSize * massPerSize;
                largeRigid.velocity = -collisionVector * (energyTransferredLarge / (newSize * massPerSize));
                NetworkServer.Spawn(largeMass);
                int splits = Random.Range(2, 5);
                float average = remainingSize / splits;
                float energyRemaining = energyTransferred - energyTransferredLarge;
                Debug.Log("Energy Remaining" + energyRemaining);
                float averageEnergy = energyRemaining / splits;
                while (remainingSize > 0) {
                    float tsize = average + Random.Range(-average / 2, average / 2);
                    float teng = averageEnergy + Random.Range(-averageEnergy / 2, averageEnergy / 2);
                    if (energyRemaining >= teng) {
                    }
                    else {
                        energyRemaining = teng;
                    }

                    if (remainingSize >= tsize) {
                    }
                    else {
                        tsize = remainingSize;
                    }
                    energyRemaining -= teng;
                    remainingSize -= tsize;
                    float tmass = tsize * massPerSize;
                    GameObject copy = GameObject.Instantiate<GameObject>(gameObject);
                    CelestialBody copyBody = copy.GetComponent<CelestialBody>();
                    Rigidbody2D copyrb = copy.GetComponent<Rigidbody2D>();
                    Vector2 jitteredVelocity = collisionVector.normalized * (teng / tmass);
                    float rotation = Random.Range(-45, 45) * Mathf.Deg2Rad;
                    jitteredVelocity = myMathf.rotate(jitteredVelocity, rotation);
                    copyrb.velocity = jitteredVelocity;

                    spawnedObjects.Add(copy);
                    NetworkServer.Spawn(copy);

                    copyBody.size = tsize;
                    copyBody.mass = tmass;
                    copyBody.updateMass();
                    copyBody.updateSize();
                }
            }
            else {
                int splits = Random.Range(4, 12);
                float average = size / splits;
                float averageEnergy = energyTransferred / splits;
                float massPerSize = getDensity();
                float remainingSize = size;
                float energyRemaining = energyTransferred;
                while (remainingSize > 0) {
                    float tsize = average + Random.Range(-average / splits, average / splits);
                    float teng = averageEnergy + Random.Range(-averageEnergy / 2, averageEnergy / 2);
                    if (energyRemaining >= teng) {
                    }
                    else {
                        energyRemaining = teng;
                    }
                    if (tsize > remainingSize) {
                        tsize = remainingSize;
                    }
                    remainingSize -= tsize;

                    float tmass = tsize * massPerSize;
                    GameObject copy = GameObject.Instantiate<GameObject>(gameObject);
                    CelestialBody copyBody = copy.GetComponent<CelestialBody>();
                    Rigidbody2D copyrb = copy.GetComponent<Rigidbody2D>();
                    spawnedObjects.Add(copy);
                    Vector2 jitteredVelocity = collisionVector.normalized * teng / tmass;
                    jitteredVelocity = myMathf.rotate(jitteredVelocity, Random.Range(0, 360) * Mathf.Deg2Rad);
                    copyrb.velocity = jitteredVelocity;

                    copyBody.size = tsize;
                    copyBody.mass = tmass;
                    copyBody.updateMass();
                    copyBody.updateSize();
                }

                

            }
            // RPC
            foreach (GameObject g in spawnedObjects) {
                CelestialBody c = g.GetComponent<CelestialBody>();
                Rigidbody2D b = g.GetComponent<Rigidbody2D>();
                g.AddComponent<DebrisTimer>();
                g.layer = LayerMask.NameToLayer("DebrisLayer");
                c.updateColor();
            }
            
            GameObject.Destroy(gameObject);
        }
    }

    public void RpcAccreteObject(GameObject coll) {

        Debug.Log("accrete object");
        CelestialBody body = coll.GetComponent<CelestialBody>();

        if (body.mass >= mass / 2) {
            return;
        }

        this.size += body.size;
        this.mass += body.mass;
        this.updateMass();
        this.updateSize();
        GameObject.Destroy(coll.gameObject);
    }
}
