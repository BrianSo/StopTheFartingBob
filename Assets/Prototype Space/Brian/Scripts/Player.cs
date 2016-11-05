using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Player : NetworkBehaviour {

	public static Player localPlayer;

	[SyncVar]
	public int charactor = NOT_SET;
	public const int NOT_SET = 0;
	public const int BOB = 1;
	public const int GARNDERER = 2;

	public override void OnStartLocalPlayer(){
		this.Singleton(ref localPlayer);
	}
	void Destory(){
		if(isLocalPlayer){
			this.RemoveSingleton(ref localPlayer);
		}
	}

	// Use this for initialization
	void Start () {
		Debug.Log("I am " + ((charactor == BOB) ? "Bob" : "Garnderer"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
