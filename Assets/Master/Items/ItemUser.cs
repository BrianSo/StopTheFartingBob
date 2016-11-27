using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ItemUser : NetworkBehaviour {

	public Animator anim;

	public AudioClip pickUpSound;

	AudioSource audioSource;

	public GameObject itemUI;

	public Item itemCarried;

	const int RIGHT_CLICK = 1;

	public GameObject itemUsedHintPrefab;

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
		StartCoroutine(PlayItemUsedHintAnimation(itemCarried));

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
			//play pick up sound
			audioSource.clip = pickUpSound;
			audioSource.Play ();
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
	IEnumerator PlayItemUsedHintAnimation(Item itemCarried){
		Sprite itemSprite = itemCarried.GetComponentInChildren<SpriteRenderer>().sprite;
		
		for(var i = 0; i < 2; i++){
			var hint = Instantiate(itemUsedHintPrefab, transform.position, Quaternion.identity) as GameObject;
			hint.GetComponent<SpriteRenderer>().sprite = itemSprite;
			yield return new WaitForSeconds(0.1f);
		}
	}

	void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

}
