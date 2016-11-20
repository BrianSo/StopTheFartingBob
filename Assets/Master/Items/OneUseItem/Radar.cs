using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Radar : Item {

	public GameObject radarEffectPrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[Client]
	public override void ItemEffect(ItemUser user){
		Debug.Log("Radar Use");
		var bc = owner.GetComponentInChildren<SelfViewController>();
		if(bc != null){
			var gameObject = Instantiate(radarEffectPrefab);
			gameObject.GetComponent<RadarEffect>().Setup(owner.gameObject);
		}
	}
}
