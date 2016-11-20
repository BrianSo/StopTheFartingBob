using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ThrowableItem : Item {

	public float range = 5f;
	public GameObject throwingItemPrefab;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		
	}

	public sealed override void ItemEffect(ItemUser user){
		CmdThrowItem(Util.MousePositionInWorld);
	}

	[Command]
	public void CmdThrowItem(Vector3 destintation){
		Debug.DrawLine(destintation + new Vector3(0.5f, 0, 0.5f), destintation - new Vector3(0.5f, 0, 0.5f),Color.red, 1f);
		Debug.DrawLine(destintation + new Vector3(-0.5f, 0, 0.5f), destintation - new Vector3(-0.5f, 0, 0.5f), Color.red, 1f);

		GameObject throwingItemObj = Instantiate(throwingItemPrefab, owner.gameObject.transform.position, Quaternion.identity) as GameObject;
		ThrowingItem throwingItem = throwingItemObj.GetComponent<ThrowingItem>();
		NetworkServer.Spawn(throwingItemObj);
		throwingItem.Setup(destintation, this.gameObject);
	}
}
