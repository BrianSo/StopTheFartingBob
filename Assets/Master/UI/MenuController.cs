using UnityEngine;

using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public InputField ipAddressText;

	// Use this for initialization
	void Start () {
		ipAddressText.text = PlayerPrefs.GetString("IP_ADDRESS","localhost");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void HostGameClick(){
		MyNetworkManager nm = MyNetworkManager.singleton;
		nm.StartHost();
	}

	public void JoinGameClick(){
		MyNetworkManager nm = MyNetworkManager.singleton;
		nm.networkAddress = ipAddressText.text;
		nm.StartClient();
	}

	public void IpAddressEndEdit(){
		PlayerPrefs.SetString("IP_ADDRESS", ipAddressText.text);
	}
	public void ExitClick(){
		Application.Quit();
	}
}
