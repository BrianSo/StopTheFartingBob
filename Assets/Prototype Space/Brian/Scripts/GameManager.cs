using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// GameManager responsible to each game
/// Actually it gave it a wrong name
/// It should be LobbyManager instead
/// It counts the player connection and assign charactor to the players
/// It will be Destroyed after the game end
/// 
/// To find the manager for the whole game software, use SceneManager instead
public class GameManager : NetworkBehaviour {
    public static GameManager singleton;

	public static event DelegateOnStateChanged delegateOnStateChanged;
	public delegate void DelegateOnStateChanged(int state);

	public int _state = 0;
	public int State{get{
		return _state;
	}private set{
		_state = value;
		if(delegateOnStateChanged != null){
			delegateOnStateChanged(_state);
		}
			
	}}
	
	public const int WAITING_CONNECTION = 0;
	public const int WAITING_READY = 1;
	public const int GAME_STARTED = 2;

	bool bobReady;
	bool gardenerReady;

	public GameObject bobPlayer;
	public GameObject gardenerPlayer;

	public GameObject bob;
	public GameObject gardener;

	public GameObject bobPrefab;
	public GameObject gardenerPrefab;

	Vector3 bobHintPosition = new Vector3(-10,0,-30);
	Vector3 gardenerHintPosition = new Vector3(-20,0,-30);

	bool isGameStarted = false;

	[Server]
	public void Synchronize(){
		RpcSynchronize(bobPlayer, gardenerPlayer, bob, gardener);
	}
	[ClientRpc]
	public void RpcSynchronize(GameObject bobPlayer, GameObject gardenerPlayer, GameObject bob, GameObject gardener){
		this.bobPlayer = bobPlayer;
		this.gardenerPlayer = gardenerPlayer;
		this.bob = bob;
		this.gardener = gardener;
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

		StartCoroutine(CheckPlayerCount());
	}
	public void OnServerRemovePlayer(GameObject player){
		//clean up values
		if(bobPlayer == player){
			bobPlayer = null;
		}else if(gardenerPlayer == player){
			gardenerPlayer = null;
		}
		Reset();
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
		RpcChangeState(WAITING_CONNECTION);
	}

	void OnDestroy(){
		Debug.Log("GameManager: Destroy");
		//singleton pattern
		if(this == singleton)
			singleton = null;
		MyNetworkManager.singleton.delegateOnServerAddPlayer -= OnServerAddPlayer;
		MyNetworkManager.singleton.delegateOnServerRemovePlayer -= OnServerRemovePlayer;
		EndGameLocal();
	}
	

	IEnumerator CheckPlayerCount(){
		if(!isServer)
			yield break;
		yield return new WaitForSeconds(0.5f);
		if(bobPlayer != null && gardenerPlayer != null){
			ShowHintScene();
		}
	}
	
	#region hint scene
	[Server]
	void ShowHintScene(){
		bob = Instantiate(bobPrefab,bobHintPosition,Quaternion.identity) as GameObject;
		NetworkServer.SpawnWithClientAuthority(bob, bobPlayer);
		gardener = Instantiate(gardenerPrefab, gardenerHintPosition, Quaternion.identity) as GameObject;
		NetworkServer.SpawnWithClientAuthority(gardener, gardenerPlayer);

		bob.GetComponent<NetworkUnit>().RpcSetPlayer(bobPlayer);
		gardener.GetComponent<NetworkUnit>().RpcSetPlayer(gardenerPlayer);

		Synchronize(); 
		RpcChangeState(WAITING_READY);
	}

	public void PlayerReady(GameObject player){
		Debug.Log(player);
		if(player == bobPlayer){
			Debug.Log("BOB ready");
			bobReady = true;
		}else if(player == gardenerPlayer){
			Debug.Log("gardener ready");
			gardenerReady = true;
		}
		CheckStartGame();
	}

	void CheckStartGame(){
		Debug.Log("CheckStartGame");
		if(bobReady && gardenerReady){
			Debug.Log("startGame");
			StartGame();
		}
	}
	#endregion

	[Server]
	public void StartGame(){
		string seed = MapManager.singleton.getRandomSeed();
		RpcStartGame(seed);
	}

	[ClientRpc]
	public void RpcStartGame(string seed){
		MapManager.singleton.seed = seed;
		MapManager.singleton.GenerateMap();

		PlaceCharactors();
		State = GAME_STARTED;
		this.GetComponent<Game>().StartGame();
	}

	[ClientRpc]
	void RpcChangeState(int state){
		State = state;
	}

	void PlaceCharactors(){
		var mm = MapManager.singleton;
		var bobPosition = mm.GetStartingPosition();
		var gardenerPosition = mm.GetStartingPosition();
		bob.transform.position = bobPosition;
		gardener.transform.position = gardenerPosition;
	}

	[Server]
	public void EndGame(){
		RpcEndGame();
	}
	void RpcEndGame(){
		EndGameLocal();
	}
	void EndGameLocal(){
		MapManager.singleton.DestroyMap();
		this.GetComponent<Game>().LeaveGame();
	}

	void OnGUI(){
		GUI.TextArea(new Rect(30,10,15,20), "" + _state);
		if(GUI.Button(new Rect(45,10,30,20), "rpc")){
			RpcCall();
		}
		if(GUI.Button(new Rect(75,10,30,20), "cmd")){
			CmdCommand();
		}
		if(Time.time < lastTime + 3)
			GUI.TextArea(new Rect(30,30,40,20), message);
	}

	string message = "";
	float lastTime = 0;
	[ClientRpc]
	public void RpcCall(){
		Debug.Log("Rpc");
		message = "Rpc";
		lastTime = Time.time;
	}

	[Command]
	public void CmdCommand(){
		Debug.Log("Command");
		message = "Cmd";
		RpcCall();
		lastTime = Time.time;
	}
}
