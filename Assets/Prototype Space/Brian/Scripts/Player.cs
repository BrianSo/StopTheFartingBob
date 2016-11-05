using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Player : NetworkBehaviour {

	public static Player localPlayer;

	[SyncVar]
	public int charactor = BOB;
	public const int BOB = 1;
	public const int GARNDERER = 2;

	public GameObject basePrefab;
	public GameObject bobPrefab;
	public GameObject garndererPrefab;

	public override void OnStartLocalPlayer(){
		this.Singleton(ref localPlayer);
		Debug.Log(localPlayer);
	}
	void Destory(){
		if(isLocalPlayer){
			this.RemoveSingleton(ref localPlayer);
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log("I am " + ((charactor == BOB) ? "Bob" : "Garnderer"));
		if(isServer){
			GameObject prefab;
			switch(charactor){
				case BOB:
					prefab = bobPrefab;
					break;
				case GARNDERER:
					prefab = garndererPrefab;
					break;
				default:
					prefab = basePrefab;
					break;
			}
			GameObject newCharactor = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
			NetworkServer.SpawnWithClientAuthority(newCharactor, this.gameObject);
			newCharactor.GetComponent<NetworkUnit>().RpcSetPlayer(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
