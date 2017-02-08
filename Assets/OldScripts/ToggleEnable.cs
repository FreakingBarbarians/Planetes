using UnityEngine;
using System.Collections;

public class ToggleEnable : MonoBehaviour {

    public GameObject thisMenu;
    public GameObject nextMenu;

    public void onClick() {
        thisMenu.SetActive(false);
        nextMenu.SetActive(true);    
    }

}
