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
		
		player = p.GetComponent<Player>();
		OnPlayerChanged();
	}

	protected virtual void OnPlayerChanged(){}

	[ClientRpc]
	public void RpcMove(Vector3 position){
		gameObject.transform.position = position;
	}
}
