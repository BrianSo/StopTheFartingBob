using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bob : NetworkBehaviour {

	[SyncVar(hook="OnHealthChange")]
	public int healthPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnHealthChange(int hp){

	}
}
