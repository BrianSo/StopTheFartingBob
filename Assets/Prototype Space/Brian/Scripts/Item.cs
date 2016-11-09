using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Item : NetworkBehaviour {


	public ItemUser owner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// return true to allow using
	/// return false to prevent using
	[Client]
	public virtual bool ItemWillUse(ItemUser user){
		return true;
	}

	/// implement the effect of the item by overriding this method
	/// this method will be invoked in client side
	/// use Command if needed
	[Client]
	public virtual void ItemEffect(ItemUser user){
		Debug.Log("Item Use");
	}

	[Client]
	/// return true to destroy the item after use
	/// return false to prevent destroying
	public virtual bool ShouldItemDestroyAfterUse(ItemUser user){
		return true;
	}

	[Server]
	/// return true to allow pick Up
	/// return false to prevent pick up
	public virtual bool ItemWillPickUp(ItemUser user){
		return true;
	}

	[Client]
	public virtual void ItemDidPickedUp(ItemUser user){}
}
