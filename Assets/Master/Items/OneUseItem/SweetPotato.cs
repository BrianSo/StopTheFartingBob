using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SweetPotato : Item {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[Client]
	public override void ItemEffect(ItemUser user){	
		CmdItemEffect();
	}

	[Command]
	public void CmdItemEffect(){
		
		var bob = owner.GetComponent<Bob>();
		if(bob != null){
			if(bob.healthPoint < 3)
				bob.healthPoint += 1;
			bob.BigFart();
		}
	}
}
