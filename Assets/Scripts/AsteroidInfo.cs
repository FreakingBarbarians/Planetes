using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AsteroidInfo : MonoBehaviour {
	public GameObject asteroidDataPanelPrefab;
	private GameObject activeDataPanel;

	// Use this for initialization
	void Start () {
		activeDataPanel = null;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

		if (hit) {
			if (activeDataPanel == null && hit.collider.gameObject.CompareTag("CelestialBody")) {
				activeDataPanel = GameObject.Instantiate (asteroidDataPanelPrefab);
				AsteroidDataPanel dataPanel = activeDataPanel.GetComponent<AsteroidDataPanel> ();
				dataPanel.grabData (hit.collider.gameObject);
				activeDataPanel.transform.SetParent (transform);
				activeDataPanel.transform.position = Camera.main.WorldToScreenPoint (hit.collider.transform.position);
			}
		} else {
			if (activeDataPanel != null) {
				GameObject.Destroy (activeDataPanel);
			}
			activeDataPanel = null;
		}
	}
}
