using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectionUIController : MonoBehaviour {

	public Button readyButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		readyButton.interactable = true;
	}

	public void ReadyClick(){
		Player.localPlayer.CmdReady();
		readyButton.interactable = false;
	}
}
