using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class HumanController :  MonoBehaviour {
    public Planet planet;
    public Orbit orbit;
	public OrbitalSelectionV2 selector;
	private int type = 0;


    public GameObject targetObj;

	// Use this for initialization
	void Start () {
        if (planet == null) {
            planet = gameObject.GetComponent<Planet>();
        }
        if (orbit == null) {
            orbit = gameObject.GetComponentInChildren<Orbit>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (true) {
            float xMove = Input.GetAxis("Horizontal");
            float yMove = Input.GetAxis("Vertical");

            planet.move(new Vector2(xMove, yMove));

            if (Input.GetMouseButton(0)) {
                CmdRotate(1);
            }
            else if (Input.GetMouseButton(1)) {
                CmdRotate(-1);
            }
            else {
                CmdStopRotate();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                planet.boost();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift)) {
                planet.unBoost();
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt)) {
                planet.shed();
            }
            if (Input.GetKeyUp(KeyCode.LeftAlt)) {
                planet.unshed();
            }

            if (Input.GetKey(KeyCode.Space)) {
                CmdConsume();
            }

            if (Input.GetKeyDown(KeyCode.Q)) {
                if (type > 1) {
                    type--;
                }
            }

            if (Input.GetKeyDown(KeyCode.E)) {
                if (type < 2) {
                    type++;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftControl)) {
                Debug.Log("Fire");
                CmdFire(type, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            if (Input.mouseScrollDelta.y != 0) {
                if (Input.mouseScrollDelta.y > 0 && planet.cameraZoom < 20) {
                    planet.cameraZoom++;
                }
                else if (Input.mouseScrollDelta.y < 0 && planet.cameraZoom > 1) {
                    planet.cameraZoom--;
                }
            }
        }		
	}


    void CmdRotate(int direction) {
        orbit.rotate(direction);
    }

    void CmdStopRotate() {
        orbit.stopRotate();
    }

    void CmdConsume() {
        orbit.ServerConsume();
    }

    void CmdFire(int type, Vector2 target) {
        Debug.Log("CMDFIRING");
        selector.CmdFire(type, target);
    }
}
