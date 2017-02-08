using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HostGameButton : MonoBehaviour {
    public MyNetwork network;
    public localDataHolder localData;

    public InputField playerName;
    public InputField port;

    public GameObject menuCanvas;
    public GameObject multiplayerCanvas;
    public GameObject highScores;

    public GameObject MainMenuAsteroidEffect;

    public void onClick() {
        network.SetUpServer(0);
        network.SetUpLocalClient();
        menuCanvas.SetActive(false);
        MainMenuAsteroidEffect.SetActive(false);
        NetworkServer.Spawn(highScores);
    }
}
