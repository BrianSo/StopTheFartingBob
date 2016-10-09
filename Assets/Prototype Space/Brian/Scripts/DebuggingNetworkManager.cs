using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DebuggingNetworkManager : NetworkManager {

	// Use this for initialization
	void Start () {

		NetworkClient client = StartHost();
		if(client == null){
			StartClient();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
