using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

/// GameManager responsible to each game
/// It will be Destroyed after the game end
/// 
/// To find the manager for the whole game software, use SceneManager instead
public class GameManager : NetworkBehaviour {
    public static GameManager singleton;

	GameObject gardener;
	GameObject bob;

	public int GameState = IDLE;
	const int IDLE = 0;
	const int WAITING = 1;
	const int PLAYING = 2;
	const int END = 3;
	

	public void Synchronize(){
		RpcSynchronize(bob, gardener);
	}
	[ClientRpc]
	public void RpcSynchronize(GameObject bob, GameObject gardener){
		this.bob = bob;
		this.gardener = gardener;
	}

	public void OnServerAddPlayer(GameObject player){
		//assign charactor to player
		if(bob == null){
			player.GetComponent<Player>().charactor = Player.BOB;
			bob = player;
		}else{
			player.GetComponent<Player>().charactor = Player.GARNDERER;
			gardener = player;
		}
	}
	public void OnServerRemovePlayer(GameObject player){
		//clean up values
		if(bob == player){
			bob = null;
		}else if(gardener == player){
			gardener = null;
		}
	}

    void Awake(){
		//singleton pattern
		if (singleton == null)
			singleton = this;    
		else if (singleton != this)
			Destroy(this);
	}
	
	void Start(){
		MyNetworkManager.singleton.delegateOnServerAddPlayer += OnServerAddPlayer;
		MyNetworkManager.singleton.delegateOnServerRemovePlayer += OnServerRemovePlayer;
		//StartCoroutine(StartGameCoroutine());
	}
	System.Collections.IEnumerator StartGameCoroutine() {
		yield return new WaitForSeconds(1);
		StartGame();
		yield return null;
	}

	void Destroy(){
		//singleton pattern
		if(this == singleton)
			singleton = null;
		MyNetworkManager.singleton.delegateOnServerAddPlayer -= OnServerAddPlayer;
		MyNetworkManager.singleton.delegateOnServerRemovePlayer -= OnServerRemovePlayer;
	}

	[Server]
	public void StartGame(){
		string seed = MapManager.singleton.getRandomSeed();
		RpcGenerateMap(seed);
	}

	[Server]
	void PlaceCharactors(){
		
	}

	[ClientRpc]
	void RpcGenerateMap(string seed){
		MapManager.singleton.seed = seed;
		MapManager.singleton.GenerateMap();

		PlaceCharactors();
	}

	[Server]
	public void EndGame(){

	}
}
