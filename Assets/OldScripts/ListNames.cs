using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ListNames : MonoBehaviour {

    public Text text;
    public List<GameObject> names = new List<GameObject>();

    private void updateText() {
        string str = "";
        foreach (GameObject obj in names) {
            Planet planet = obj.GetComponent<Planet>();
            if (planet != null) {
                str += planet.getName() + "\n";
            }
        }
        text.text = str;
    }

    public void addName(GameObject obj) {
        names.Add(obj);
        updateText();
    }

    public void removeName(GameObject obj) {
        names.Remove(obj);
        updateText();
    }
}
