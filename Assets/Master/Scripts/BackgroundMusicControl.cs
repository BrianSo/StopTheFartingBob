using UnityEngine;
using System.Collections;

public class BackgroundMusicControl : MonoBehaviour {

	// Use this for initialization

	public AudioSource player;

	public AudioClip backgroundMusic;
	public AudioClip gameBackgroundMusic;
	public AudioClip bobWinSound;
	public AudioClip gardenerWinSound;
	public AudioClip bobWinBackgroundMusic;
	public AudioClip gardenerWinBackgroundMusic;

	void Awake(){
		Game.delegateOnGameStart += OnGameStart;
		Game.delegateOnGameEnd += OnGameEnd;
		Game.delegateOnBobWin += OnBobWin;
		Game.delegateOnGardenerWin += OnGardenerWin;
		Game.delegateOnGameLeave += OnGameLeave;

		player.clip = backgroundMusic;
		player.loop = true;
		player.Play();
	}

	void Destroy(){
		Game.delegateOnGameStart -= OnGameStart;
		Game.delegateOnGameEnd -= OnGameEnd;
	}

	void OnGameLeave(){
		player.clip = backgroundMusic;
		player.loop = true;
		player.Play();
	}
	void OnGameStart(){
		player.clip = gameBackgroundMusic;
		player.loop = true;
		player.Play();
	}

	void OnGameEnd(){
		player.Stop();
	}

	void OnBobWin(){
		player.clip = bobWinSound;
		player.loop = false;
		player.Play();
	}

	void OnGardenerWin(){
		player.clip = gardenerWinSound;
		player.loop = false;
		player.Play();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
