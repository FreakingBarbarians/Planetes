using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GameFlow : NetworkBehaviour {

    // Initialize singlePlayerServer
    void Start() {
        SetUpServer();
    }

    public void SetUpServer() {
        NetworkServer.Listen(-1);
        NetworkServer.SpawnObjects();
        ClientScene.ConnectLocalServer();
    }

}
