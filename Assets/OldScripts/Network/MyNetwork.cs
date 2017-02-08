using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections.Generic;
using UnityEngine.UI;


public class MyNetwork : MonoBehaviour {

    NetworkClient myClient;
    public bool isAtStartup = true;
    public GameObject ServerPrefab;
    public List<GameObject> spawns = new List<GameObject>();
    public InputField oldIp;
    public bool gameOn = false;

    void Start() {
        SetUpDummyServer();
        SetUpLocalDummyClient();
    }

	// Update is called once per frame
	void Update () {
        //if (!isAtStartup) {
        //    return;
        //}

        //if (Input.GetKey(KeyCode.S)) {
        //    SetUpServer();
        //    SetUpLocalClient();
        //}
        //else if (Input.GetKey(KeyCode.Return)) {
        //    SetUpClient();
        //}
	}

    public void SetUpServer() {
        NetworkServer.Listen(25565);
        GameObject g = GameObject.Instantiate(ServerPrefab);
        g.GetComponent<MyServerManager>().Init();
        NetworkServer.Spawn(g);
        isAtStartup = false;
    }

    public void SetUpServer(int port) {
        cleanUpServer();
        NetworkServer.Shutdown();
        NetworkServer.dontListen = false;
        NetworkServer.Reset();
        NetworkServer.Listen(port);
        GameObject g = GameObject.Instantiate(ServerPrefab);
        g.GetComponent<MyServerManager>().Init();
        NetworkServer.Spawn(g);
        isAtStartup = false;
        gameOn = true;
    }

    public void SetUpDummyServer() {
        NetworkServer.dontListen = true;
        NetworkServer.Listen(-1);
        // GameObject g = GameObject.Instantiate(ServerPrefab);
        // g.GetComponent<MyServerManager>().Init();
        // NetworkServer.Spawn(g);
        NetworkServer.SpawnObjects();
        isAtStartup = false;
    }

    public void SetUpClient() {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.Error, OnFailToConnect);
        Debug.Log("Connecting to " + oldIp.text.Trim());
        myClient.Connect(oldIp.text.Trim(), 25565);
        //ClientScene.ClearSpawners();
        //foreach (GameObject spawnable in spawns) {
        //    ClientScene.RegisterPrefab(spawnable);
        //}
        isAtStartup = false;
    }

    public void SetUpClient(string ip, int port) {
        NetworkServer.Shutdown();
        NetworkServer.dontListen = false;
        NetworkServer.Reset();
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.Error, OnFailToConnect);
        Debug.Log("Connecting to " + ip);
        myClient.Connect(ip, port);
        //ClientScene.ClearSpawners();
        //foreach (GameObject spawnable in spawns) {
        //    ClientScene.RegisterPrefab(spawnable);
        //}
        isAtStartup = false;
        Debug.Log("Terminates");
        gameOn = true;
    }

    public void SetUpLocalClient() {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        //ClientScene.ClearSpawners();
        //foreach (GameObject spawnable in spawns) {
        //    ClientScene.RegisterPrefab(spawnable);
        //}

        isAtStartup = false;
    }

    public void SetUpLocalDummyClient() {
        myClient = ClientScene.ConnectLocalServer();
        ClientScene.ClearSpawners();
        foreach (GameObject spawnable in spawns) {
            ClientScene.RegisterPrefab(spawnable);
        }
    }

    public void OnConnected(NetworkMessage m) {
        Debug.Log("Connect!");
        ClientScene.AddPlayer(myClient.connection, 1);
    }

    public void OnFailToConnect(NetworkMessage m) {
        Debug.Log("Failed To Connect");
        Debug.Log(m.ReadMessage<StringMessage>().value);
    }

    private void cleanUpServer() {
        foreach (NetworkIdentity g in NetworkServer.objects.Values) {
            if (!g.gameObject.CompareTag("GameFlowObject")) {
                GameObject.Destroy(g.gameObject);
            }
        }
    }
}
