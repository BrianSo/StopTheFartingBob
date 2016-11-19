using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bob : NetworkBehaviour {

	public HealthUIControl uiControl;

	[SyncVar(hook="OnHealthChange")]
	public int healthPoint;

	public float pollutingSpeed = 1f;

	CoolDown fartCooldown = new CoolDown(1f);

	// Use this for initialization
	void Start () {
		fartCooldown.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		fartCooldown.timer -= Time.deltaTime * pollutingSpeed;
		if(fartCooldown.IsReady()){
			fartCooldown.Reset();
			//TODO place fart visual effect and bob farting animation

			if(isServer){
				//TODO Spawn the fart using NetworkServer.Spawn()
				Game.singleton.IncreasePollutionIndex(1f);
			}
		}else{
			
		}
	}



	void OnHealthChange(int hp){
		healthPoint = hp;
		Debug.Log("My hp changed to " + hp);
		//TODO update UI
		uiControl.SetHealth(hp);
	}

	[Server]
	public void TakeDamage(int damage){
		healthPoint -= damage;
		if(healthPoint <= 0){
			Game.singleton.BobGotZeroHealth();
		}
	}

	//play animation
	//play sound effect
	//no need to update healthPoint
	//as it will be synced
	[ClientRpc]
	public void RpcGetHurted(int damage){
		Debug.Log("Oh~ I am attacked");

	}


	void OnPlayerChanged(){
		if(this.IsOwnByLocalPlayer()){
			uiControl.ShowHealthImages();
		}
	}
	void Awake(){
		GetComponent<NetworkUnit>().EventOnPlayerChanged +=OnPlayerChanged;
	}
	void Destroy(){
		GetComponent<NetworkUnit>().EventOnPlayerChanged -=OnPlayerChanged;
	}
}
