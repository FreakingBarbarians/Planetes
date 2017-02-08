using UnityEngine;
using System.Collections;

public class PhaseSwitch : MonoBehaviour {

    public GameObject lastPhase;
    public GameObject nextPhase;
    public AudioClip winSound;

    void OnDestroy() {
        AudioSource.PlayClipAtPoint(winSound, Camera.main.transform.position);
        lastPhase.SetActive(false);
        nextPhase.SetActive(true);
    }
}
