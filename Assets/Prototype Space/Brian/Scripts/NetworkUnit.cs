using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkUnit : NetworkBehaviour {

	public Player player;
	public bool isOwnByLocalPlayer{
		get{
			return player == Player.localPlayer;
		}
	}

	[ClientRpc]
	public void RpcSetPlayer(GameObject p){
		Debug.Log("set player");
		player = p.GetComponent<Player>();
		OnPlayerChanged();
	}

	protected virtual void OnPlayerChanged(){}
}
