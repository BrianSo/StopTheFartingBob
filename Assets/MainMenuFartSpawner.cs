using UnityEngine;
using System.Collections;

public class MainMenuFartSpawner : MonoBehaviour {


	CoolDown fartTimer;
	public GameObject uiFartPrefab;

	// Use this for initialization
	void Start () {
		fartTimer = new CoolDown(0.3f);
	}
	
	// Update is called once per frame
	void Update () {
		fartTimer.Update();
		if(fartTimer.IsReady()){
			SpawnFart();
			fartTimer.Reset();
		}
	}

	void SpawnFart(){
		var fart = Instantiate(uiFartPrefab);
		fart.transform.parent = gameObject.transform;
		fart.transform.position = gameObject.transform.position + new Vector3(10,-4,0);
	}
}
