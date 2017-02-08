using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking.NetworkSystem;

public class MyServerManager : NetworkBehaviour {

    public struct ConnectedPlayer {
        public NetworkConnection connection;
        public short playerId;
        public GameObject player;
    }

    public List<ConnectedPlayer> connections;
    public GameObject playerPrefab;
    public static MyServerManager mainServerManager;
    public GameFlow gameFlow;

    void Awake() {
        mainServerManager = this;
    }

    public void Init() {
        NetworkServer.RegisterHandler(MsgType.AddPlayer, OnPlayerAdd);
        connections = new List<ConnectedPlayer>();
        NetworkServer.SpawnObjects();
    }

    private void OnPlayerAdd(NetworkMessage msg) {
        int playerControllerId = msg.ReadMessage<IntegerMessage>().value;
        GameObject player = GameObject.Instantiate<GameObject>(playerPrefab);
        Vector2 pos = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
        player.transform.position = pos;
        player.GetComponent<Planet>().alive = true;
        NetworkServer.AddPlayerForConnection(msg.conn, player, (short) playerControllerId);

        Debug.Log("Adding New Player");
        ConnectedPlayer newConnectedPlayer;
        newConnectedPlayer.connection = msg.conn;
        newConnectedPlayer.playerId = (short) playerControllerId;
        newConnectedPlayer.player = player;
        connections.Add(newConnectedPlayer);
    }

    public void OnPlayerDestroyed(int team, NetworkConnection connection, GameObject player, short playerId) {
        GameObject newPlayer = GameObject.Instantiate(playerPrefab);
        Vector2 pos = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
        player.transform.position = pos;
        newPlayer.GetComponent<Planet>().alive = true;
        NetworkServer.ReplacePlayerForConnection(connection, newPlayer, playerId);
        ConnectedPlayer p = findConnectedPlayer(connection, playerId);
        p.player = newPlayer;
    }

    private ConnectedPlayer findConnectedPlayer(NetworkConnection conn, short playerId) {
        foreach (ConnectedPlayer connectedPlayer in connections) {
            if (connectedPlayer.connection == conn && connectedPlayer.playerId == playerId) {
                return connectedPlayer;
            }
        }
        return new ConnectedPlayer();
    }
}
