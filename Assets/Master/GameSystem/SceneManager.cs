﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public static SceneManager singleton;

	int _state;
	public int State{
		get{return _state;}
		private set{
			_state = value;
			OnStateChange(_state);
		}
	}
	const int MAIN_MENU = 0;
	const int CONNECTING_TO_HOST = 1;
	const int WAITING_CLIENT = 2;
	const int SELECTION_SCENE = 3;
	const int GAME_SCENE = 4;

	public GameObject mainMenuUI;
	public GameObject connectingToHostUI;
	public GameObject WaitingClientUI;
	public GameObject SelectionSceneUI;
	public GameObject GameSceneUI;

	// Use this for initialization
	void Start () {
		ApplyDelegate();
		this.State = MAIN_MENU;
	}

	void OnGUI(){
		//GUI.TextArea(new Rect(10, 10, 15, 20), "" + _state);
	}

	void OnStateChange(int state){
		Debug.Log("Scene: OnStateChange:" + state);
		mainMenuUI.SetActive(false);
		connectingToHostUI.SetActive(false);
		WaitingClientUI.SetActive(false);
		SelectionSceneUI.SetActive(false);
		GameSceneUI.SetActive(false);
		switch(state){
			case MAIN_MENU:
				mainMenuUI.SetActive(true);
				break;
			case CONNECTING_TO_HOST:
				connectingToHostUI.SetActive(true);
				break;
			case WAITING_CLIENT:
				WaitingClientUI.SetActive(true);
				break;
			case SELECTION_SCENE:
				SelectionSceneUI.SetActive(true);
				break;
			case GAME_SCENE:
				GameSceneUI.SetActive(true);
				break;
		}
	}

	void ApplyDelegate(){
		MyNetworkManager nm = MyNetworkManager.singleton;
		nm.delegateOnStartHost += OnStartHost;
		nm.delegateOnStartClient += OnStartClient;
		nm.delegateOnStopHost += OnDisconnected;
		nm.delegateOnStopClient += OnDisconnected;
		GameManager.delegateOnStateChanged += OnGameStateChanged;
	}
	void RemoveDelegate(){
		MyNetworkManager nm = MyNetworkManager.singleton;
		nm.delegateOnStartHost -= OnStartHost;
		nm.delegateOnStartClient -= OnStartClient;
		nm.delegateOnStopHost -= OnDisconnected;
		nm.delegateOnStopClient -= OnDisconnected;
		GameManager.delegateOnStateChanged -= OnGameStateChanged;
	}

	void OnGameStateChanged(int state){
		Debug.Log("OnGameStateChanged:" + state);
		switch(state){
			case GameManager.WAITING_CONNECTION:
				if(!MyNetworkManager.singleton.isNetworkActive){
					State = MAIN_MENU;
				}else if(MyNetworkManager.singleton.isHost){
					State = WAITING_CLIENT;
				}else{
					State = CONNECTING_TO_HOST;
				}
			break;
			case GameManager.WAITING_READY:
				State = SELECTION_SCENE;
			break;
			case GameManager.GAME_STARTED:
				State = GAME_SCENE;
			break;
		}
	}
	
	void OnStartHost(){
		State = WAITING_CLIENT;
	}

	void OnStartClient(){
		if(MyNetworkManager.singleton.isHost){
			State = WAITING_CLIENT;
		}else{
			State = CONNECTING_TO_HOST;
		}
	}

	void OnDisconnected(){
		State = MAIN_MENU;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void Awake(){
		if (singleton == null){
			singleton = this;
			DontDestroyOnLoad(gameObject);    
		}else if (singleton != this){
			Destroy(this);
		}
	}
	void OnDestroy(){
		// reslove singleton
		if(singleton == this)
			singleton = null;

		RemoveDelegate();
	}
}
