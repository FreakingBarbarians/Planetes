using UnityEngine;
using System.Collections;

public class GameOnRun : MonoBehaviour {
    void Update() {
        if (MyServerManager.mainServerManager != null) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
