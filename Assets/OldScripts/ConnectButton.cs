using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConnectButton : MonoBehaviour {

    public MyNetwork server;
    public GameObject thisMenu;
    public GameObject nextMenu;
    public GameObject menuEffects;
    public GameObject scoreboard;

    public localDataHolder localData;

    public InputField ip;
    public InputField port;
    public InputField username;


    public void OnClick() {

        int myPort = int.Parse(port.text);
        string myip = ip.text;
        localData.playerName = username.text;

        server.SetUpClient(myip, myPort);

        menuEffects.SetActive(false);    
        nextMenu.SetActive(true);
        thisMenu.SetActive(false);
    }
}
