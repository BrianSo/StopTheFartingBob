using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkUnit : NetworkBehaviour {

	public event Callback EventOnPlayerChanged;
	public delegate void Callback();

	[HideInInspector]
	public Player player;
	public bool isOwnByLocalPlayer{
		get{
			return player == Player.localPlayer;
		}
	}

	[ClientRpc]
	public void RpcSetPlayer(GameObject p){
		
		player = p.GetComponent<Player>();
		EventOnPlayerChanged();
	}

	[ClientRpc]
	public void RpcMove(Vector3 position){
		gameObject.transform.position = position;
	}
}
