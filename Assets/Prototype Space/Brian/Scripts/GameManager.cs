using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// GameManager responsible to each game
/// It will be Destroyed after the game end
/// 
/// To find the manager for the whole game software, use SceneManager instead
public class GameManager : NetworkBehaviour {
    public static GameManager singleton;

	
	GameObject bobPlayer;
	GameObject gardenerPlayer;

	GameObject bob;
	GameObject gardener;

	public GameObject bobPrefab;
	public GameObject gardenerPrefab;

	Vector3 bobHintPosition = new Vector3(-10,0,-30);
	Vector3 gardenerHintPosition = new Vector3(-20,0,-30);

	bool isGameStarted = false;

	public void Synchronize(){
		RpcSynchronize(bobPlayer, gardenerPlayer);
	}
	[ClientRpc]
	public void RpcSynchronize(GameObject bob, GameObject gardener){
		this.bobPlayer = bob;
		this.gardenerPlayer = gardener;
	}

	public void OnServerAddPlayer(GameObject player){
		//assign charactor to player
		if(bobPlayer == null){
			player.GetComponent<Player>().charactor = Player.BOB;
			bobPlayer = player;
		}else if(gardenerPlayer == null){
			player.GetComponent<Player>().charactor = Player.GARNDERER;
			gardenerPlayer = player;
		}

		StartCoroutine(CheckStartGame());
	}
	public void OnServerRemovePlayer(GameObject player){
		//clean up values
		if(bobPlayer == player){
			bobPlayer = null;
			Reset();
		}else if(gardenerPlayer == player){
			gardenerPlayer = null;
			Reset();
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
	}

	void Reset(){
		NetworkServer.Destroy(bob);
		NetworkServer.Destroy(gardener);
		isGameStarted = false;
	}

	void Destroy(){
		//singleton pattern
		if(this == singleton)
			singleton = null;
		MyNetworkManager.singleton.delegateOnServerAddPlayer -= OnServerAddPlayer;
		MyNetworkManager.singleton.delegateOnServerRemovePlayer -= OnServerRemovePlayer;
	}
	

	IEnumerator CheckStartGame(){
		if(!isServer)
			yield break;
		yield return new WaitForSeconds(0.5f);
		if(bobPlayer != null && gardenerPlayer != null){
			ShowHintScene();
		}
	}
	

	void ShowHintScene(){
		bob = Instantiate(bobPrefab,bobHintPosition,Quaternion.identity) as GameObject;
		NetworkServer.SpawnWithClientAuthority(bob, bobPlayer);
		gardener = Instantiate(gardenerPrefab, gardenerHintPosition, Quaternion.identity) as GameObject;
		NetworkServer.SpawnWithClientAuthority(gardener, gardenerPlayer);

		bob.GetComponent<NetworkUnit>().RpcSetPlayer(bobPlayer);
		gardener.GetComponent<NetworkUnit>().RpcSetPlayer(gardenerPlayer);
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
