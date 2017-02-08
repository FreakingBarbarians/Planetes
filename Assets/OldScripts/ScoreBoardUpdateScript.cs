using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreBoardUpdateScript : NetworkBehaviour {

    public Text scoreboard;

    [ClientRpc]
    public void RpcUpdateScoreboard(string str) {
        scoreboard.text = str;
    }

}
