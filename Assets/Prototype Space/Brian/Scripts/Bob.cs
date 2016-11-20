using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bob : NetworkBehaviour {

	public HealthUIControl uiControl;

	[SyncVar(hook="OnHealthChange")]
	public int healthPoint;

	public float pollutingSpeed = 1f;

	public GameObject fartPrefab;

	public AudioClip[] fartingSounds;
	AudioSource audioSource;

	CoolDown fartCooldown = new CoolDown(0.5f);

	// Use this for initialization
	void Start () {
		fartCooldown.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		fartCooldown.timer -= Time.deltaTime * pollutingSpeed;
		if(fartCooldown.IsReady()){
			fartCooldown.Reset();
			Fart();
		}else{
			
		}
	}

	public void Fart(){
		//TODO place fart visual effect and bob farting animation

		audioSource.clip = fartingSounds[Random.Range(0,fartingSounds.Length - 1)];
		audioSource.Play();
		ServerFart();
	}

	[ServerCallback]
	void ServerFart(){
		Game.singleton.IncreasePollutionIndex(5.5f);

		var rot = transform.rotation.eulerAngles.y;
		Vector3 offset = Vector3.zero;
		if(rot < 45){
			offset.z = -1f;
		}else if(rot < 90 + 45){
			offset.x = -1f;
		}else if(rot < 180 + 45){
			offset.z = 1f;
		}else if(rot < 270 + 45){
			offset.x = 1f;
		}else{
			offset.z = -1f;
		}
		var fartObj = Instantiate(fartPrefab, transform.position, Quaternion.identity) as GameObject;
		var fart = fartObj.GetComponent<Fart>();
		fart.rb.AddForce(offset, ForceMode.Impulse);
		NetworkServer.Spawn(fartObj);
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
		audioSource = GetComponent<AudioSource>();
		GetComponent<NetworkUnit>().EventOnPlayerChanged +=OnPlayerChanged;
	}
	void Destroy(){
		GetComponent<NetworkUnit>().EventOnPlayerChanged -=OnPlayerChanged;
	}
}
