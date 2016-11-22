using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ItemUser : NetworkBehaviour {

	public Animator anim;

	public GameObject itemUI;

	public Item itemCarried;

	const int RIGHT_CLICK = 1;

	NetworkUnit unit;
	// Use this for initialization
	void Start () {
		unit = this.GetComponent<NetworkUnit>();
	}
	
	bool IsOwnByLocalPlayer(){
		return unit.player == Player.localPlayer;
	}

	// Update is called once per frame
	void Update () {
		if(IsOwnByLocalPlayer()){

			// use item using right click
			if(Input.GetMouseButtonDown(RIGHT_CLICK) && itemCarried != null){
				if(itemCarried.ItemWillUse(this)){

					itemCarried.ItemEffect(this);

					if(itemCarried.ShouldItemDestroyAfterUse(this)){
						CmdDestoryItemAfterUse();
					}
				}
			}
		}
	}
	[Command]
	void CmdDestoryItemAfterUse(){
		RpcDestoryItemAfterUse();
	}
	[ClientRpc]
	void RpcDestoryItemAfterUse(){
		// Use animation for player
		StartCoroutine("PlayUseAnimation");

		Destroy(itemCarried.gameObject, 2f);//delay destroy
		itemCarried = null;
		if(this.IsOwnByLocalPlayer()){
			//do UI stuff
			InGameUIControl.singleton.RemoveItem();
		}
	}

	[Server]
	void OnTriggerEnter(Collider other){
		
		if(itemCarried == null && other.gameObject.CompareTag("Item")){
			Item item = other.gameObject.GetComponent<Item>();
			if(item.ItemWillPickUp(this)){
				//assign the item to the player
				NetworkIdentity netId = item.gameObject.GetComponent<NetworkIdentity>();
				netId.AssignClientAuthority(this.GetPlayer().connectionToClient);

				//do this on server in advance to prevent picking up multiple items
				itemCarried = item; 
				RpcPickUpItem(item.gameObject);
			}
		}
	}

	[ClientRpc]
	void RpcPickUpItem(GameObject itemObj){
		Item item = itemObj.GetComponent<Item>();
		this.itemCarried = item;
		item.owner = this;
		if(this.IsOwnByLocalPlayer()){
			//do UI stuff
			InGameUIControl.singleton.PickUpItem(itemObj.GetComponentInChildren<SpriteRenderer>().sprite);
		}
		Debug.Log("Going to Destroy item");
		item.gameObject.SetActive(false);
		item.ItemDidPickedUp(this);
	}

	IEnumerator PlayUseAnimation() {
		anim.SetBool ("isUsing", true);
		yield return new WaitForSeconds(0.5f);
		anim.SetBool ("isUsing", false);
	}
}
