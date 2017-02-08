using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
    public GameObject toEnable;
    public GameObject playerObject;
    public static GameOverScript script;

    void Start() {
        script = this;
    }

    public void EndGame() {
        toEnable.SetActive(true);
        if (playerObject != null) {
            playerObject.GetComponent<HumanController>().enabled = false;
            playerObject.GetComponent<Planet>().shedding = false;
            playerObject.GetComponent<Planet>().boosting = false;
        }
    }
}
