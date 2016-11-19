using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SelfViewController : NetworkBehaviour {

	public MeshRenderer mr;

	// Use this for initialization
	void Start () {
		if (!this.IsOwnByLocalPlayer()) {
			mr.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
