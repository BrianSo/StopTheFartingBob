using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BunanaPeel : ThrowingItem {


	new SphereCollider collider;
	public SpriteRenderer dangerousHintRenderer;

	
	protected override void Awake(){
		base.Awake();
		collider = this.GetComponent<SphereCollider>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnItemArrived(){
		collider.isTrigger = true;
		collider.enabled = true;
		dangerousHintRenderer.enabled = true;
	}

	[ServerCallback]
	void OnTriggerEnter(Collider other){
		//Peel Effect
		if(other.gameObject.GetComponent<BaseCharactor>()){
			RpcTriggered(other.gameObject);
		}
	}

	[ClientRpc]
	void RpcTriggered(GameObject other){
		gameObject.SetActive(false);
		Destroy(gameObject, 2f);
		Debug.Log("I step on bunanaPeel, T^T");
		//Play Animation
		other.GetComponent<BaseCharactor>().PlayHitAnimationHelper();
	}
}
