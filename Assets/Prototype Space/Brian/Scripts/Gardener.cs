﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Gardener : NetworkBehaviour {

	public Animator anim;
	public CoolDown attackCooldown;
	public int attackDamage = 1;
	public float attackRadius = 0.5f;
	public float attackRange = 1.0f;
	public LayerMask attackLayer;

	bool isOwnByLocalPlayer;

	// Use this for initialization
	void Start () {
		this.isOwnByLocalPlayer = this.IsOwnByLocalPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		attackCooldown.Update();

		if(isOwnByLocalPlayer){
			var leftClick = Input.GetMouseButtonDown(0);
			if(leftClick && attackCooldown.IsReady()){
				CmdAttack(Util.MousePositionInWorld);
			}
		}
	}

	//check cool down
	//check hit
	[Command]
	void CmdAttack(Vector3 mousePosition){
		if(attackCooldown.IsReady()){
			attackCooldown.Reset();
			
			var isHit = false;

			//Use CapsuleCast to check whether it is hit
			RaycastHit[] hits;
			Vector3 p1 = transform.position;
			Vector3 direction = (mousePosition - p1).normalized;
			Vector3 p2 = p1 + direction * attackRange;
			
			// use spherecast instead of raycast so that the ray has a radius on it 
			hits = Physics.SphereCastAll(p1, attackRadius, direction, attackRange, attackLayer);
			Debug.Log("hit count:" + hits.Length);
			Debug.DrawLine(p1, p2, Color.red, 1f);
			if (hits.Length > 0){
				foreach(var hit in hits){
					var gameObject = hit.collider.gameObject;
					var bob = gameObject.GetComponent<Bob>();
					if(bob != null){
						Debug.Log("gardener: attack you!");
						isHit = true;
						bob.TakeDamage(attackDamage);
						bob.RpcGetHurted(attackDamage);
					}
				}
			}

			RpcAttack(isHit);
		}
	}

	//play aniamtion
	//reset cooldown
	[ClientRpc]
	void RpcAttack(bool isHit){
		attackCooldown.Reset();

		//TODO player animation
		StartCoroutine("PlayAttackAnimation");
		
		if(isHit){
			//maybe play a sound for hit
		}else{
			//maybe play a sound for not hit
		}
	}

	IEnumerator PlayAttackAnimation() {
		anim.SetBool ("isAttacking", true);
		yield return new WaitForSeconds(0.5f);
		anim.SetBool ("isAttacking", false);
	}
}
