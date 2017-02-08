using UnityEngine;
using System.Collections;

public class DebrisTimer : MonoBehaviour {

    public float time = 5f;
	private float colorTime = 0;
	private SpriteRenderer sp;
	Color previousColor;

	// Use this for initialization
	void Start () {
		sp = GetComponent<SpriteRenderer> ();
		previousColor = sp.color;
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
		colorTime += Time.deltaTime * 3;
		if (colorTime >= 1) {
			colorTime = 0;
		}
		Color c = new Color ();
		c.a = 1;
		c.r = colorTime;
		c.g = colorTime;
		c.b = colorTime;
		sp.color = c;
        if (time < 0) {
            gameObject.layer = LayerMask.NameToLayer("CelestialBodiesLayer");
            if (gameObject.GetComponent<CelestialBody>().mass < 1) {
                GameObject.Destroy(gameObject);
            }
			sp.color = previousColor;
            this.enabled = false;
        }
        
	}
}
