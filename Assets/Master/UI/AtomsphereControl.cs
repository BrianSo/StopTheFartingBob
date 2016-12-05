using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class AtomsphereControl : MonoBehaviour {

	ScreenOverlay overlay;

	void Awake(){
		overlay = GetComponent<ScreenOverlay>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Game.singleton){
			var pollutionRate = (Game.singleton.pollutionIndex/100f);
			overlay.intensity = pollutionRate;
		}
	}

	void OnEnable(){
		overlay.enabled = true;
	}
	void OnDisable(){
		overlay.enabled = false;
	}
}
