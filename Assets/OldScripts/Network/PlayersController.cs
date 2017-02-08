using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayersController : NetworkManager {

    public List<NetworkConnection> players;
    public GameObject spawn1;
    public GameObject spawn2;

    void Start() {
        players = new List<NetworkConnection>();
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        Debug.Log("called");
        players.Add(conn);
        GameObject spawnUsed;
        if (players.Count == 1) {
            spawnUsed = spawn1;
        } else {
            spawnUsed = spawn2;
        }
        GameObject player = GameObject.Instantiate<GameObject>(playerPrefab);
        player.transform.position = spawnUsed.transform.position;
        player.transform.rotation = spawnUsed.transform.rotation;
        NetworkServer.AddPlayerForConnection(conn, player, (short)players.Count);
    }
}
