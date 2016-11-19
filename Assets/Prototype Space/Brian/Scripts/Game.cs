using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// This class handle the logic after the map is generated, the players are placed in the map
/// This class is responsible to handle any game logic and detect any end game condition
///
/// GameManager will enable this component to start the game
/// GameManager will disable this component to end the game
public class Game : NetworkBehaviour {

	public static event DelegateOnGameEnd delegateOnGameEnd;
	public delegate void DelegateOnGameEnd();

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

	void OnEnable () {
		StartGame();
		Camera.main.GetComponent<AtomsphereControl>().enabled = true;
	}
	void OnDisable(){
		isGameStarted = false;
		Camera.main.GetComponent<AtomsphereControl>().enabled = false;
		InGameUIControl.singleton.HideUI();
	}

	void StartGame(){
		//initialization
		pollutionIndex = 0;
		randomItemTimer = itemGenerationInterval;
		isGameStarted = true;
		InGameUIControl.singleton.ShowUI();
	}

	void EndGame(){
		isGameStarted = false;
		if(delegateOnGameEnd != null)
			delegateOnGameEnd();
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
		Debug.Log("pollution index: " + val + " & " + pollutionIndex);
		//maybe change game ui
	}

	[Server]
	public void IncreasePollutionIndex(float amount){
		if(!isGameStarted)
			return;
		pollutionIndex += amount;
		if(isGameStarted && pollutionIndex >= 100){
			StopGame();
			RpcBobWin();
		}
	}

	[Server]
	public void BobGotZeroHealth(){
		if(!isGameStarted)
			return;
		StopGame();
		RpcGardenerWin();
	}

	void StopGame(){
		isGameStarted = false;
	}

	[ClientRpc]
	void RpcBobWin(){
		StopGame();
		StartCoroutine(BobWin());
	}
	[ClientRpc]
	void RpcGardenerWin(){
		StopGame();
		StartCoroutine(GardenerWin());
	}

	[Client]
	IEnumerator BobWin(){
		Debug.Log("Bob win");
		//Play win animation
		yield return new WaitForSeconds(3f);
		//when animation ended
		//show end game ui
		EndGame();
		yield return null;
	}

	[Client]
	IEnumerator GardenerWin(){
		Debug.Log("Gardener win");
		//Play win animation
		yield return new WaitForSeconds(3f);
		//when animation ended
		//show end game ui
		EndGame();
		yield return null;
	}
}
