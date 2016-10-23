using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour {
    public static GameManager singleton;

	List<GameObject> charactors;
	GameObject gardener;
	GameObject bob;

    void Awake(){
		//singleton pattern
		if (singleton == null)
			singleton = this;    
		else if (singleton != this)
			Destroy(this);
	}
	void Start(){
		StartCoroutine(StartGameCoroutine());
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
