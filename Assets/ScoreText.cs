using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour {
    private Text text;
    public Planet planet;
    private float score = 0;

    void Start() {
        text = GetComponent<Text>();
    }

    void Update() {
        if(planet != null)
        score = planet.mass;
        text.text = ((int)score).ToString();
    }
}
