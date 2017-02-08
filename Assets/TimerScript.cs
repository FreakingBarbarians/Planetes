using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
    public Text timer;
    public GameOverScript gameOver;
    public float TimeLeft = 600;
    public AudioSource timerSound;
    public float secondTimer = 0;

    void Update() {

        secondTimer += Time.deltaTime;
        if (secondTimer > 1) {
            secondTimer = 0;
            timerSound.Play();
        }



        TimeLeft -= Time.deltaTime;
        float tempTime = TimeLeft;
        int minutes = (int) tempTime / 60;
        int seconds = (int) tempTime % 60;
        timer.text = minutes.ToString() + ":" + seconds.ToString();
        if (TimeLeft < 0) {
            gameOver.EndGame();
            GameObject.Destroy(gameObject);
        }
    }
}
