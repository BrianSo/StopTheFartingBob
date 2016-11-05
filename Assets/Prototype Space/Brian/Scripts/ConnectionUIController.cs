using UnityEngine;
using System.Collections;

public class ConnectionUIController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void HostDisconnectClick(){
		MyNetworkManager.singleton.StopHost();
	}
	public void ClientDisconnectClick(){
		MyNetworkManager.singleton.StopClient();
	}
}
