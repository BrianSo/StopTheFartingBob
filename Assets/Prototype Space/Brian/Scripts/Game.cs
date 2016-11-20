using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// This class handle the logic after the map is generated, the players are placed in the map
/// This class is responsible to handle any game logic and detect any end game condition
///
/// This class also handle he winning screen
/// 
public class Game : NetworkBehaviour {

	public delegate void EventCallback();

	//when game win and shown the end animation, no extra code will the Game run
	public static event EventCallback delegateOnGameFinish;
	//when game win, right at the game wining condition
	public static event EventCallback delegateOnGameEnd;
	public static event EventCallback delegateOnGameStart;
	public static event EventCallback delegateOnGameLeave;
	public static event EventCallback delegateOnBobWin;
	public static event EventCallback delegateOnGardenerWin;

	public static Game singleton;

	public float itemGenerationInterval = 1f;
	private float randomItemTimer;

	public bool isGameStarted = false;

	[SyncVar(hook="OnPollutionIndexChanged")]
	public float pollutionIndex;

	void Awake(){
		this.Singleton(ref singleton);
	}
	void Destroy(){
		this.RemoveSingleton(ref singleton);
	}

	public void StartGame(){
		this.enabled = true;
		Camera.main.GetComponent<AtomsphereControl>().enabled = true;
		//initialization
		pollutionIndex = 0;
		randomItemTimer = itemGenerationInterval;
		isGameStarted = true;
		InGameUIControl.singleton.ShowUI();
		if(delegateOnGameStart!=null)
			delegateOnGameStart();
	}
	public void LeaveGame(){
		this.enabled = false;
		isGameStarted = false;
		Camera.main.GetComponent<AtomsphereControl>().enabled = false;
		InGameUIControl.singleton.HideUI();
		if(delegateOnGameLeave!=null)
			delegateOnGameLeave();
	}

	// Update is called once per frame
	void Update () {
		if(isServer){
			HandleItemGeneration();	
		}
	}

	[Server]
	void HandleItemGeneration(){
		randomItemTimer -= Time.deltaTime;
		if(randomItemTimer <= 0){
			randomItemTimer = itemGenerationInterval;

			// place new item
			var prefab = ItemsPool.GetRandomItemPrefab();
			var obj = Instantiate(prefab, MapManager.singleton.GetItemPosition(), Quaternion.identity) as GameObject;
			NetworkServer.Spawn(obj);
		}
	}

	void OnPollutionIndexChanged(float val){
		pollutionIndex = val;
		//maybe change game ui
	}

	[Server]
	public void IncreasePollutionIndex(float amount){
		if(!isGameStarted)
			return;
		pollutionIndex += amount;
		if(isGameStarted && pollutionIndex >= 100){
			isGameStarted = false;
			RpcBobWin();
		}
	}

	[Server]
	public void BobGotZeroHealth(){
		if(!isGameStarted)
			return;
		isGameStarted = false;
		RpcGardenerWin();
	}

	void StopGame(){
		isGameStarted = false;
		if(delegateOnGameEnd != null)
			delegateOnGameEnd();
	}

	[ClientRpc]
	void RpcBobWin(){
		StopGame();
		if(delegateOnBobWin != null)
			delegateOnBobWin();
		StartCoroutine(BobWin());
	}
	[ClientRpc]
	void RpcGardenerWin(){
		StopGame();
		if(delegateOnGardenerWin != null)
			delegateOnGardenerWin();
		StartCoroutine(GardenerWin());
	}

	[Client]
	IEnumerator BobWin(){
		Debug.Log("Bob win");
		//Play win animation
		yield return Util.MoveCameraTo(Camera.main, GameManager.singleton.bob.transform, 3f);
		//when animation ended

		//show end game ui
		if(delegateOnGameFinish != null)
			delegateOnGameFinish();

		yield return new WaitForSeconds(3f);

		MyNetworkManager.singleton.Disconnect();

		yield return null;
	}

	[Client]
	IEnumerator GardenerWin(){
		Debug.Log("Gardener win");
		//Play win animation
		yield return Util.MoveCameraTo(Camera.main, GameManager.singleton.gardener.transform, 3f);
		//when animation ended

		//show end game ui
		if(delegateOnGameFinish != null)
			delegateOnGameFinish();

		yield return new WaitForSeconds(3f);

		MyNetworkManager.singleton.Disconnect();
		yield return null;
	}
}
