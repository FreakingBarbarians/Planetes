using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ServerPlayerData : NetworkBehaviour {

    public MyServerManager server;

    private float timer = 0;
    public float updateInterval = 5;
    public int displayedPositions = 10;
    public ScoreBoardUpdateScript scoreboard;

    void Start() {
        server = MyServerManager.mainServerManager;
    }

	void Update () {

        if (server == null) {
            server = MyServerManager.mainServerManager;
        }

        if (timer >= updateInterval) {
            timer = 0;
            updateScoreboard();
        }
        timer += Time.deltaTime;
	}

    private void updateScoreboard() {
        int count = 0;
        string str = "";

        if (server == null) {
            return;
        }

        foreach (MyServerManager.ConnectedPlayer connectedPlayer in server.connections) {
            count++;

            if (count > displayedPositions) {
                break;
            }

            if (connectedPlayer.player != null) {
                Planet planet = connectedPlayer.player.GetComponent<Planet>();
                str += count + ". " + planet.getName();
                str += "   " + planet.mass;
                str += "\n";
            }
        }
        scoreboard.RpcUpdateScoreboard(str);
    }
}
