using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AsteroidDataPanel : MonoBehaviour {
	public Text sizeData;
	public Text massData;

	public void grabData(GameObject obj){
		CelestialBody cb = obj.GetComponent<CelestialBody> ();
		sizeData.text = cb.size.ToString ();
		massData.text = cb.mass.ToString ();
	}

}
