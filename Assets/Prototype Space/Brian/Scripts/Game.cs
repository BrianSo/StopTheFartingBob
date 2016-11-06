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

	[SyncVar(hook="OnPollutionIndexChanged")]
	public int pollutionIndex;

	void OnEnable () {
		this.Singleton(ref singleton);
		StartGame();
	}
	void OnDisable(){
		this.RemoveSingleton(ref singleton);
	}

	void StartGame(){
		//initialization
		pollutionIndex = 0;
	}

	void EndGame(){
		if(delegateOnGameEnd != null)
			delegateOnGameEnd();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnPollutionIndexChanged(int val){
		if(val > 100){
			StartCoroutine(BobWin());
		}
	}

	IEnumerator BobWin(){
		//Play win animation
		yield return new WaitForSeconds(3f);
		//when animation ended
		//show end game ui
		EndGame();
		yield return null;
	}

	IEnumerator GardenerWin(){
		//Play win animation
		yield return new WaitForSeconds(3f);
		//when animation ended
		//show end game ui
		EndGame();
		yield return null;
	}
}
